namespace NotificationService_Kisen.Senders
{
    public interface IPushSender { Task SendAsync(string deviceId, string text); }
}
