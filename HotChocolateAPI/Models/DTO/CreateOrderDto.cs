﻿using HotChocolateAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models
{
    public class CreateOrderDto
    {
        public Dictionary<int, int> ProductId { get; set; }
        public int AddressId { get; set; }
        
    }
}
