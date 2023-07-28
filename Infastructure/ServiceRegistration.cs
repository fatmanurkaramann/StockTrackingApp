using Microsoft.Extensions.DependencyInjection;
using StockTrackingApp.Application.Abstraction.Token;
using StockTrackingApp.Infastructure.Services.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Infastructure
{
    public static class ServiceRegistration
    {
        public static void InfastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
