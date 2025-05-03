using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotificationService_Kisen.Data;
using NotificationService_Kisen.DTO;
using NotificationService_Kisen.Models;

namespace NotificationService_Kisen.Controllers
{
    [ApiController]
    [Route("api/subscribers")]
    public class SubscribersController : ControllerBase
    {
        private readonly NotificationDbContext _db;
        public SubscribersController(NotificationDbContext db) => _db = db;

        [HttpPost("mobile")]
        public async Task<IActionResult> SubscribeMobile([FromBody] MobileSubscribeDto dto)
        {
            var sub = await _db.Subscribers.FindAsync(dto.DeviceId);
            if (sub == null)
            {
                sub = new Subscriber
                {
                    SubscriberId = dto.DeviceId,
                    SubscriberType = "MobileApp",
                    ReceiveNewAlerts = true
                };
                _db.Subscribers.Add(sub);
            }

            _db.SubscriberRegions.RemoveRange(
                _db.SubscriberRegions.Where(sr => sr.SubscriberId == dto.DeviceId));

            foreach (var regionId in dto.RegionIds)
            {
                _db.SubscriberRegions.Add(new SubscriberRegion
                {
                    SubscriberId = dto.DeviceId,
                    RegionId = regionId
                });
            }

            await _db.SaveChangesAsync();

            return Ok(new { message = "Subscribed (mobile)", dto.RegionIds });
        }

        [HttpPost("bot")]
        public async Task<IActionResult> SubscribeBot([FromBody] BotSubscribeDto dto)
        {
            var subId = "tg-" + dto.TelegramUserId;

            var sub = await _db.Subscribers.FindAsync(subId);
            if (sub == null)
            {
                sub = new Subscriber
                {
                    SubscriberId = subId,
                    SubscriberType = "Telegram",
                    ReceiveNewAlerts = true
                };
                _db.Subscribers.Add(sub);
            }

            _db.SubscriberRegions.RemoveRange(
                _db.SubscriberRegions.Where(sr => sr.SubscriberId == subId));

            foreach (var regionId in dto.RegionIds)
            {
                _db.SubscriberRegions.Add(new SubscriberRegion
                {
                    SubscriberId = subId,
                    RegionId = regionId
                });
            }

            await _db.SaveChangesAsync();
            return Ok(new { message = "Subscribed (bot)", dto.RegionIds });
        }

        [HttpGet("{subscriberId}/regions")]
        public async Task<IActionResult> GetRegions(string subscriberId)
        {
            var regions = await _db.SubscriberRegions
                .Where(sr => sr.SubscriberId == subscriberId)
                .Select(sr => sr.RegionId)
                .ToListAsync();
            return Ok(regions);
        }

        [HttpDelete("{subscriberId}/regions")]
        public async Task<IActionResult> UnsubscribeMany(string subscriberId, [FromBody] List<int> regionIds)
        {
            if (regionIds == null || !regionIds.Any())
            {
                return NoContent();
            }

            var subscriptionsToRemove = await _db.SubscriberRegions
                .Where(x => x.SubscriberId == subscriberId && regionIds.Contains(x.RegionId))
                .ToListAsync(); 

            if (subscriptionsToRemove.Any())
            {
                _db.SubscriberRegions.RemoveRange(subscriptionsToRemove);
                await _db.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
