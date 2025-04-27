using NotificationService_Kisen.Senders;

namespace NotificationService_Kisen
{
    public class ConsolePushSender : IPushSender
    {
        public Task SendAsync(string deviceId, string text)
        {
            Console.WriteLine($"[Push] To device: {deviceId} — {text}");
            return Task.CompletedTask;
        }
    }
}
