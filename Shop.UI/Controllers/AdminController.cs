using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.CreateProducts;
using Shop.Application.ProductsAdmin;
using Shop.Application.StockAdmin;
using Shop.Database;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Stock Actions
       [HttpGet("stocks")] 
       public IActionResult GetStocks() => Ok(new GetStock(_context).Do());
       
       [HttpPost("stocks")] 
       public async Task<IActionResult> CreateStock([FromBody] CreateStock.Request request) => Ok((await new CreateStock(_context).Do(request)));
       
       [HttpDelete("stocks/{id}")] 
       public async Task<IActionResult> DeleteStock(int id) => Ok((await new DeleteStock(_context).Do(id)));
       
       [HttpPut("stocks")] 
       public async Task<IActionResult> UpdateStock([FromBody] UpdateStock.Request request) => Ok((await new UpdateStock(_context).Do(request)));
    }
}
