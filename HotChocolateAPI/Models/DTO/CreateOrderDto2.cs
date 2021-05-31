using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.DTO
{
    public class CreateOrderDto2
    {
        public List<ProductForOrderDto> Product { get; set; }
        public int AddressId { get; set; }

    }
}
