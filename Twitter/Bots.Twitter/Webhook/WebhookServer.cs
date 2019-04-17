using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bots.Twitter.Webhook
{
    public class WebhookServer : WebhookServerBase
    {
        private class RecipientChecker
        {
            [JsonProperty("for_user_id")]
            public long Recipient { get; set; }
        }

        public WebhookServer(int port, string consumerSecret, LogLevel logLevel) : this(port, consumerSecret, 0, logLevel)
        {      
        }

        public WebhookServer(int port, string consumerSecret, long userId, LogLevel logLevel) : base(port, logLevel)
        {
            ConsumerSecret = consumerSecret;
            Recipient = userId;

            GetReceived += WebhookServer_GetReceived;
            PostReceived += WebhookServer_PostReceived;
        }

        public override string WebhookPath => "webhook"; // http://host:port/webhook

        public string ConsumerSecret { get; private set; }

        /// <summary>
        /// 0 - process any subscription
        /// </summary>
        public long Recipient { get; set; } = 0;

        
        public delegate void MessageHandler(MessageEventArgs e);

        public delegate void FollowHandler(FollowEventArgs e);

        public delegate void TweetHandler(TweetEventArgs e);

        public delegate void LikeHandler(LikeEventArgs e);

        public event WebhookHandler InvalidPostReceived;

        public event MessageHandler OnMessage;

        public event FollowHandler OnFollow;

        public event FollowHandler OnUnFollow;

        public event TweetHandler OnTweet;

        public event TweetHandler OnRetweet;

        public event TweetHandler OnQuote;

        public event TweetHandler OnComment;

        public event TweetHandler OnMention;

        public event LikeHandler OnLike;

        
        private async void WebhookServer_GetReceived(WebhookEventArgs e)
        {
            try
            {
                string field = "crc_token";
                var connection = e.Request.HttpContext.Connection;

                if (e.Request.Query.ContainsKey(field))
                {
                    var crcToken = e.Request.Query[field];
                    Logger.LogWarning(EventId, Resources.SubscriptionSuccess.Format(connection.RemoteIpAddress, connection.RemotePort));
                    e.Request.ContentType = ContentTypes.ApplicationJson;
                    e.Response.StatusCode = (int)HttpStatusCode.OK;
                    await e.Response.WriteAsync(CRC(ConsumerSecret, crcToken));
                    e.IsValid = true;
                }
                else
                {
                    Logger.LogWarning(EventId, Resources.SubscriptionFail.Format(connection.RemoteIpAddress, connection.RemotePort));
                    e.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(EventId, ex, ex.Message);
            }
        }

        private async void WebhookServer_PostReceived(WebhookEventArgs e)
        {
            try
            {
#if !DEBUG
                const string signatureHeader = "X-Twitter-Webhooks-Signature";
                
                if (!e.Request.Headers.Keys.Contains(signatureHeader))
                {
                    Logger.LogWarning(Resources.InvalidSignature);

                    InvalidPostReceived?.Invoke(e);

                    return;
                }

                var signature = e.Request.Headers[signatureHeader][0];

                if (!VerifySignature(signature, e.BodyRaw))
                {
                    Logger.LogWarning(Resources.InvalidSignature);

                    InvalidPostReceived?.Invoke(e);

                    return;
                }
#endif
                e.IsValid = true;

                if (Recipient != 0)
                {
                    RecipientChecker check = e.Body.FromJson<RecipientChecker>();

                    if (Recipient != check.Recipient) return;
                }

                WebhookEvent webhookEvent = e.Body.FromJson<WebhookEvent>();

                if (webhookEvent.DirectMessageEvents != null)
                {
                    if (OnMessage != null)
                    {
                        foreach (var item in webhookEvent.DirectMessageEvents)
                        {
                            MessageEventArgs args = new MessageEventArgs()
                            {
                                Recipient = webhookEvent.Recipient,
                                Message = item.ToMessage()
                            };

                            await Task.Run(() =>
                            {
                                OnMessage.Invoke(args);
                            });
                        }
                    }
                }

                if (webhookEvent.FollowEvents != null)
                {
                    foreach (var item in webhookEvent.FollowEvents)
                    {
                        if (item.Type == "follow" && OnFollow != null)
                        {
                            FollowEventArgs args = new FollowEventArgs()
                            {
                                Recipient = webhookEvent.Recipient,
                                Timestamp = item.Timestamp,
                                Type = FollowType.Follow,
                                Target = item.Target,
                                Source = item.Source
                            };

                            await Task.Run(() =>
                            {
                                OnFollow.Invoke(args);
                            });
                        }

                        if (item.Type == "unfollow" && OnFollow != null)
                        {
                            FollowEventArgs args = new FollowEventArgs()
                            {
                                Recipient = webhookEvent.Recipient,
                                Timestamp = item.Timestamp,
                                Type = FollowType.Unfollow,
                                Target = item.Target,
                                Source = item.Source
                            };

                            await Task.Run(() =>
                            {
                                OnUnFollow.Invoke(args);
                            });
                        }
                    }
                }

                if (webhookEvent.TweetCreateEvents != null)
                {
                    foreach (var item in webhookEvent.TweetCreateEvents)
                    {
                        TweetEventArgs args = new TweetEventArgs()
                        {
                            Recipient = webhookEvent.Recipient,
                            Tweet = item
                        };

                        bool processed = false;

                        if (item.RetweetedFrom != null)
                        {
                            if (OnRetweet != null)
                            {
                                await Task.Run(() =>
                                {
                                    OnRetweet.Invoke(args);
                                });
                            }

                            processed = true;
                        }

                        if (item.QuotedFrom != null)
                        {
                            if (OnQuote != null)
                            {
                                await Task.Run(() =>
                                {
                                    OnQuote.Invoke(args);
                                });
                            }

                            processed = true;
                        }

                        if (item.ReplyToUserId != null && item.ReplyToStatusId != null)
                        { 
                            if (OnComment != null)
                            {
                                await Task.Run(() =>
                                {
                                    OnComment.Invoke(args);
                                });
                            }

                            processed = true;
                        }

                        if (item.ReplyToUserId != null && item.ReplyToStatusId == null)
                        {
                            if (OnMention != null)
                            {
                                await Task.Run(() =>
                                {
                                    OnMention.Invoke(args);
                                });
                            }

                            processed = true;
                        }

                        if (!processed)
                        {
                            if (OnTweet != null)
                            {
                                await Task.Run(() =>
                                {
                                    OnTweet.Invoke(args);
                                });
                            }
                        }
                    }

                    #region 
                    //if (Tweeted != null)
                    //{
                    //    foreach (var item in webhookEvent.TweetCreateEvents)
                    //    {
                    //        TweetCreateEventArgs args = new TweetCreateEventArgs()
                    //        {
                    //            Tweet = item
                    //        };

                    //        Tweeted.Invoke(args);
                    //    }
                    //}
                    #endregion
                }

                if (webhookEvent.LikeEvents != null)
                {
                    if (OnLike != null)
                    {
                        foreach (var item in webhookEvent.LikeEvents)
                        {
                            LikeEventArgs args = new LikeEventArgs()
                            {
                                Recipient = webhookEvent.Recipient,
                                Id = item.Id,
                                Timestamp = item.Timestamp,
                                Tweet = item.Tweet,
                                User = item.User
                            };

                            await Task.Run(() =>
                            {
                                OnLike.Invoke(args);
                            });  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(EventId, ex, ex.Message);
            }
        }

        private string CRC(string consumerSecret, string crcToken)
        {
            byte[] consumerSecretBytes = Encoding.UTF8.GetBytes(consumerSecret);
            byte[] crcTokenBytes = Encoding.UTF8.GetBytes(crcToken);

            HMACSHA256 hmacSHA256Alog = new HMACSHA256(consumerSecretBytes);

            byte[] computedHash = hmacSHA256Alog.ComputeHash(crcTokenBytes);

            return "{\n" +
                $"\"response_token\":\"sha256={Convert.ToBase64String(computedHash)}\"" +
                "\n}";
        }

        private bool VerifySignature(string signature, byte[] body)
        {
            using (var crypto = new HMACSHA256(Encoding.UTF8.GetBytes(ConsumerSecret)))
            {
                var hash = Convert.ToBase64String(crypto.ComputeHash(body));
                return hash == signature.Replace("sha256=", "");
            }
        }
    }
}
