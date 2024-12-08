using System.Diagnostics.CodeAnalysis;
using FluentAssertions;

namespace BeerSender.Domain.Tests;

/// <summary>
/// The base class for CommandHandler tests/
/// </summary>
/// <typeparam name="TCommand">The command type for the handler.</typeparam>
[SuppressMessage(
    "Maintainability",
    "CA1515:Consider making public types internal",
    Justification = "Used for unit tests")]
public abstract class CommandHandlerTest<TCommand>
{
    /// <summary>
    /// If no explicit aggregateId is provided, this one will be used behind the scenes.
    /// </summary>
    [SuppressMessage(
        "Design",
        "CA1051:Do not declare visible instance fields")]
    protected readonly Guid _aggregateId = Guid.NewGuid();
    
    /// <summary>
    /// The command handler, to be provided in the Test class.
    /// This to account for additional injections.
    /// </summary>
    protected abstract CommandHandler<TCommand> Handler { get; }
    
    /// <summary>
    /// A fake, in-memory event store.
    /// </summary>
    [SuppressMessage(
        "Design",
        "CA1051:Do not declare visible instance fields")]
    protected readonly TestStore eventStore = new();

    protected void Given(params object[] events)
    {
        Given(_aggregateId, events);
    }

    /// <summary>
    /// Sets a list of previous events for a specified aggregate ID.
    /// </summary>
    protected void Given(Guid aggregateId, params object[] events)
    {
        eventStore.previousEvents.AddRange(events
            .Select((e, i) => new StoredEvent(aggregateId, i, DateTime.Now, e)));
    }

    /// <summary>
    /// Triggers the handling of a command against the configured events.
    /// </summary>
    protected void When(TCommand command)
    {
        Handler.Handle(command);
    }

    /// <summary>
    /// Asserts that the expected events have been appended to the event store
    /// for the default aggregate ID.
    /// </summary>
    /// <param name="expectedEvents"></param>
    protected void Then(params object[] expectedEvents)
    {
        Then(_aggregateId, expectedEvents);
    }
    
    /// <summary>
    /// Asserts that the expected events have been appended to the event store
    /// for a specific aggregate ID.
    /// </summary>
    protected void Then(Guid aggregateId, params object[] expectedEvents)
    {
        ArgumentNullException.ThrowIfNull(expectedEvents);
        var actualEvents = eventStore.newEvents
            .Where(e => e.AggregateId == aggregateId)
            .OrderBy(e => e.SequenceNumber)
            .Select(e => e.EventData)
            .ToArray();

        actualEvents.Length.Should().Be(expectedEvents.Length);

        for (var i = 0; i < actualEvents.Length; i++)
        {
            actualEvents[i].Should().BeOfType(expectedEvents[i].GetType());
            try
            {
                actualEvents[i].Should().BeEquivalentTo(expectedEvents[i]);
            }
            catch (InvalidOperationException ex)
            {
                // Empty event with matching type is OK. This means that the event class
                // has no properties. If the types match in this situation, the correct
                // event has been appended. So we should ignore this exception.
                if (!ex.Message.StartsWith("No members were found for comparison.", StringComparison.InvariantCulture))
                    throw;
            }
        }
    }
}
