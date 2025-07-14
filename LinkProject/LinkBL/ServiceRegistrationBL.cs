using LinkBL.ModelBL.TableWithUrlCrud;
using Microsoft.Extensions.DependencyInjection;

namespace LinkBL
{
    public static class ServiceRegistrationBL
    {
        public static void AddRegistrationBL(this IServiceCollection services)
        {
            services.AddScoped<ITableWithUrlCrud, TableWithUrlCrud>();
        }
    }
}
