using System.Diagnostics.CodeAnalysis;

namespace BeerSender.Domain;

public abstract class AggregateRoot
{
    [SuppressMessage(
        "Naming",
        "CA1716:Identifiers should be correct",
        Justification = "'event' is a best fit in this context")]
    [SuppressMessage(
        "Performance",
        "CA1822:Mark members as static",
        Justification = "Used for dynamic event dispatch")]
    public void Apply(object @event) {}
}
