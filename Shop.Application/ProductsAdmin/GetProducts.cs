using System.Collections.Generic;
using System.Linq;
using Shop.Database;

namespace Shop.Application.ProductsAdmin
{
    public class GetProducts
    {
        private readonly ApplicationDbContext _ctx;

        public GetProducts(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<ProductViewModel> Do() => _ctx.Products.ToList().Select(x=> new ProductViewModel
           {
               Id = x.Id,
               Name = x.Name,
               Description = x.Description,
               Value = x.Value
           });
        
        public class ProductViewModel
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
                public decimal Value { get; set; }   
            }  
    }
    
    
}