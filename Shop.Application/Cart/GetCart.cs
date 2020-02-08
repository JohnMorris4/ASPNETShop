using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Infrastructure;
using Shop.Database;

namespace Shop.Application.Cart
{
    public class GetCart
    {
        private readonly ISessionManager _sessionManager;
        private readonly ApplicationDbContext _ctx;

        public GetCart(ISessionManager sessionManager, ApplicationDbContext ctx)
        {
            _sessionManager = sessionManager;
            _ctx = ctx;
        }

        
        public class Response
        {
            public string Name { get; set; }
            public int StockId { get; set; }
            public string Value { get; set; }
            public decimal RealValue { get; set; }
            public int Qty { get; set; }
        }
        public IEnumerable<Response> Do()
        {
            var cartList = _sessionManager.GetCart();

            if (cartList == null)
            {
                return new List<Response>();
            }
            var response = _ctx.Stock
                .Include(x => x.Product)
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new Response
                {
                    Name = x.Product.Name,
                    Value = $"${x.Product.Value.ToString("N2")}",
                    RealValue = x.Product.Value,
                    StockId = x.Id,
                    Qty = cartList.FirstOrDefault(y => y.StockId == x.Id).Qty
                }).ToList();
            
            return response;
        }
    }
}