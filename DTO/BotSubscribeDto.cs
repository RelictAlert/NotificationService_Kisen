namespace NotificationService_Kisen.DTO
{
    public class BotSubscribeDto
    {
        public long TelegramUserId { get; set; }
        public int[] RegionIds { get; set; }
    }
}
