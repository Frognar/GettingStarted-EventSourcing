namespace BeerSender.Domain;

public interface INotificationService
{
#pragma warning disable CA1716
    void PublishEvent(Guid aggregateId, object @event);
#pragma warning restore CA1716
}
