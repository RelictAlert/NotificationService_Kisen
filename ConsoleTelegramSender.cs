using NotificationService_Kisen.Senders;

namespace NotificationService_Kisen
{
    public class ConsoleTelegramSender : ITelegramSender
    {
        public Task SendAsync(string chatId, string text)
        {
            Console.WriteLine($"[Telegram] To: {chatId} — {text}");
            return Task.CompletedTask;
        }
    }
}
