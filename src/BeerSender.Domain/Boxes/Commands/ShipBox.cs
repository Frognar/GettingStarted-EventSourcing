namespace BeerSender.Domain.Boxes.Commands;

public record ShipBox(
    Guid BoxId);

public class ShipBoxHandler(IEventStore eventStore)
    : CommandHandler<ShipBox>(eventStore)
{
    public override void Handle(ShipBox command)
    {
        ArgumentNullException.ThrowIfNull(command);
        EventStream<Box> boxStream = GetStream<Box>(command.BoxId);
        Box box = boxStream.GetEntity();
        if (box.IsClosed == false)
        {
            boxStream.Append(new FailedToShipBox(
                FailedToShipBox.FailReason.BoxWasNotReady));
        }
        else if (box.ShippingLabel is null)
        {
            boxStream.Append(new FailedToShipBox(
                FailedToShipBox.FailReason.BoxHasNoShippingLabel));
        }
        else if (box.IsShipped)
        {

            boxStream.Append(new FailedToShipBox(
                FailedToShipBox.FailReason.BoxWasAlreadyShipped));
        }
        else
        {
            boxStream.Append(new BoxShipped());
        }
    }
}
