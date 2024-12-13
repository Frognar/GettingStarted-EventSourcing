using BeerSender.Domain.Boxes;
using BeerSender.Domain.Boxes.Commands;
using BeerSender.Web.Contract;

namespace BeerSender.Web.Mappers;

internal static class BoxMapper
{
    public static CreateBox ToCommand(this CreateBoxRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return new CreateBox(request.BoxId, request.DesiredNumberOfSpots);
    }

    public static AddBeerBottle ToCommand(this AddBeerBottleRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return new AddBeerBottle(
            request.BoxId,
            new BeerBottle(
                request.Brewery,
                request.Name,
                request.AlcoholPercentage,
                request.BeerType));
    }

    public static AddShippingLabel ToCommand(this AddShippingLabelRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return new AddShippingLabel(request.BoxId, request.Label);
    }

    public static CloseBox ToCommand(this CloseBoxRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return new CloseBox(request.BoxId);
    }

    public static ShipBox ToCommand(this ShipBoxRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return new ShipBox(request.BoxId);
    }
}
