using BeerSender.Domain.Boxes.Commands;

namespace BeerSender.Domain.Tests.Boxes;

public class CloseBoxHandlerTests : BoxTest<CloseBox>
{
    protected override CommandHandler<CloseBox> Handler => new CloseBoxHandler(eventStore);
    
    [Fact]
    public void IfBoxIsNotEmpty_ThenShouldCloseBox()
    {
        Given(
            Box_created_with_capacity(6),
            Beer_bottle_added(carte_blanche)
        );
        
        When(
            Close_box()
        );
        
        Then(
            Box_closed()
        );
    }
    
    // Commands
    protected CloseBox Close_box() => new(Box_Id);
}
