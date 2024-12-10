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
}
