using ATMSystem.Core.ApplicationServices.Abstractions;
using ATMSystem.Core.DomainModel;

namespace ATMSystem.Core.ApplicationServices.Services;

public class EventPublisher : IEventPublisher
{
    private readonly List<IEventSubscriber> _handlers = new();

    public EventPublisher(IEnumerable<IEventSubscriber> subscribers)
    {
        foreach (IEventSubscriber subscriber in subscribers)
        {
            Subscribe(subscriber);
        }
    }

    public void Subscribe(IEventSubscriber handler)
    {
        _handlers.Add(handler);
    }

    public async Task Publish(Operation operation)
    {
        foreach (IEventSubscriber handler in _handlers)
        {
            await handler.Handle(operation);
        }
    }
}