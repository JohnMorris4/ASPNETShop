using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.Cart
{
    public class RemoveFromCart
    {
        private readonly ISession _session;
        private readonly ApplicationDbContext _ctx;

        public RemoveFromCart(ISession session, ApplicationDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }
         
                 
        public class Request
             {
                 public int StockId { get; set; }
                 public int Qty { get; set; }
             }
         public async Task<bool> Do(Request request)
         {
             var cartList = new List<CartProduct>();
             var stringObject = _session.GetString("cart");
             if (string.IsNullOrEmpty(stringObject))
             {
                 return true;
             }
            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
             if (!cartList.Any(x => x.StockId == request.StockId))
             {
                 return true;
             }
             
             cartList.Find(x => x.StockId == request.StockId).Qty -= request.Qty;
             
                 stringObject = JsonConvert.SerializeObject(cartList);
                 
                 _session.SetString("cart", stringObject);

                 var stockOnHold = _ctx.StocksOnHold
                     .FirstOrDefault(x => x.StockId == request.StockId);
                 stockOnHold.Qty -= request.Qty;
                 if (stockOnHold.Qty)
                 {
                     
                 }
                 return true;
         }
    }
}