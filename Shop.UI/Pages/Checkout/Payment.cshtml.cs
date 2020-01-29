using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Shop.Application.Cart;

using Shop.Database;
using Stripe;

namespace Shop.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public string PublicKey { get; }
        private readonly ApplicationDbContext _ctx;

        public PaymentModel(IConfiguration config, ApplicationDbContext ctx)
        {
            _ctx = ctx;
            PublicKey = config["Stripe:PublicKey"].ToString();
        }

       

        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Do();
            if (information == null)
            {
                return RedirectToPage("/Checkout/CustomerInformation");
            }
            return Page();
        }

        public IActionResult OnPost(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();
            
            var CartOrder = new GetOrder(HttpContext.Session, _ctx);

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = CartOrder.GetTotalCharge(),
                Description = "Shop Purchase",
                Currency = "usd",
                Customer = customer.Id
            });
            
            
            return RedirectToPage("/Index");
        }
    }
}