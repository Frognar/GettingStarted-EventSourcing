using BeerSender.Domain.Boxes;

namespace BeerSender.Web.Contract;

#pragma warning disable CA1515
public record AddBeerBottleRequest(
    Guid BoxId,
    string Brewery,
    string Name,
    double AlcoholPercentage,
    BeerType BeerType);
#pragma warning restore CA1515
