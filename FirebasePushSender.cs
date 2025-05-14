using FirebaseAdmin.Messaging;
using NotificationService_Kisen.Senders;

namespace NotificationService_Kisen
{
    public class FirebasePushSender : IPushSender
    {
        public async Task SendAsync(string deviceToken, int alertId, string text)
        {
            var message = new Message
            {
                Token = deviceToken,
                Android = new AndroidConfig
                {
                    Priority = Priority.High
                },
                Data = new Dictionary<string, string>
                {
                    ["title"] = "Kisen Alert",
                    ["body"] = text,
                    ["alertId"] = alertId.ToString(),
                    ["regionName"] = text.Substring(text.LastIndexOf("in ") + 3)
                }
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);
        }
    }

}
