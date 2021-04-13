using HotChocolateAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public IEnumerable<Product> Products{ get; set; }


    }
}
