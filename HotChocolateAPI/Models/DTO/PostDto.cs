using HotChocolateAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.DTO
{
    public class PostDto
    {
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }

        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        public string MainPictureAdress { get; set; }
       
    }
}
