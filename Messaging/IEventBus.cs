namespace NotificationService_Kisen.Messaging
{
    public interface IEventBus
    {
        void Publish<T>(T @event, string routingKey);
        void Subscribe<T>(string routingKey, Func<T, Task> handler);
    }
}
