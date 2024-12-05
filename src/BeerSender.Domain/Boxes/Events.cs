namespace BeerSender.Domain.Boxes;

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

public record FailedToAddBeerBottle(
    FailedToAddBeerBottle.FailReason Reason)
{
    public enum FailReason
    {
        BoxWasFull,
    }
}

public record BeerBottleAdded(BeerBottle BeerBottle);