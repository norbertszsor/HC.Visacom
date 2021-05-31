using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.DTO
{
    public class ProductsForMyOrdersDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int? Amount { get; set; }


        public string PictureURL { get; set; }
    }
}
