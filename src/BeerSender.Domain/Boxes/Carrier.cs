using System.Diagnostics.CodeAnalysis;

namespace BeerSender.Domain.Boxes;

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
