namespace BeerSender.Domain.Boxes;

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
