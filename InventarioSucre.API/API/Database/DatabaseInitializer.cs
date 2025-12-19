using Microsoft.Data.SqlClient;

namespace API.Database;

public static class DatabaseInitializer
{
    public static void Initialize(IConfiguration configuration)
    {
        var masterConn = configuration.GetConnectionString("MasterConnection");
        var inventarioConn = configuration.GetConnectionString("InventarioConnection");

        ExecuteScript(masterConn!, "Database/01_CreateDatabase.sql");
        ExecuteScript(inventarioConn!, "Database/02_InitSchema.sql");
    }

    private static void ExecuteScript(string connectionString, string scriptPath)
    {
        var script = File.ReadAllText(scriptPath);

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var batches = script.Split(new[] { "\nGO", "\r\nGO" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var batch in batches)
        {
            using var command = new SqlCommand(batch, connection);
            command.ExecuteNonQuery();
        }
    }
}