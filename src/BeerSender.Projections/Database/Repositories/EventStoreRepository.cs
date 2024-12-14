using System.Data;
using System.Reflection;
using System.Text.Json;
using BeerSender.Domain;
using Dapper;

namespace BeerSender.Projections.Database.Repositories;

internal sealed class EventStoreRepository(EventStoreConnectionFactory dbFactory)
{
    public List<StoredEventWithVersion> GetEvents(
        IEnumerable<Type> types, byte[] lastVersion, int numberOfEvents)
    {
        List<string> typeNames = types.Select(t => t.FullName ?? "").ToList();

        string query =
            """
            SELECT TOP (@NumberOfEvents)
                    [AggregateId], [SequenceNumber], [Timestamp],
                    [EventTypeName], [EventBody], [RowVersion]
            FROM    dbo.[Events]
            WHERE   [EventTypeName] in @TypeNames
                    AND [RowVersion] > @LastVersion
            ORDER BY 
                    [RowVersion]
            """;

        using IDbConnection connection = dbFactory.CreateConnection();

        return connection.Query<DatabaseEvent>(
                query,
                new { TypeNames = typeNames, LastVersion = lastVersion, NumberOfEvents = numberOfEvents })
            .Select(e => e.ToStoredEventWithVersion())
            .ToList();
    }
}

internal sealed record DatabaseEvent
{
    public Guid AggregateId { get; init; }
    public int SequenceNumber { get; init; }
    public DateTime Timestamp { get; init; }
    public string? EventTypeName { get; init; }
    public string? EventBody { get; init; }
    public byte[] RowVersion { get; init; } = [];

    public StoredEventWithVersion ToStoredEventWithVersion()
    {
        if (EventTypeName == null)
        {
            throw new Exception("EventTypeName should not be null");
        }

        if (EventBody == null)
        {
            throw new Exception("EventBody should not be null");
        }

        Type? eventType = _domainAssembly.GetType(EventTypeName);
        if (eventType == null)
        {
            throw new Exception($"Type not Found: {EventTypeName}");
        }

        object? eventData = JsonSerializer.Deserialize(EventBody, eventType);
        if (eventData == null)
        {
            throw new Exception($"Could not deserialize EventBody as {EventTypeName}");
        }

        return new StoredEventWithVersion(
            AggregateId,
            SequenceNumber,
            Timestamp,
            eventData,
            RowVersion);
    }

    private static Assembly _domainAssembly = typeof(CommandRouter).Assembly;
}

internal sealed record StoredEventWithVersion(
    Guid AggregateId,
    int SequenceNumber,
    DateTime Timestamp,
    object EventData,
    byte[] RowVersion
);