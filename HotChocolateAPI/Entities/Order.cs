using HotChocolateAPI.Models.ViewModels;
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
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public string Date { get; set; }
        public int? OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal TotalCost { get; set; }
        public List<Product> Products { get; set; }
        public List<OrderAmountProducts> OrderAmountProducts { get; set; }
    }
}
