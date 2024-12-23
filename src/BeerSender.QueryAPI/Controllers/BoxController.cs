using BeerSender.Domain;
using BeerSender.Domain.Boxes;
using BeerSender.QueryAPI.Database;
using Microsoft.AspNetCore.Mvc;

namespace BeerSender.QueryAPI.Controllers;

[ApiController]
[Route("[controller]")]
#pragma warning disable CA1515
public class BoxController(
    IEventStore eventStore,
    BoxQueryRepository repository) : ControllerBase
#pragma warning restore CA1515
{
    [HttpGet]
    [Route("{id}")]
    public Box GetById([FromRoute]Guid id)
    {
        EventStream<Box> boxStream = new(eventStore, id);
        Box box = boxStream.GetEntity();
        return box;
    }

    [HttpGet]
    [Route("{id}/version/{version}")]
    public Box GetById([FromRoute] Guid id, [FromRoute]int version)
    {
        EventStream<Box> boxStream = new(eventStore, id);
        Box box = boxStream.GetEntity(version);
        return box;
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
