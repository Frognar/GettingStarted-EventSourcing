using BeerSender.Domain.Boxes.Commands;

namespace BeerSender.Domain.Tests.Boxes;

public class ShipBoxHandlerTest : BoxTest<ShipBox>
{
    protected override CommandHandler<ShipBox> Handler => new ShipBoxHandler(eventStore);

    [Fact]
    public void IfClosedBoxIsShipped_ThenBoxShouldBeShipped()
    {
        Given(
            Box_created_with_capacity(6),
            Beer_bottle_added(carte_blanche),
            Beer_bottle_added(Oatmeal_double_ipa),
            Box_closed()
        );

        When(
            Ship_box()
        );
        
        Then(
            Box_shipped()
        );
    }
    
    // Commands
    protected ShipBox Ship_box() => new(Box_Id);
}
