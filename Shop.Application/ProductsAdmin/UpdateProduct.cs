using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Database;

namespace Shop.Application.ProductsAdmin
{
    public class UpdateProducts
    {
        private readonly ApplicationDbContext _ctx;

        public UpdateProducts(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task Do(ProductViewModel vm)
        {
            await _ctx.SaveChangesAsync();
        }
        
        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }   
        }  
    }
}