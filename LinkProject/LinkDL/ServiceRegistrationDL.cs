using LinkDL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static LinkDL.Context.LinkContext;

namespace LinkDL
{
    public static class ServiceRegistrationDL
    {
        public static void AddRegistrationDL(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextFactory<LinkContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 3, 0))));
            services.AddScoped<ILinkContextFactory, LinkContextFactory>();
        }
    }
}
