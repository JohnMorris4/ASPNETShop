using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Models;

namespace Shop.Application.Cart
{
    public class GetCustomerInformation
    {
        
        private readonly ISession _session;
         
        public GetCustomerInformation(ISession session)
        {
            _session = session;
        }
         
                 
        public class Response
        {
            
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zipcode { get; set; }
        }
        public Response Do()
        {
           var stringObject = _session.GetString("customer-info");

           if (string.IsNullOrEmpty(stringObject))
           {
               return null;
           }
           var customerInformation  = JsonConvert.DeserializeObject<CustomerInformation>(stringObject);

           return new Response
           {
               FirstName = customerInformation.FirstName,
               LastName = customerInformation.LastName,
               Address1 = customerInformation.Address1,
               Address2 = customerInformation.Address2,
               City = customerInformation.City,
               State = customerInformation.State,
               Zipcode = customerInformation.Zipcode,
               Email = customerInformation.Email,
               PhoneNumber = customerInformation.PhoneNumber
           };



        }
    }
}