using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.ViewModels
{
    public class OrderView
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public decimal TotalCost { get; set; }
    }
}
