using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bot;

namespace Twitter.Bot.Webhook
{
    using Bot;

    public class WebhookServer
    {
        public const string WebhookPath = "webhook"; // http://host:port/webhook
        public const string WebhookStatusPath = "status"; // http://host:port/status

        private IWebHost host;
        private LogLevel logLevel;
        private int _eventId = 1;

        internal WebhookServer()
        {
            LoggerFactory factory = new LoggerFactory();
            factory.AddConsole().AddDebug();
            Logger = factory.CreateLogger(this.GetType().ToString() + $"[WITHOUT_WEBHOOK]");
        }

        public WebhookServer(int port, string consumerSecret, LogLevel level)
        {
            Port = port;
            ConsumerSecret = consumerSecret;
            //VerifyToken = verifyToken;
            logLevel = level;
            CreateWebhookHost();
        }

        public ILogger Logger { get; private set; }

        public int Port { get; private set; }

        public string ConsumerSecret { get; private set; }

        public virtual async void StartReceivingAsync()
        {
            await Task.Run(() =>
            {
                //var builder = WebHost.CreateDefaultBuilder();

                //builder.ConfigureServices(cfg =>
                //{
                //    cfg.AddRouting();
                //});

                //builder.ConfigureLogging(cfg =>
                //{
                //    //cfg.ClearProviders();
                //    //cfg.AddConsole();
                //    //cfg.AddDebug();
                //});

                //builder.UseKestrel(options =>
                //{
                //    options.Listen(IPAddress.Any, Port);
                //});

                //builder.Configure(cfg =>
                //{
                //    cfg.UseRouter(r =>
                //    {
                //        r.MapGet(WebhookTestPath, Test);
                //        r.MapGet(WebhookPath, Get);
                //        r.MapPost(WebhookPath, Post);
                //    });
                //});

                //host = builder.Build();
                //logger = host.Services.GetService<ILoggerFactory>().CreateLogger(this.GetType().ToString() + $"[{IPAddress.Any}:{Port}]");

                host.Run();
            });
        }

        public void WaitForShutdown()
        {
            while (host == null)
            {
                Thread.Sleep(100);
            }

            host.WaitForShutdown();
        }


        public delegate void PostHandler(PostEventArgs e);

        public delegate void MessageHandler(MessageEventArgs e);

        public event PostHandler PostReceived;

        public event PostHandler PostFailed;

        public event MessageHandler MessageReceived;


        private void CreateWebhookHost()
        {
            var builder = WebHost.CreateDefaultBuilder();

            builder.ConfigureServices(cfg =>
            {
                cfg.AddRouting();
            });

            builder.ConfigureLogging(cfg =>
            {
                cfg.SetMinimumLevel(logLevel);
                //cfg.ClearProviders();
                //cfg.AddConsole();
                //cfg.AddDebug();
            });

            builder.UseKestrel(options =>
            {
                options.Listen(IPAddress.Any, Port);
            });

            builder.Configure(cfg =>
            {
                cfg.UseRouter(r =>
                {
                    r.MapGet(WebhookStatusPath, Status);
                    r.MapGet(WebhookPath, Get);
                    r.MapPost(WebhookPath, Post);
                });
            });

            host = builder.Build();
            Logger = host.Services.GetService<ILoggerFactory>().CreateLogger(this.GetType().ToString() + $"[{IPAddress.Any}:{Port}]");
        }

        private async Task Status(HttpRequest request, HttpResponse response, RouteData route)
        {
            try
            {
                response.ContentType = ContentTypes.TextHtml;
                await response.WriteAsync(Resources.WebhookStatus.Format(request.HttpContext.Connection.RemoteIpAddress));
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
            }
        }

        private async Task Get(HttpRequest request, HttpResponse response, RouteData route)
        {
            var eventId = new EventId(_eventId++);

            try
            {
                string field = "crc_token";
                var connection = request.HttpContext.Connection;

                if (request.Query.ContainsKey(field))
                {
                    var crcToken = request.Query[field];
                    Logger.LogInformation(eventId, Resources.SubscriptionSuccess.Format(connection.RemoteIpAddress, connection.RemotePort));
                    response.ContentType = ContentTypes.ApplicationJson;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    await response.WriteAsync(CRC(ConsumerSecret, crcToken));
                }
                else
                {
                    Logger.LogWarning(eventId, Resources.SubscriptionFail.Format(connection.RemoteIpAddress, connection.RemotePort));
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                }
            }
            catch (Exception e)
            {
                Logger.LogError(eventId, e, e.Message);
            }
        }

