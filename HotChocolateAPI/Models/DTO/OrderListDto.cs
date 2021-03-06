﻿using HotChocolateAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
