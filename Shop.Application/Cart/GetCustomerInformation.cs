using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Shop.Application.Cart
{
    public class GetCustomerInformation
    {
        
        private readonly ISession _session;
         
        public GetCustomerInformation(ISession session)
        {
            _session = session;
        }
         
                 
        public class Request
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
        public Request Do()
        {
           var stringObject = _session.GetString("customer-info");

           if (string.IsNullOrEmpty(stringObject))
           {
               return null;
           }
           var response  = JsonConvert.DeserializeObject<Request>(stringObject);

           return response;



        }
    }
}