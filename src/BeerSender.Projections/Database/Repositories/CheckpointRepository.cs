using System.Data;
using Dapper;

namespace BeerSender.Projections.Database.Repositories;

internal sealed class CheckpointRepository(ReadStoreConnection dbConnection)
{
    private IDbConnection Connection => dbConnection.GetConnection();
    private IDbTransaction Transaction => dbConnection.GetTransaction();

    public byte[] GetCheckpoint(string checkpointName)
    {
        string query = 
            """
            SELECT  [EventVersion]
            FROM    [dbo].[ProjectionCheckpoints]
            WHERE   [ProjectionName] = @CheckpointName
            """;

        byte[]? result = Connection.ExecuteScalar<byte[]>(
            query, 
            new { CheckpointName = checkpointName },
            Transaction);

        if (result is null)
        {
            string createCommand =
                """
                INSERT INTO  [dbo].[ProjectionCheckpoints]
                ([ProjectionName]) 
                OUTPUT inserted.[EventVersion] 
                VALUES (@CheckpointName);
                """;
            
            result = Connection.ExecuteScalar<byte[]>(
                createCommand,
                new { CheckpointName = checkpointName },
                Transaction);

            Transaction.Commit();
        }

        if (result is null)
        {
            throw new Exception("Failed to create checkpoint");
        }

        return result;
    }

    public void SetCheckpoint(string checkpointName, byte[] checkpoint)
    {
        string updateCommand =
            """
            UPDATE  [dbo].[ProjectionCheckpoints]
            SET     [EventVersion] = @Checkpoint
            WHERE   [ProjectionName] = @CheckpointName
            """;

        Connection.Execute(
            updateCommand,
            new { CheckpointName = checkpointName, Checkpoint = checkpoint },
            Transaction);
    }
}
