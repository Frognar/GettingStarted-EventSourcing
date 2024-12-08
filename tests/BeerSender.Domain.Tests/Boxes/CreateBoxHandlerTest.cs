using BeerSender.Domain.Boxes.Commands;

namespace BeerSender.Domain.Tests.Boxes;

public class CreateBoxHandlerTest : BoxTest<CreateBox>
{
    protected override CommandHandler<CreateBox> Handler => new CreateBoxHandler(eventStore);

    [Fact]
    public void IfBoxIsCreatedWithSmallDesiredNumberOfSpots_ThenSmallBoxShouldBeCreated()
    {
        Given();
        When(
            Create_box_with_desired_number_of_spots(2)
        );
        
        Then(
            Small_box_created()
        );
    }
    
    [Fact]
    public void IfBoxIsCreatedWithMediumDesiredNumberOfSpots_ThenMediumBoxShouldBeCreated()
    {
        Given();
        When(
            Create_box_with_desired_number_of_spots(10)
        );
        
        Then(
            Medium_box_created()
        );
    }
    
    [Fact]
    public void IfBoxIsCreatedWithLargeDesiredNumberOfSpots_ThenLargeBoxShouldBeCreated()
    {
        Given();
        When(
            Create_box_with_desired_number_of_spots(20)
        );
        
        Then(
            Large_box_created()
        );
    }
    
    // Commands
    protected CreateBox Create_box_with_desired_number_of_spots(int desiredNumberOfSpots) => new(Box_Id, desiredNumberOfSpots);
}
