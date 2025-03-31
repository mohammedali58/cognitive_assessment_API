using global::Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Threading;

namespace APP.Infrastructure.Seeding
{
    public static class DbInitializer
    {
        public static void EnsureDatabaseAndMigrate(IServiceProvider services, string connectionString)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();

            const int maxRetries = 5;
            const int delayMilliseconds = 3000;

            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            var targetDatabase = builder.Database;
            builder.Database = "postgres"; // Connect to the default postgres DB

            bool dbReady = false;

            for (int attempt = 1; attempt <= maxRetries && !dbReady; attempt++)
            {
                try
                {
                    using var adminConn = new NpgsqlConnection(builder.ConnectionString);
                    adminConn.Open();

                    using (var cmd = adminConn.CreateCommand())
                    {
                        cmd.CommandText = $"SELECT 1 FROM pg_database WHERE datname = '{targetDatabase}'";
                        var exists = cmd.ExecuteScalar() != null;

                        if (!exists)
                        {
                            logger.LogInformation("🛠 Database '{Db}' does not exist. Creating it...", targetDatabase);
                            using var createCmd = adminConn.CreateCommand();
                            createCmd.CommandText = $"CREATE DATABASE \"{targetDatabase}\"";
                            createCmd.ExecuteNonQuery();
                            logger.LogInformation("✅ Database '{Db}' created successfully.", targetDatabase);
                        }
                        else
                        {
                            logger.LogInformation("✅ Database '{Db}' already exists.", targetDatabase);
                        }
                    }

                    dbReady = true; // success
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, $"⚠️ Attempt {attempt}/{maxRetries} failed to connect or verify the database.");
                    if (attempt == maxRetries)
                    {
                        logger.LogError("❌ Could not verify or create database after {Max} attempts.", maxRetries);
                        throw;
                    }
                    Thread.Sleep(delayMilliseconds);
                }
            }

            // Step 2: Apply EF Core migrations
            bool migrationDone = false;

            for (int attempt = 1; attempt <= maxRetries && !migrationDone; attempt++)
            {
                using var scope = services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    logger.LogInformation("🔄 Applying EF Core migrations...");
                    dbContext.Database.Migrate();
                    logger.LogInformation("✅ Migrations complete.");
                    migrationDone = true;
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, $"⚠️ Attempt {attempt}/{maxRetries} failed to apply migrations.");
                    if (attempt == maxRetries)
                    {
                        logger.LogError("❌ Failed to apply migrations after {Max} attempts.", maxRetries);
                        throw;
                    }
                    Thread.Sleep(delayMilliseconds);
                }
            }
        }
    }
}
