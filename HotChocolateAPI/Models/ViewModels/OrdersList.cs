using HotChocolateAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.ViewModels
{
    public class OrdersList
    {
        [Display(Name ="Id zamówienia")]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Address> Addresses { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }
    }
}
