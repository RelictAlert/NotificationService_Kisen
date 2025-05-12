namespace NotificationService_Kisen.Senders
{
    public interface IPushSender { Task SendAsync(string deviceToken, int alertId, string text); }
}
