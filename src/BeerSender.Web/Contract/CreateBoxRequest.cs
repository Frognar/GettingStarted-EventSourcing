namespace BeerSender.Web.Contract;

#pragma warning disable CA1515
public record CreateBoxRequest(
    Guid BoxId,
    int DesiredNumberOfSpots);
#pragma warning restore CA1515
