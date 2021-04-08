using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models
{
    public class ManageAccountDto
    {
        [Required]
        public bool IsActivated { get; set; }
    }
}
