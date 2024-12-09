using BeerSender.Domain;

namespace BeerSender.EventStore;

#pragma warning disable CA1724
public class EventStore (EventStoreConnectionFactory dbConnectionFactory)
#pragma warning restore CA1724
    : IEventStore
{
#pragma warning disable CA1823
    private readonly EventStoreConnectionFactory _dbConnectionFactory = dbConnectionFactory;
#pragma warning restore CA1823

    public IEnumerable<StoredEvent> GetEvents(Guid aggregateId)
    {
        throw new NotImplementedException();
    }

    public void AppendEvent(StoredEvent @event)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        throw new NotImplementedException();
    }
}
