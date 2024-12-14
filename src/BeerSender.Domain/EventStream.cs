using System.Diagnostics.CodeAnalysis;

namespace BeerSender.Domain;

[SuppressMessage(
    "Naming",
    "CA1711:Identifiers should be correct",
    Justification = "EventStream is a best fit in this context")]
public class EventStream<TEntity>(IEventStore eventStore, Guid aggregateId)
    where TEntity : AggregateRoot, new()
{
    private int _lastSequenceNumber;
    public TEntity GetEntity(int sequenceNumber = int.MaxValue)
    {
        var events = eventStore.GetEvents(aggregateId);
        TEntity entity = new();
        foreach (StoredEvent @event in events.TakeWhile(e => e.SequenceNumber <= sequenceNumber))
        {
            entity.Apply((dynamic)@event.EventData);
            _lastSequenceNumber = @event.SequenceNumber;
        }

        return entity;
    }

    public void Append(object @event)
    {
        _lastSequenceNumber++;
        StoredEvent storedEvent = new(
            aggregateId,
            _lastSequenceNumber,
            DateTime.UtcNow,
            @event);
        
        eventStore.AppendEvent(storedEvent);
    }
}
