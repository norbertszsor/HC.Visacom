﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.DTO
{
    public class AddPictureDto
    {
        [Required]
        public string PictureName { get; set; }
        [Required]
        public int ProductId { get; set; }

    }
}
