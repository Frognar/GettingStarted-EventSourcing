using BeerSender.Domain.Boxes;
using BeerSender.Domain.Boxes.Commands;

namespace BeerSender.Domain.Tests.Boxes;

public class AddShippingLabelHandlerTest : BoxTest<AddShippingLabel>
{
    protected override CommandHandler<AddShippingLabel> Handler => new AddShippingLabelHandler(eventStore);
    
    [Fact]
    public void IfLabelIsValid_ThenLabelShouldBeAdded()
    {
        Given(
            Box_created_with_capacity(6)
        );
        
        When(
            Add_shipping_label(Valid_FexEx_shipping_label)
        );
        
        Then(
            Shipping_label_added(Valid_FexEx_shipping_label)
        );
    }
    
    // Commands
    protected AddShippingLabel Add_shipping_label(ShippingLabel shippingLabel)
        => new(Box_Id, shippingLabel);
}
