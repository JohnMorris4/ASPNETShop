using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        private readonly IHostingEnvironment _env;

        public CustomerInformationModel(IHostingEnvironment env)
        {
            _env = env;
        }
        
        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }
        
        
        public IActionResult OnGet([FromServices] GetCustomerInformation getCustomerInformation)
        {
            //Get Cart
            var information = getCustomerInformation.Do();
            if (information == null)
            {
                if (_env.IsDevelopment())
                {
                    CustomerInformation = new AddCustomerInformation.Request
                    {
                        FirstName = "A",
                        LastName = "A",
                        Address1 = "A",
                        Address2 = "A",
                        City = "A",
                        State = "A",
                        Zipcode = "A",
                        Email = "aa@gg.com",
                        PhoneNumber = "11",

                    };
                }
                return Page();
            }
            else
            {
                return RedirectToPage("Payment");
            }
            
        }

        public IActionResult OnPost([FromServices] AddCustomerInformation addCustomerInformation)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Post Cart
            addCustomerInformation.Do(CustomerInformation);
            
            return RedirectToPage("Payment");
        }
    }
}