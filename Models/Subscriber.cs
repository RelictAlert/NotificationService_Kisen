using System.ComponentModel.DataAnnotations;

namespace NotificationService_Kisen.Models
{
    public class Subscriber
    {
        [Key]
        public string SubscriberId { get; set; }
        public string SubscriberType { get; set; }
        public bool ReceiveNewAlerts { get; set; } = true;
        public ICollection<SubscriberRegion> Regions { get; set; }
    }
}
