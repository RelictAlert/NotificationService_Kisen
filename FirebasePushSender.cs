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
                Notification = new Notification {
                    Title = "New Alert",
                    Body = text 
                },

                Data = new Dictionary<string, string>
                {
                    [ "alertId"] = alertId.ToString() ,
                    [ "regionName"] = text.Substring(text.LastIndexOf("in ") + 3)
                }
            };
            await FirebaseMessaging.DefaultInstance.SendAsync(message);
        }

    }
}
