using BeerSender.Domain.Boxes;

namespace BeerSender.Web.Contract;

#pragma warning disable CA1515
public record AddShippingLabelRequest(
    Guid BoxId,
    ShippingLabel Label);
#pragma warning restore CA1515
