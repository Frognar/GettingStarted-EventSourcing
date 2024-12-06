using System.Diagnostics.CodeAnalysis;
using BeerSender.Domain.Boxes;

namespace BeerSender.Domain.Tests.Boxes;

[SuppressMessage(
    "Maintainability",
    "CA1515:Consider making public types internal",
    Justification = "Used for unit tests")]
public abstract class BoxTest<TCommand> : CommandHandlerTest<TCommand>
{
    protected Guid Box_Id => _aggregateId;
    
    // Events
    protected BoxCreated Box_created_with_capacity(int capacity)
        => new(new BoxCapacity(capacity));
    
    protected BeerBottleAdded Beer_bottle_added(BeerBottle bottle)
        => new(bottle);
    
    // Test data
    [SuppressMessage(
        "Design",
        "CA1051:Do not declare visible instance fields")]
    protected BeerBottle carte_blanche = new(
        "Wolf",
        "Carte Blanche",
        0.0,
        BeerType.Triple
    );
}