using ATMSystem.Core.DomainModel;

namespace ATMSystem.Core.ApplicationServices.Abstractions;

public interface IEventSubscriber
{
    Task Handle(Operation operation);
}