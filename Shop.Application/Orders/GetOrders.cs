using System.Collections.Generic;
using Shop.Application.StockAdmin;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.Orders
{
    public class GetOrders
    {
        private readonly ApplicationDbContext _ctx;

        public GetOrders(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public class Response
        {
            public string OrderRef { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zipcode { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

            public IEnumerable<Product> Products { get; set; }   
        }


        public Response Do(int status)
        {
            return status.ToString("N2");
        }
    }
}