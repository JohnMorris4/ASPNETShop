using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Application.Infrastructure;
using Shop.Domain.Models;

namespace Shop.Application.Cart
{
    public class AddCustomerInformation
    {
        private readonly ISessionManager _sessionManager;


        public AddCustomerInformation(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }
         
                 
        public class Request
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
            [Required]
            public string Address1 { get; set; }
            
            public string Address2 { get; set; }
            [Required]
            public string City { get; set; }
            [Required]
            public string State { get; set; }
            [Required]
            [DataType(DataType.PostalCode)]
            public string Zipcode { get; set; }
        }
        public void Do(Request request)
        {
            _sessionManager.AddCustomerInformation(new CustomerInformation
             {
                 FirstName = request.FirstName,
                 LastName = request.LastName,
                 Address1 = request.Address1,
                 Address2 = request.Address2,
                 City = request.City,
                 State = request.State,
                 Zipcode = request.Zipcode,
                 Email = request.Email,
                 PhoneNumber = request.PhoneNumber
             });
        }
    }
}