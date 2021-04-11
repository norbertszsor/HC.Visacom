using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models
{
    public class CreateProductDto
    {
       
        public string Name { get; set; }
        public decimal Price { get; set; }
        [MaxLength(200,ErrorMessage ="Opis może zawierać maksymalnie 200 znaków.")]
        public string Description { get; set; }
        public int? Amount { get; set; }


    }
}
