namespace NotificationService_Kisen.Models
{
    public class SubscriberRegion
    {
        public string SubscriberId { get; set; }
        public Subscriber Subscriber { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
