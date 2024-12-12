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
}
