using HotChocolateAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.DTO
{
    public class MyOrdersDto
    {
        public string Status { get; set; }
        public List<ProductsForMyOrdersDto> Products { get; set; }
        public Address Address { get; set; }
        public decimal TotalCost { get; set; }
    }
}
