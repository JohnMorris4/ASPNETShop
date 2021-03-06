using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Infrastructure;
using Shop.Database;

namespace Shop.Application.Cart
{
    public class GetOrder
    {
        private readonly ISessionManager _sessionManager;
        private readonly ApplicationDbContext _ctx;

        public GetOrder(ISessionManager sessionManager, ApplicationDbContext ctx)
        { 
            _sessionManager = sessionManager;
            _ctx = ctx;
        }

        public class Response
        {
            public IEnumerable<Product> Products { get; set; }
            public CustomerInformation CustomerInformation { get; set; }
            public int GetTotalCharge() => Products.Sum(x => x.Value * x.Qty);
        }



        public class Product
        {
            
            public int StockId { get; set; }
            public int ProductId { get; set; }
            public int Qty { get; set; }
            public int Value { get; set; }
            
        }
        public class CustomerInformation
        {
            
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zipcode { get; set; }
        }
        
        
        public Response Do()
        {
            var cart = _sessionManager.GetCart();

            var listOfProducts = _ctx.Stock
                .Include(x => x.Product)
                .Where(x => cart.Any(y => y.StockId == x.Id))
                .Select(x => new Product
                {
                    ProductId = x.ProductId,
                    StockId = x.Id,
                    Value = (int)(x.Product.Value * 100),
                    Qty = cart.FirstOrDefault(y => y.StockId == x.Id).Qty
                }).ToList();
            
            var customerInformation = _sessionManager.GetCustomerInformation();
          
            
            return new Response
            {
                Products = listOfProducts,
                CustomerInformation = new CustomerInformation
                {
                    FirstName = customerInformation.FirstName,
                    LastName = customerInformation.LastName,
                    Address1 = customerInformation.Address1,
                    Address2 = customerInformation.Address2,
                    City = customerInformation.City,
                    State = customerInformation.State,
                    Zipcode = customerInformation.Zipcode,
                    Email = customerInformation.Email,
                    PhoneNumber = customerInformation.PhoneNumber
                }
            };
        }
    }
}