using LinkDL.Context;
using Microsoft.EntityFrameworkCore;

namespace LinkUI
{
    public static class Migration
    {
        public static async Task InitialMigration(this WebApplication app, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("CoonectiongString to database is null or empty");

            using (var scope = app.Services.CreateScope())
            {
                var factory = scope.ServiceProvider.GetRequiredService<ILinkContextFactory>();
                var context = await factory.CreateDbContext();

                if (!await CkeckConnectiongToDatabase(context))
                    throw new Exception("Failed to connect to database after 10 attempts");

                await ExecuteMigration(context);
            }
        }

        public static async Task ExecuteMigration(ILinkContext context)
        {
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                try
                {
                    await context.Database.MigrateAsync();
                }
                catch
                {
                    throw new Exception("Failed during migration");
                }
            }
        }

        public static async Task<bool> CkeckConnectiongToDatabase(ILinkContext context)
        {
            if (string.IsNullOrEmpty(context.Database.GetDbConnection().ConnectionString))
                throw new NullReferenceException("Connectionstring is null or empty");

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    await context.Database.CanConnectAsync();
                    return true;
                }
                catch
                {
                    await Task.Delay(3000);
                }
            }

            return false;
        }
    }
}
