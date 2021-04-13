using HotChocolateAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.ViewModels
{
    public class ListOfOrdersViewModel
    {
        public List<ProductsForOrder> products { get; set; }
        public int OrderId { get; set; }
        public User[] UserDetails { get; set; }

    }
}
