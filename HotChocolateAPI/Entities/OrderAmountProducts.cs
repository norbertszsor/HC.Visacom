using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Entities
{
    public class OrderAmountProducts
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
