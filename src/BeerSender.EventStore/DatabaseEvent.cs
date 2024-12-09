using System.Text.Json;
using BeerSender.Domain;

namespace BeerSender.EventStore;

public record DatabaseEvent
{
    public Guid AggregateId { get; init; }
    public int SequenceNumber { get; init; }
    public DateTime Timestamp { get; init; }
    public string? EventTypeName { get; init; }
    public string? EventBody { get; init; }

    public IEnumerable<byte>? RowVersion { get; init; }

    public static DatabaseEvent FromStoredEvent(StoredEvent storedEvent)
    {
        ArgumentNullException.ThrowIfNull(storedEvent);
        var typeName = storedEvent.EventData.GetType().FullName;

        if (typeName == null)
        {
            throw new InvalidOperationException("Event type name is null");
        }

        return new DatabaseEvent
        {
            AggregateId = storedEvent.AggregateId,
            SequenceNumber = storedEvent.SequenceNumber,
            Timestamp = storedEvent.Timestamp,
            EventTypeName = typeName,
            EventBody = JsonSerializer.Serialize(storedEvent.EventData)
        };
    }


    public StoredEvent ToStoredEvent()
    {
        if (EventTypeName is null)
        {
            throw new InvalidOperationException("EventTypeName should not be null");
        }

        if (EventBody is null)
        {
            throw new InvalidOperationException("EventBody should not be null");
        }
        
        Type? eventType = Type.GetType(EventTypeName);
        if (eventType is null)
        {
            throw new InvalidOperationException($"Type not found: '{EventTypeName}'");
        }
        
        object? eventData = JsonSerializer.Deserialize(EventBody, eventType);
        if (eventData is null)
        {
            throw new InvalidOperationException($"Could not deserialize EventBody as {EventTypeName}");
        }
        
        return new StoredEvent(AggregateId, SequenceNumber, Timestamp, eventData);
    }
}
