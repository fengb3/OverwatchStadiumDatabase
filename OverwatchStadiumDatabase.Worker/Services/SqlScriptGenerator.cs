using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OverwatchStadiumDatabase.Worker.Services;

public interface ISqlScriptGenerator
{
    Task GenerateScriptAsync(string outputPath, CancellationToken cancellationToken);
}

public class SqlScriptGenerator(
    OverwatchStadiumDbContext dbContext,
    ILogger<SqlScriptGenerator> logger
) : ISqlScriptGenerator
{
    public async Task GenerateScriptAsync(string outputPath, CancellationToken cancellationToken)
    {
        logger.LogInformation("Generating SQL script to {OutputPath}...", outputPath);
        var sb = new StringBuilder();

        sb.AppendLine("-- Overwatch Stadium Database Full Dump");
        sb.AppendLine($"-- Generated at {DateTime.UtcNow:O}");
        sb.AppendLine(
            "-- Note: This script is designed to be compatible with SQLite, PostgreSQL, SQL Server, and MySQL (with ANSI_QUOTES enabled)."
        );
        sb.AppendLine();

        // Get all tables in dependency order
        var tables = GetTablesInDependencyOrder();

        // SQL Server specific: Allow inserting into identity columns
        sb.AppendLine("-- SQL Server: Enable Identity Insert");
        sb.AppendLine("/*");
        foreach (var table in tables)
        {
            sb.AppendLine($"SET IDENTITY_INSERT \"{table}\" ON;");
        }
        sb.AppendLine("*/");
        sb.AppendLine();

        // MySQL specific: Enable ANSI Quotes for compatibility
        sb.AppendLine("-- MySQL: Enable ANSI Quotes");
        sb.AppendLine("/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ANSI_QUOTES' */;");
        sb.AppendLine();

        // Open connection to read data
        var connection = dbContext.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);

        foreach (var table in tables)
        {
            sb.AppendLine($"-- Table: {table}");

            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM \"{table}\"";

            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
            {
                var columns = new List<string>();
                var values = new List<string>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columns.Add($"\"{reader.GetName(i)}\"");
                    values.Add(FormatValue(reader.GetValue(i)));
                }

                sb.AppendLine(
                    $"INSERT INTO \"{table}\" ({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)});"
                );
            }
            sb.AppendLine();
        }

        // SQL Server specific: Disable Identity Insert
        sb.AppendLine("-- SQL Server: Disable Identity Insert");
        sb.AppendLine("/*");
        foreach (var table in tables)
        {
            sb.AppendLine($"SET IDENTITY_INSERT \"{table}\" OFF;");
        }
        sb.AppendLine("*/");

        // MySQL specific: Restore SQL Mode
        sb.AppendLine();
        sb.AppendLine("-- MySQL: Restore SQL Mode");
        sb.AppendLine("/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;");

        await File.WriteAllTextAsync(outputPath, sb.ToString(), cancellationToken);
        logger.LogInformation("SQL script generated successfully.");
    }

    private List<string> GetTablesInDependencyOrder()
    {
        var entityTypes = dbContext.Model.GetEntityTypes();
        var sorted = new List<IEntityType>();
        var visited = new HashSet<IEntityType>();

        void Visit(IEntityType entity)
        {
            if (visited.Contains(entity))
                return;
            visited.Add(entity);

            foreach (var fk in entity.GetForeignKeys())
            {
                Visit(fk.PrincipalEntityType);
            }

            sorted.Add(entity);
        }

        foreach (var entity in entityTypes)
        {
            Visit(entity);
        }

        return sorted
            .Select(e => e.GetTableName())
            .Where(t => !string.IsNullOrEmpty(t))
            .Distinct()
            .ToList()!;
    }

    private static string FormatValue(object value)
    {
        if (value == null || value == DBNull.Value)
            return "NULL";

        if (value is bool b)
            return b ? "1" : "0";

        if (value is string s)
            return $"'{s.Replace("'", "''")}'";

        if (value is DateTime dt)
            return $"'{dt:yyyy-MM-dd HH:mm:ss.FFF}'";

        if (value is byte[] bytes)
            return $"X'{BitConverter.ToString(bytes).Replace("-", "")}'";

        return value.ToString()!;
    }
}
