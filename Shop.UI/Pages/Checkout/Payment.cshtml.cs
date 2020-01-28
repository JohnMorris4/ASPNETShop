using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Shop.Application.Cart;

namespace Shop.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public PaymentModel(IConfiguration config)
        {
            PublicKey = config["Strike:PublicKey"].ToString();
        }

        public string PublicKey { get; }

        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Do();
            if (information == null)
            {
                return RedirectToPage("/Checkout/CustomerInformation");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}