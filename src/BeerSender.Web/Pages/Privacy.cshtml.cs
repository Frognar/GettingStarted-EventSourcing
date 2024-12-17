using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeerSender.Web.Pages;

#pragma warning disable CA1812
internal sealed class PrivacyModel : PageModel
#pragma warning restore CA1812
{
    private readonly ILogger<PrivacyModel> _logger;

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

#pragma warning disable CA1822
    public void OnGet()
#pragma warning restore CA1822
    {
    }
}