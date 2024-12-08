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

    [Fact]
    public void IfBoxIsNotFull_ThenBottleShouldBeAdded()
    {
        Given(
            Box_created_with_capacity(2),
            Beer_bottle_added(Oatmeal_double_ipa)
        );
        
        When(
            Add_beer_bottle(carte_blanche)
        );

        Then(
            Beer_bottle_added(carte_blanche)
        );
    }

    [Fact]
    public void IfBoxIsFull_ThenShouldFailedToAddNextBottle()
    {
        Given(
            Box_created_with_capacity(1),
            Beer_bottle_added(carte_blanche)
        );
        
        When(
            Add_beer_bottle(Oatmeal_double_ipa)
        );

        Then(
            Failed_to_add_bottle_because_box_was_full()
        );
    }
    
    // Commands
    protected AddBeerBottle Add_beer_bottle(BeerBottle bottle)
        => new(Box_Id, bottle);
}
