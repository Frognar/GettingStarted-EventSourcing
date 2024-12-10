using System.Data;
using BeerSender.Domain;
using Dapper;

namespace BeerSender.EventStore;

#pragma warning disable CA1724
public class EventStore (EventStoreConnectionFactory dbConnectionFactory)
#pragma warning restore CA1724
    : IEventStore
{
    private readonly EventStoreConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public IEnumerable<StoredEvent> GetEvents(Guid aggregateId)
    {
        string query =
            """
            SELECT
                  [AggregateId]
                , [SequenceNumber]
                , [Timestamp]
                , [EventTypeName]
                , [EventBody]
                , [RowVersion]
            WHERE [AggregateId] = @AggregateId
            ORDER BY [SequenceNumber]
            """;

        using IDbConnection connection = _dbConnectionFactory.Create();
        return connection.Query<DatabaseEvent>(query, new { AggregateId = aggregateId })
            .Select(e => e.ToStoredEvent());
    }

    private List<StoredEvent> _newEvents = [];
    public void AppendEvent(StoredEvent @event)
    {
        _newEvents.Add(@event);
    }

    public void SaveChanges()
    {
        throw new NotImplementedException();
    }
}
