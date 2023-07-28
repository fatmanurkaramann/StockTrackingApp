using Microsoft.Extensions.DependencyInjection;
using MediatR;
namespace StockTrackingApp.Application
{
    public static class ServiceRegistration
    {
        public static void AddAplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceRegistration));
        }
    }
}
