using System.Diagnostics.CodeAnalysis;

namespace BeerSender.Domain.Boxes;

public class Box
{
    
}

// Commands
public record CreateBox(
    Guid BoxId,
    int DesiredNumberOfSpots);

public record AddShippingLabel(
    Guid BoxId,
    string TrackingCode,
    Carrier Carrier);

// Events
public record BoxCreated(
    int ActualNumberOfSpots);

public record ShippingLabelAdded(
    string TrackingCode,
    Carrier Carrier);

public record ShippingLabelFailedToAdd(
    ShippingLabelFailedToAdd.FailReason Reason)
{
    public enum FailReason
    {
        TrackingCodeInvalid,
    }
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