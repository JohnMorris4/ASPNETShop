using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.OrdersAdmin;
using System.Threading.Tasks;
using Shop.Application.Orders;
using Shop.Database;
using GetOrder = Shop.Application.OrdersAdmin;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class OrdersController : Controller
    {
        // private readonly ApplicationDbContext _ctx;
        //
        // public OrdersController(ApplicationDbContext ctx)
        // {
        //     _ctx = ctx;
        //     
        // }

        [HttpGet("")]
        public IActionResult GetOrders(int status, [FromServices] GetOrders getOrders) => Ok(getOrders.Do(status));

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id, [FromServices] GetOrder.GetOrder getOrder) => Ok(getOrder.Do(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id,
        [FromServices] UpdateOrder updateOrder) => Ok((await updateOrder.Do(id)));
    }
}