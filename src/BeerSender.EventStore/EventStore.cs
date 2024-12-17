using System.Data;
using BeerSender.Domain;
using Dapper;

namespace BeerSender.EventStore;

#pragma warning disable CA1724
public class EventStore (
    EventStoreConnectionFactory dbConnectionFactory,
    INotificationService notificationService)
#pragma warning restore CA1724
    : IEventStore
{
    private readonly EventStoreConnectionFactory _dbConnectionFactory = dbConnectionFactory;
    private readonly INotificationService _notificationService = notificationService;

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
            FROM [dbo].[Events]
            WHERE [AggregateId] = @AggregateId
            ORDER BY [SequenceNumber]
            """;

        using IDbConnection connection = _dbConnectionFactory.Create();
        return connection.Query<DatabaseEvent>(query, new { AggregateId = aggregateId })
            .Select(e => e.ToStoredEvent());
    }

    private readonly List<StoredEvent> _newEvents = [];
    public void AppendEvent(StoredEvent @event)
    {
        _newEvents.Add(@event);
    }

    public void SaveChanges()
    {
        string insertCommand =
            """
            INSERT INTO [dbo].[Events]
                ([AggregateId], [SequenceNumber], [Timestamp]
                , [EventTypeName], [EventBody])
            VALUES
            (@AggregateId, @SequenceNumber, @Timestamp
              , @EventTypeName, @EventBody)
            """;

        using IDbConnection connection = _dbConnectionFactory.Create();
        connection.Open();
        using IDbTransaction transaction = connection.BeginTransaction();
        connection.Execute(
            insertCommand,
            _newEvents.Select(DatabaseEvent.FromStoredEvent),
            transaction);

        transaction.Commit();
        _newEvents.ForEach(e => _notificationService.PublishEvent(e.AggregateId, e.EventData));
        _newEvents.Clear();
    }
}
