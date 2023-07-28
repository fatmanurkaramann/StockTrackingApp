using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockTrackingApp.Application.Repositories.Product;
using StockTrackingApp.Domain.Entities.Identity;
using StockTrackingApp.Persistance.Contexts;
using StockTrackingApp.Persistance.Repositories.Product;

namespace StockTrackingApp.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<StockTrackingAppDbContext>(options => options.UseSqlServer(Configuration
                .ConnectionString));
            services.AddIdentity<AppUser, AppRole>(options=>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase= false;
                options.Password.RequireUppercase= false;
            }).AddEntityFrameworkStores<StockTrackingAppDbContext>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddAutoMapper(typeof(ServiceRegistration));
        }
    }
}
