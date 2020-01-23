using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Database;

namespace Shop.Application.ProductsAdmin
{
    public class DeleteProducts
    {
        private readonly ApplicationDbContext _ctx;

        public DeleteProducts(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> Do(int id)
        {
            var Product = _ctx.Products.FirstOrDefault(x => x.Id == id);
            _ctx.Products.Remove(Product);
            await _ctx.SaveChangesAsync();
            return true;
        }
        
        
    }
}