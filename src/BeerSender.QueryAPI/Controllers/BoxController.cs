using BeerSender.Domain.Boxes;
using BeerSender.QueryAPI.Database;
using Microsoft.AspNetCore.Mvc;

namespace BeerSender.QueryAPI.Controllers;

[ApiController]
[Route("[controller]")]
#pragma warning disable CA1515
public class BoxController(BoxQueryRepository repository) : ControllerBase
#pragma warning restore CA1515
{
    [HttpGet]
    [Route("{id}")]
    public Box GetById([FromRoute]Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("{id}/version/{version}")]
    public Box GetById([FromRoute] Guid id, [FromRoute]int version)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("all-open")]
    public IEnumerable<OpenBox> GetOpenBoxes()
    {
        return repository.GetAllOpen();
    }

    [HttpGet]
    [Route("all-unsent")]
    public IEnumerable<UnshippedBox> GetUnsentBoxes()
    {
        return repository.GetAllUnsent();
    }
}
