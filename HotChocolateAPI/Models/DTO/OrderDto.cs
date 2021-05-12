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
        public UserDto User { get; set; }
        public string Status { get; set; }
        public IEnumerable<CreateProductDto> Products{ get; set; }
        public Address Address { get; set; }
        public decimal TotalCost { get; set; }
        

    }
}
