using System.Data;
using Dapper;

namespace BeerSender.Projections.Database.Repositories;

internal sealed class UnshippedBoxRepository(ReadStoreConnection dbConnection)
{
    private IDbConnection Connection => dbConnection.GetConnection();
    private IDbTransaction Transaction => dbConnection.GetTransaction();

    public void CreateOpenBox(Guid boxId)
    {
        string insertCommand =
            """
            INSERT INTO [dbo].[UnshippedBoxes]
            (BoxId, Status) VALUES (@BoxId, @Status)
            """;

        Connection.Execute(
            insertCommand,
            new { BoxId = boxId, Status = "Open" },
            Transaction);
    }

    public void CloseBox(Guid boxId)
    {
        string updateCommand =
            """
            UPDATE [dbo].[UnshippedBoxes]
            SET [Status] = @Status
            WHERE BoxId = @BoxId
            """;

        Connection.Execute(
            updateCommand,
            new { BoxId = boxId, Status = "Closed" },
            Transaction);
    }

    public void ShipBox(Guid boxId)
    {
        string deleteCommand =
            """
            DELETE FROM [dbo].[UnshippedBoxes]
            WHERE BoxId = @BoxId
            """;

        Connection.Execute(
            deleteCommand,
            new { BoxId = boxId },
            Transaction);
    }
}