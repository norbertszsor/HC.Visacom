using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models
{
    public class ManageAccountDto
    {
        public bool IsActivated { get; set; }
        public int RoleId { get; set; }

    }
}
