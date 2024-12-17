using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeerSender.Web.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
#pragma warning disable CA1812
internal sealed class ErrorModel(ILogger<ErrorModel> logger) : PageModel
#pragma warning restore CA1812
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

#pragma warning disable CA1823
    private readonly ILogger<ErrorModel> _logger = logger;
#pragma warning restore CA1823

    public void OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}