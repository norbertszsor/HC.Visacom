using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotChocolateAPI.Entities
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        public string MainPictureAdress { get; set; }
        public List<PostParts> PostParts { get; set; }

    }
}
