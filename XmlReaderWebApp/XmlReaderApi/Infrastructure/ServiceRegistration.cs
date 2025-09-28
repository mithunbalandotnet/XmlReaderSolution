using XmlReader.BLL;
using XmlReader.BLL.Contract;

namespace XmlReaderApi.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<ITaxService, TaxService>();
        }
    }
}
