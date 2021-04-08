using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public Product[] Product { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public DateTime Date { get; set; }
    }
}
