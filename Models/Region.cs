using System.ComponentModel.DataAnnotations;

namespace NotificationService_Kisen.Models
{
    public class Region
    {
        [Key]
        public int RegionId { get; set; }
        public string Name { get; set; }
        public ICollection<SubscriberRegion> Subscriptions { get; set; }
    }
}
