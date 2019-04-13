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

namespace Messenger.Bot.Webhook
{
    public class WebhookServer
    {
        public const string WebhookPath = "webhook"; // http://host:port/webhook
        public const string WebhookTestPath = "test"; // http://host:port/test

        private IWebHost host;
        private LogLevel logLevel;
        private int _eventId = 1;

        internal WebhookServer()
        {
            LoggerFactory factory = new LoggerFactory();
            factory.AddConsole().AddDebug();
            Logger = factory.CreateLogger(this.GetType().ToString() + $"[WITHOUT_WEBHOOK]");
        }

        public WebhookServer(int port, string appSecret, string verifyToken, LogLevel level)
        {
            Port = port;
            AppSecret = appSecret;
            VerifyToken = verifyToken;
            logLevel = level;
            CreateWebhookHost();
        }

        public ILogger Logger { get; private set; }

        public int Port { get; private set; }

        public string AppSecret { get; private set; }

        public string VerifyToken { get; private set; }

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

        public delegate void PostbackHandler(PostbackEventArgs e);

        public event PostHandler PostReceived;

        public event PostHandler PostFailed;

        public event MessageHandler MessageReceived;

        public event PostbackHandler PostbackReceived;

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
                    r.MapGet(WebhookTestPath, Test);
                    r.MapGet(WebhookPath, Get);
                    r.MapPost(WebhookPath, Post);
                });
            });

            host = builder.Build();
            Logger = host.Services.GetService<ILoggerFactory>().CreateLogger(this.GetType().ToString() + $"[{IPAddress.Any}:{Port}]");
        }

        private async Task Test(HttpRequest request, HttpResponse response, RouteData route)
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
                var mode = request.Query["hub.mode"];
                var token = request.Query["hub.verify_token"];
                var challenge = request.Query["hub.challenge"];
                var connection = request.HttpContext.Connection;

                if (mode == "subscribe" && token == VerifyToken)
                {
                    Logger.LogInformation(eventId, Resources.SubscriptionSuccess.Format(connection.RemoteIpAddress, connection.RemotePort));
                    response.ContentType = ContentTypes.TextHtml;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    await response.WriteAsync(challenge);
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

        private bool VerifySignature(string signature, byte[] body)
        {
            using (var crypto = new HMACSHA1(Encoding.UTF8.GetBytes(AppSecret)))
            {
                var hash = crypto.ComputeHash(body).ToHex();
                return hash.ToLower() == signature.Replace("sha1=", "").ToLower();
            }
        }

        private void ProcessRequest(string body)
        {
            var e = JsonConvert.DeserializeObject<Event>(body);

            foreach (var entry in e.Entries)
            {
                foreach (var item in entry.Items)
                {
                    if (item.Message != null && MessageReceived != null)
                    {
                        MessageEventArgs messageEventArgs = new MessageEventArgs()
                        {
                            Sender = item.Sender.Id,
                            Message = item.Message
                        };

                        ThreadPool.QueueUserWorkItem(state => MessageReceived.Invoke(messageEventArgs));
                    }

                    if (item.Postback != null && PostbackReceived != null)
                    {
                        PostbackEventArgs postbackEventArgs = new PostbackEventArgs()
                        {
                            Sender = item.Sender.Id,
                            Postback = item.Postback
                        };

                        ThreadPool.QueueUserWorkItem(state => PostbackReceived.Invoke(postbackEventArgs));
                    }
                }
            }
        }
    }
}
