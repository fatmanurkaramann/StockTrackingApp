using StockTrackingApp.Application.Repositories.Product;
using StockTrackingApp.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Persistance.Repositories.Product
{
    public class ProductReadRepository :ReadRepository<Domain.Entities.Product>, IProductReadRepository
    {
        public ProductReadRepository(StockTrackingAppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
