using StockTrackingApp.Application.Repositories.Product;
using StockTrackingApp.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrackingApp.Persistance.Repositories.Product
{
    public class ProductWriteRepository : WriteRepository<Domain.Entities.Product>, IProductWriteRepository
    {
        public int Example1 { 
            get
            {
                return 5;
            }
            set { }
        }
        public int MyProperty { get; set; } = 5;
        public static int Example2 => 5;
        public static readonly int Example3 = 5;
        public ProductWriteRepository(StockTrackingAppDbContext dbContext) : base(dbContext)
        {
            Example1 = 1;
            MyProperty = 9;
            Example2 = 5;
            Example3 = 7;
        }
    }
}
