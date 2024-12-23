namespace BeerSender.Domain.Boxes.Commands;

public record CreateBox(
    Guid BoxId,
    int DesiredNumberOfSpots);

public class CreateBoxHandler(IEventStore eventStore)
    : CommandHandler<CreateBox>(eventStore)
{
    public override void Handle(CreateBox command)
    {
        ArgumentNullException.ThrowIfNull(command);
        EventStream<Box> boxStream = GetStream<Box>(command.BoxId);
        BoxCapacity capacity = BoxCapacity.Create(command.DesiredNumberOfSpots);
        boxStream.Append(new BoxCreated(capacity));
    }
}