        private async Task Post(HttpRequest request, HttpResponse response, RouteData route)
        {
            var eventId = new EventId(_eventId++);

            try
            {
                byte[] buf = new byte[request.ContentLength.Value];
                await request.Body.ReadAsync(buf, 0, buf.Length);

                string body = Encoding.UTF8.GetString(buf);
                Logger.LogDebug(eventId, Resources.WebhookPost + body);
#if !DEBUG
                const string signatureHeader = "X-Hub-Signature";
                
                if (!request.Headers.Keys.Contains(signatureHeader))
                {
                    Logger.LogWarning(Resources.InvalidSignature);

                    if (PostFailed != null)
                    {
                        ThreadPool.QueueUserWorkItem(state => PostFailed.Invoke(new PostEventArgs()
                        {
                            Headers = request.Headers,
                            Body = body
                        }));
                    }

                    return;
                }

                var signature = request.Headers[signatureHeader][0];

                if (!VerifySignature(signature, buf))
                {
                    Logger.LogWarning(Resources.InvalidSignature);

                    if (PostFailed != null)
                    {
                        ThreadPool.QueueUserWorkItem(state => PostFailed.Invoke(new PostEventArgs()
                        {
                            Headers = request.Headers,
                            Body = body
                        }));
                    }

                    return;
                }
#endif          
                if (PostReceived != null)
                {
                    ThreadPool.QueueUserWorkItem(state => PostReceived.Invoke(new PostEventArgs()
                    {
                        Headers = request.Headers,
                        Body = body
                    }));
                }

                ProcessRequest(body);
            }
            catch (Exception e)
            {
                Logger.LogError(eventId, e, e.Message);
            }
        }

        private void ProcessRequest(string body)
        {
            object parsedJson = JsonConvert.DeserializeObject(body);

            Console.WriteLine(parsedJson.ToJson());

            WebhookEvent e = body.FromJson<WebhookEvent>();

            if (e.DirectMessageEvents != null)
            {
                foreach (var item in e.DirectMessageEvents)
                {
                    Message m = new Message
                    {
                        Id = item.Id,
                        Timestamp = item.Timestamp,
                        Sender = item.Data.Sender,
                        Recipient = item.Data.Target.RecipientId,
                        Text = item.Data.Data.Text,
                        QuickReplyResponse = item.Data.Data.QuickReplyResponse
                    };

                    MessageEventArgs messageEventArgs = new MessageEventArgs()
                    {
                        Message = m
                    };

                    ThreadPool.QueueUserWorkItem(state => MessageReceived.Invoke(messageEventArgs));
                }
            }

            //var e = JsonConvert.DeserializeObject<Event>(body);

            //foreach (var entry in e.Entries)
            //{
            //    foreach (var item in entry.Items)
            //    {
            //        if (item.Message != null && MessageReceived != null)
            //        {
            //            MessageEventArgs messageEventArgs = new MessageEventArgs()
            //            {
            //                Sender = item.Sender.Id,
            //                Message = item.Message
            //            };

            //            ThreadPool.QueueUserWorkItem(state => MessageReceived.Invoke(messageEventArgs));
            //        }

            //        if (item.Postback != null && PostbackReceived != null)
            //        {
            //            PostbackEventArgs postbackEventArgs = new PostbackEventArgs()
            //            {
            //                Sender = item.Sender.Id,
            //                Postback = item.Postback
            //            };

            //            ThreadPool.QueueUserWorkItem(state => PostbackReceived.Invoke(postbackEventArgs));
            //        }
            //    }
            //}
        }



        internal static string CRC(string consumerSecret, string crcToken)
        {
            byte[] consumerSecretBytes = Encoding.UTF8.GetBytes(consumerSecret);
            byte[] crcTokenBytes = Encoding.UTF8.GetBytes(crcToken);

            HMACSHA256 hmacSHA256Alog = new HMACSHA256(consumerSecretBytes);

            byte[] computedHash = hmacSHA256Alog.ComputeHash(crcTokenBytes);

            return "{\n" +
                $"\"response_token\":\"sha256={Convert.ToBase64String(computedHash)}\"" +
                "\n}";
        }
    }
}
