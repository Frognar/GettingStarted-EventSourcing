namespace BeerSender.QueryAPI.Database;

internal static class ReadDbExtensions
{
    public static void RegisterReadDatabase(this IServiceCollection services)
    {
        services.AddSingleton<ReadStoreConnectionFactory>();

        services.AddTransient<BoxQueryRepository>();
    }
}
