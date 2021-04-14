using HotChocolateAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.ViewModels
{
    public class UserDetailsView
    {
      
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string PhoneNumber { get; set; }
        public bool IsActivated { get; set; }

        public  Address Address { get; set; }
       
    }
}
