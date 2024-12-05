using System.Diagnostics.CodeAnalysis;

namespace BeerSender.Domain.Boxes;

public class Box : AggregateRoot
{
    public void Apply(BoxCreated @event)
    {
        ArgumentNullException.ThrowIfNull(@event);
        Capacity = @event.Capacity;
    }

    public void Apply(ShippingLabelAdded @event)
    {
        ArgumentNullException.ThrowIfNull(@event);
        ShippingLabel = @event.Label;
    }

    public BoxCapacity? Capacity { get; private set; }

    public ShippingLabel? ShippingLabel { get; private set; }
}

[SuppressMessage(
    "Naming",
    "CA1711:Identifiers should be correct",
    Justification = "FedEx is a valid carrier name")]
public enum Carrier
{
    UPS,
    FedEx,
    BPost,
}

// Commands
public record CreateBox(
    Guid BoxId,
    int DesiredNumberOfSpots);

// Events
public record BoxCreated(
    BoxCapacity Capacity);

public record ShippingLabelAdded(
    ShippingLabel Label);

public record ShippingLabelFailedToAdd(
    ShippingLabelFailedToAdd.FailReason Reason)
{
    public enum FailReason
    {
        TrackingCodeInvalid,
    }
}

public record ShippingLabel(Carrier Carrier, string TrackingCode)
{
    public bool IsValid()
    {
        return Carrier switch
        {
            Carrier.UPS => TrackingCode.StartsWith("ABC", StringComparison.Ordinal),
            Carrier.FedEx => TrackingCode.StartsWith("DEF", StringComparison.Ordinal),
            Carrier.BPost => TrackingCode.StartsWith("GHI", StringComparison.Ordinal),
            _ => throw new ArgumentOutOfRangeException(nameof(Carrier), Carrier, null)
        };
    }
}

public record BoxCapacity(int NumberOfSpots)
{
    public static BoxCapacity Create(int desiredNumberOfSpots)
    {
        return desiredNumberOfSpots switch
        {
            <= 6 => new BoxCapacity(6),
            <= 12 => new BoxCapacity(12),
            _ => new BoxCapacity(24),
        };
    }
}