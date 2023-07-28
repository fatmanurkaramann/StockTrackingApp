using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockTrackingApp.Domain.Entities;
using StockTrackingApp.Domain.Entities.Comman;
using StockTrackingApp.Domain.Entities.Identity;

namespace StockTrackingApp.Persistance.Contexts
{
    public class StockTrackingAppDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public StockTrackingAppDbContext(DbContextOptions options) : base(options)
        {
            //ioc containerda doldurulacak
        }

        public DbSet<Product> Products { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker : entityler üzerinden yapılan değişikliklerin ya da yeni eklenen verinin yakalanmasıını
            //sağlayan propertydir.Update operasyonlarınd Track edilen verileri yakalayıp elde etmemeizi sağlar.

            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
