using ATMSystem.Core.DomainModel;

namespace ATMSystem.Core.ApplicationServices.Abstractions;

public interface IEventPublisher
{
    void Subscribe(IEventSubscriber handler);

    Task Publish(Operation operation);
}