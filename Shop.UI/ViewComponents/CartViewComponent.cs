using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Shop.Application.Cart;
using Shop.Database;

namespace Shop.UI.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CartViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string view = "Default")
        {
            return View(view,new GetCart(HttpContext.Session, _context).Do());
        }
    }
}