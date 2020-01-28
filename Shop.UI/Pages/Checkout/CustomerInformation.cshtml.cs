using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Database;

namespace Shop.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }
        
        
        public IActionResult OnGet()
        {
            //Get Cart
            var information = new GetCustomerInformation(HttpContext.Session).Do();
            if (information == null)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("Payment");
            }
            
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Post Cart
            new AddCustomerInformation(HttpContext.Session).Do(CustomerInformation);
            
            return RedirectToPage("Payment");
        }
    }
}