using Dapper;

namespace BeerSender.QueryAPI.Database;

#pragma warning disable CA1515
public class BoxQueryRepository(ReadStoreConnectionFactory dbFactory)
#pragma warning restore CA1515
{
    public IEnumerable<OpenBox> GetAllOpen()
    {
        var query =
            """
            SELECT BoxId ,Capacity, NumberOfBottles
            FROM dbo.OpenBoxes
            """;

        using var connection = dbFactory.Create();

        return connection.Query<OpenBox>(query);
    }
    
    public IEnumerable<UnshippedBox> GetAllUnsent()
    {
        var query =
            """
            SELECT BoxId, Status
            FROM dbo.UnshippedBoxes
            """;

        using var connection = dbFactory.Create();

        return connection.Query<UnshippedBox>(query);
    }
}

#pragma warning disable CA1515
public class UnshippedBox
#pragma warning restore CA1515
{
    public Guid BoxId { get; init; }
    public string? Status { get; init; }
}

#pragma warning disable CA1515
public class OpenBox
{
    public Guid BoxId { get; init; }
    public int Capacity { get; init; }
    public int NumberOfBottles { get; init; }
}
#pragma warning restore CA1515