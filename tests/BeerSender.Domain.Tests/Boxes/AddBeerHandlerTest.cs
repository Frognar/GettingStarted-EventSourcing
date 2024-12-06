using BeerSender.Domain.Boxes;
using BeerSender.Domain.Boxes.Commands;

namespace BeerSender.Domain.Tests.Boxes;

public class AddBeerHandlerTest : CommandHandlerTest<AddBeerBottle>
{
    protected override CommandHandler<AddBeerBottle> Handler => new AddBeerBottleHandler(eventStore);

    [Fact]
    public void IfBoxIsEmpty_ThenBottleShouldBeAdded()
    {
        Given(
            new BoxCreated(new BoxCapacity(6))
        );
        
        When(
            new AddBeerBottle(_aggregateId, new BeerBottle(
                "Wolf",
                "Carte Blanche",
                0.0,
                BeerType.Triple
            ))
        );

        Then(
            new BeerBottleAdded(new BeerBottle(
                "Wolf",
                "Carte Blanche",
                0.0,
                BeerType.Triple
            ))
        );
    }
}
