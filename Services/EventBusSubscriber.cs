using NotificationService_Kisen.Handlers;
using NotificationService_Kisen.Messaging;
using Shared.Events;

namespace NotificationService_Kisen.Services
{
    public class EventBusSubscriber : IHostedService
    {
        private readonly IEventBus _bus;
        private readonly IServiceProvider _sp;

        public EventBusSubscriber(IEventBus bus, IServiceProvider sp)
        {
            _bus = bus;
            _sp = sp;
        }

        public Task StartAsync(CancellationToken _)
        {
            _bus.Subscribe<AlertCreatedEvent>("alert.created", async evt =>
            {
                using var scope = _sp.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<AlertCreatedHandler>();
                await handler.HandleAsync(evt);
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken _) => Task.CompletedTask;
    }
}
