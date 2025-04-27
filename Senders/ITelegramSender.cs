namespace NotificationService_Kisen.Senders
{
    public interface ITelegramSender { Task SendAsync(string chatId, string text); }
}
