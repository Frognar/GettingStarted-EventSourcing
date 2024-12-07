namespace BeerSender.Domain.Boxes.Commands;

public record AddShippingLabel(
    Guid BoxId,
    ShippingLabel Label);

public class AddShippingLabelHandler(IEventStore eventStore)
    : CommandHandler<AddShippingLabel>(eventStore)
{
    public override void Handle(AddShippingLabel command)
    {
        ArgumentNullException.ThrowIfNull(command);
        var boxStream = GetStream<Box>(command.BoxId);
        if (command.Label.IsValid())
        {
            boxStream.Append(new ShippingLabelAdded(command.Label));
        }
        else
        {
            boxStream.Append(new FailedToAddShippingLabel(
                FailedToAddShippingLabel.FailReason.TrackingCodeInvalid));
        }
    }
}