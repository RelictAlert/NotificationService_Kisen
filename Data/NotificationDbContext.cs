using Microsoft.EntityFrameworkCore;
using NotificationService_Kisen.Models;

namespace NotificationService_Kisen.Data
{
    public class NotificationDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<SubscriberRegion>()
              .HasKey(sr => new { sr.SubscriberId, sr.RegionId });

            mb.Entity<SubscriberRegion>()
              .HasOne(sr => sr.Subscriber)
              .WithMany(s => s.Regions)
              .HasForeignKey(sr => sr.SubscriberId);

            mb.Entity<SubscriberRegion>()
              .HasOne(sr => sr.Region)
              .WithMany(r => r.Subscriptions)
              .HasForeignKey(sr => sr.RegionId);

            mb.Entity<Region>().HasData(
                new Region { RegionId = 1, Name = "Kyiv" },
                new Region { RegionId = 2, Name = "Kharkiv" },
                new Region { RegionId = 3, Name = "Odesa" },
                new Region { RegionId = 4, Name = "Dnipro" },
                new Region { RegionId = 5, Name = "Donetsk" },
                new Region { RegionId = 6, Name = "Lviv" },
                new Region { RegionId = 7, Name = "Zaporizhzhia" },
                new Region { RegionId = 8, Name = "Kryvyi Rih" },
                new Region { RegionId = 9, Name = "Mykolaiv" },
                new Region { RegionId = 10, Name = "Mariupol" },
                new Region { RegionId = 11, Name = "Luhansk" },
                new Region { RegionId = 12, Name = "Vinnytsia" },
                new Region { RegionId = 13, Name = "Sevastopol" },
                new Region { RegionId = 14, Name = "Simferopol" },
                new Region { RegionId = 15, Name = "Kherson" },
                new Region { RegionId = 16, Name = "Poltava" },
                new Region { RegionId = 17, Name = "Chernihiv" },
                new Region { RegionId = 18, Name = "Cherkasy" },
                new Region { RegionId = 19, Name = "Zhytomyr" },
                new Region { RegionId = 20, Name = "Sumy" },
                new Region { RegionId = 21, Name = "Khmelnytskyi" },
                new Region { RegionId = 22, Name = "Chernivtsi" },
                new Region { RegionId = 23, Name = "Rivne" },
                new Region { RegionId = 24, Name = "Ivano-Frankivsk" },
                new Region { RegionId = 25, Name = "Kropyvnytskyi" },
                new Region { RegionId = 26, Name = "Kamianske" },
                new Region { RegionId = 27, Name = "Lutsk" },
                new Region { RegionId = 28, Name = "Kremenchuk" },
                new Region { RegionId = 29, Name = "Bila Tserkva" },
                new Region { RegionId = 30, Name = "Melitopol" }
            );
        }
        public NotificationDbContext(DbContextOptions<NotificationDbContext> opts) : base(opts) { }

        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<SubscriberRegion> SubscriberRegions { get; set; }
    }
}
