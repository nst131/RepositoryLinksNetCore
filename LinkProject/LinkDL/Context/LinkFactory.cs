using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LinkDL.Context
{
    public class LinkFactory : IDesignTimeDbContextFactory<LinkContext>
    {
        public LinkContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("Connection_String");

            Console.WriteLine(connectionString);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                Console.WriteLine(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../LinkUI")));

                var config = new ConfigurationBuilder()
                    .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../LinkUI")))
                    .AddJsonFile("appsettings.json", optional: true)
                    .Build();

                connectionString = config.GetConnectionString("DefaultConnection");

                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new InvalidOperationException("Connection string is not found in environment variable or appsettings.json");

                Console.WriteLine(connectionString);
            }

            var optionsBuilder = new DbContextOptionsBuilder<LinkContext>();
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 3, 0)));

            var context = new LinkContext(optionsBuilder.Options);

            if (!context.Database.CanConnect())
                throw new InvalidOperationException("Unable to connect to the database with the provided connection string");

            return context;
        }
    }
}
