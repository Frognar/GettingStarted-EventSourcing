using System.Diagnostics.CodeAnalysis;

namespace BeerSender.Domain;

public interface IEventStore
{
    IEnumerable<StoredEvent> GetEvents(Guid aggregateId);

    [SuppressMessage(
        "Naming",
        "CA1716:Identifiers should be correct",
        Justification = "'event' is a best fit in this context")]
    void AppendEvent(StoredEvent @event);

    void SaveChanges();
}
