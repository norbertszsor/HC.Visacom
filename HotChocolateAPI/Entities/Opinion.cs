using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Entities
{
    public class Opinion
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Stars { get; set; }
        public string DescriptionOfOpinion { get; set; }
        public string Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
