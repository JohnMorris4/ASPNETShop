using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.StockAdmin
{
    public class GetStock
    {
        private readonly ApplicationDbContext _context;

        public GetStock(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductViewModel> Do()
        {
            var stock = _context.Products
                .Include(x => x.Stock)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Stock = x.Stock.Select(y => new StockViewModel
                    {
                        Id = y.Id,
                        Description = y.Description,
                        Qty = y.Qty
                    })
                })
                .ToList();
            return stock;

        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
        
        /*
         * Changing this to retrieve this from the database to make one call to get a list then pass this down to the UI layer
         */
        // public IEnumerable<StockViewModel> Do(int productId)
        // {
        //     var stock = _context.Stock
        //         .Where(x => x.ProductId == productId)
        //         .Select(x=> new StockViewModel
        //         {
        //          Id   = x.Id,
        //          Description = x.Description,
        //          Qty = x.Qty
        //         })
        //         .ToList();
        //     return stock;
        // } 
        
        public class StockViewModel
        {
            public int Id { get; set; }    
            public string Description { get; set; }
            public int Qty { get; set; }
            // public int ProductId { get; set; }
        }
    }
}