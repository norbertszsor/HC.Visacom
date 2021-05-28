using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.Query
{
    public class ProductQuery
    {
        public string ProductName { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }




    }
}
