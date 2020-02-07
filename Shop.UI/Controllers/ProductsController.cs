using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.CreateProducts;
using Shop.Application.ProductsAdmin;
using Shop.Database;

namespace Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("")] 
        public IActionResult GetProducts() => Ok(new GetProducts(_context).Do());
       
        [HttpGet("{id}")] 
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_context).Do(id));
       
        [HttpPost("")] 
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.Request request) => Ok((await new CreateProduct(_context).Do(request)));
       
        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteProducts(int id) => Ok((await new DeleteProduct(_context).Do(id)));
       
        [HttpPut("")] 
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct.Request request) => Ok((await new UpdateProduct(_context).Do(request)));

    }
}