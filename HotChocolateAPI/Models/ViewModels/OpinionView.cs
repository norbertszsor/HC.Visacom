using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Models.ViewModels
{
    public class OpinionView
    {
        public int Stars { get; set; }
        public string DescriptionOfOpinion { get; set; }
        public string Date { get; set; }
        public string UserName { get; set; }
    }
}
