using System.Diagnostics.CodeAnalysis;

namespace BeerSender.Domain.Tests;

/// <summary>
/// An in-memory Event Store for unit test purposes
/// </summary>
[SuppressMessage(
    "Maintainability",
    "CA1515:Consider making public types internal",
    Justification = "Used for unit tests")]
public class TestStore : IEventStore
{
    /// <summary>
    /// Add any events that have happened before to this collection.
    /// </summary>
    [SuppressMessage(
        "Design",
        "CA1051:Do not declare visible instance fields")]
    [SuppressMessage(
        "Design",
        "CA1002:Do not expose generic lists")]
    public readonly List<StoredEvent> previousEvents = [];
    
    /// <summary>
    /// Use this collection to verify which events have been raised
    /// </summary>
    [SuppressMessage(
        "Design",
        "CA1051:Do not declare visible instance fields")]
    [SuppressMessage(
        "Design",
        "CA1002:Do not expose generic lists")]
    public readonly List<StoredEvent> newEvents = [];

    /// <summary>
    /// Gets the events from "previousEvents" for the Aggregate ID.
    /// </summary>
    public IEnumerable<StoredEvent> GetEvents(Guid aggregateId)
    {
        return previousEvents
            .Where(e => e.AggregateId == aggregateId)
            .ToList();
    }

    /// <summary>
    /// Appends new events to the "newEvents" collection.
    /// </summary>
    public void AppendEvent(StoredEvent @event)
    {
        newEvents.Add(@event);
    }

    /// <summary>
    /// Not used in command handle tests
    /// </summary>
    public void SaveChanges()
    {
    }
}
