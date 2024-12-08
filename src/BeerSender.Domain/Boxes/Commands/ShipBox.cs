namespace BeerSender.Domain.Boxes.Commands;

public record ShipBox(
    Guid BoxId);

public class ShipBoxHandler(IEventStore eventStore)
    : CommandHandler<ShipBox>(eventStore)
{
    public override void Handle(ShipBox command)
    {
        ArgumentNullException.ThrowIfNull(command);
        var boxStream = GetStream<Box>(command.BoxId);
        var box = boxStream.GetEntity();
        if (box.IsShipped)
        {

            boxStream.Append(new FailedToShipBox(
                FailedToShipBox.FailReason.BoxWasAlreadyShipped));
        }
        else if (box.IsClosed)
        {
            boxStream.Append(new BoxShipped());
        }
        else
        {
            boxStream.Append(new FailedToShipBox(
                FailedToShipBox.FailReason.BoxWasNotReady));
        }
    }
}
