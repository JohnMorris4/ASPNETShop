using Microsoft.AspNetCore.Mvc;
using Shop.Application.Orders;
using Shop.Database;

namespace Shop.UI.Controllers
{
    
    [Route("[controller]")]
    public class OrdersController
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult GetOrders(int status) => Ok(new GetOrders(_context).Do(status));
    }
}