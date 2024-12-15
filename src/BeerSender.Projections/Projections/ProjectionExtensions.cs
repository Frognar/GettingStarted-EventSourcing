using Microsoft.Extensions.DependencyInjection;

namespace BeerSender.Projections.Projections;

internal static class ProjectionExtensions
{
    public static void RegisterProjections(this IServiceCollection services)
    {
        services.AddTransient<OpenBoxProjection>();
        services.AddHostedService<ProjectionService<OpenBoxProjection>>();
    }
}
