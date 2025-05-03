using Microsoft.EntityFrameworkCore;
using NotificationService_Kisen.Data;
using NotificationService_Kisen.Senders;
using Shared.Events;

namespace NotificationService_Kisen.Handlers
{
    public class AlertCreatedHandler
    {
        private readonly NotificationDbContext _db;
        private readonly ITelegramSender _tg;
        private readonly IPushSender _push;

        public AlertCreatedHandler(NotificationDbContext db,
                                   ITelegramSender tg,
                                   IPushSender push)
        {
            _db = db;
            _tg = tg;
            _push = push;
        }

        public async Task HandleAsync(AlertCreatedEvent evt)
        {
            var region = await _db.Regions
                                 .FirstOrDefaultAsync(r => r.Name == evt.RegionName);
            if (region == null) return;

            var subs = await _db.SubscriberRegions
                                .Where(sr => sr.RegionId == region.RegionId
                                          && sr.Subscriber.ReceiveNewAlerts)
                                .Select(sr => sr.SubscriberId)
                                .ToListAsync();

            foreach (var id in subs)
            {
                if (id.StartsWith("tg-"))
                    await _tg.SendAsync(
                        chatId: id.Substring(3),
                        alertId: evt.AlertId,
                        text: evt.Summary
                    );
                else
                    await _push.SendAsync(id, evt.Summary);
            }
        }
    }
}
