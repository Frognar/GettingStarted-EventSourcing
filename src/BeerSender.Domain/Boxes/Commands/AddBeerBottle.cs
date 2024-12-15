namespace BeerSender.Domain.Boxes.Commands;

public record AddBeerBottle(
    Guid BoxId,
    BeerBottle BeerBottle);

public class AddBeerBottleHandler(IEventStore eventStore)
    : CommandHandler<AddBeerBottle>(eventStore)
{
    public override void Handle(AddBeerBottle command)
    {
        ArgumentNullException.ThrowIfNull(command);
        EventStream<Box> boxStream = GetStream<Box>(command.BoxId);
        Box box = boxStream.GetEntity();
        if (!box.IsFull)
        {
            boxStream.Append(new BeerBottleAdded(command.BeerBottle));
        }
        else
        {
            boxStream.Append(new FailedToAddBeerBottle(
                FailedToAddBeerBottle.FailReason.BoxWasFull));
        }
    }
}