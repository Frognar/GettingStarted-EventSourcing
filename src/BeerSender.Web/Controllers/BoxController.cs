using BeerSender.Domain;
using BeerSender.Web.Contract;
using BeerSender.Web.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace BeerSender.Web.Controllers;

[ApiController]
[Route("api/command/box")]
#pragma warning disable CA1515
public class BoxController(CommandRouter router) : ControllerBase
#pragma warning restore CA1515
{
    private readonly CommandRouter _router = router;

    [HttpPost]
    [Route("create")]
    public IActionResult CreateBox([FromBody] CreateBoxRequest request)
    {
        _router.HandleCommand(request.ToCommand());
        return Accepted();
    }

    [HttpPost]
    [Route("add-bear")]
    public IActionResult AddBeerBottle([FromBody] AddBeerBottleRequest request)
    {
        _router.HandleCommand(request.ToCommand());
        return Accepted();
    }
}
