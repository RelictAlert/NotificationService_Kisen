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
                new Region { RegionId = 1, Name = "Київ" },
                new Region { RegionId = 2, Name = "Харків" },
                new Region { RegionId = 3, Name = "Одеса" },
                new Region { RegionId = 4, Name = "Дніпро" },
                new Region { RegionId = 5, Name = "Донецьк" },
                new Region { RegionId = 6, Name = "Львів" },
                new Region { RegionId = 7, Name = "Запоріжжя" },
                new Region { RegionId = 8, Name = "Кривий Ріг" },
                new Region { RegionId = 9, Name = "Миколаїв" },
                new Region { RegionId = 10, Name = "Маріуполь" },
                new Region { RegionId = 11, Name = "Луганськ" },
                new Region { RegionId = 12, Name = "Вінниця" },
                new Region { RegionId = 13, Name = "Севастополь" },
                new Region { RegionId = 14, Name = "Сімферополь" },
                new Region { RegionId = 15, Name = "Херсон" },
                new Region { RegionId = 16, Name = "Полтава" },
                new Region { RegionId = 17, Name = "Чернігів" },
                new Region { RegionId = 18, Name = "Черкаси" },
                new Region { RegionId = 19, Name = "Житомир" },
                new Region { RegionId = 20, Name = "Суми" },
                new Region { RegionId = 21, Name = "Хмельницький" },
                new Region { RegionId = 22, Name = "Чернівці" },
                new Region { RegionId = 23, Name = "Рівне" },
                new Region { RegionId = 24, Name = "Івано-Франківськ" },
                new Region { RegionId = 25, Name = "Кропивницький" },
                new Region { RegionId = 26, Name = "Кам'янське" },
                new Region { RegionId = 27, Name = "Луцьк" },
                new Region { RegionId = 28, Name = "Кременчук" },
                new Region { RegionId = 29, Name = "Біла Церква" },
                new Region { RegionId = 30, Name = "Мелітополь" }
            );
        }
        public NotificationDbContext(DbContextOptions<NotificationDbContext> opts) : base(opts) { }

        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<SubscriberRegion> SubscriberRegions { get; set; }
    }
}
