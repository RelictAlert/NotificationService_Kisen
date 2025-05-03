namespace NotificationService_Kisen.Senders
{
    public interface ITelegramSender { Task SendAsync(string chatId, int alertId, string text); }
}
