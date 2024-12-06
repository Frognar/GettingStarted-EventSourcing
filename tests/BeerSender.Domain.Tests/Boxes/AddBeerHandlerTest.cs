using BeerSender.Domain.Boxes;
using BeerSender.Domain.Boxes.Commands;

namespace BeerSender.Domain.Tests.Boxes;

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
