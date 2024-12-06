using System.Diagnostics.CodeAnalysis;
using BeerSender.Domain.Boxes;
using BeerSender.Domain.Boxes.Commands;

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

public class AddBeerHandlerTest : BoxTest<AddBeerBottle>
{
    protected override CommandHandler<AddBeerBottle> Handler => new AddBeerBottleHandler(eventStore);

    [Fact]
    public void IfBoxIsEmpty_ThenBottleShouldBeAdded()
    {
        Given(
            Box_created_with_capacity(6)
        );
        
        When(
            Add_beer_bottle(carte_blanche)
        );

        Then(
            Beer_bottle_added(carte_blanche)
        );
    }
    
    // Commands
    protected AddBeerBottle Add_beer_bottle(BeerBottle bottle)
        => new(Box_Id, bottle);
}
