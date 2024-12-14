using System.Data;
using Microsoft.Data.SqlClient;

namespace BeerSender.QueryAPI.Database;

#pragma warning disable CA1515
public class ReadStoreConnectionFactory(IConfiguration configuration)
#pragma warning restore CA1515
{
    private readonly string? _connectionString 
        = configuration.GetConnectionString("ReadStore");

    public IDbConnection Create() 
        => new SqlConnection(_connectionString);
}
