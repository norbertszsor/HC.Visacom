﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public int? AddressId { get; set; }
        public bool IsActivated { get; set; }

        public virtual Address Address { get; set; }
        public virtual Role Role { get; set; }
        
    }
}
