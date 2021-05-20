using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Controllers
{
    [Route("/api/blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        
        public IActionResult CreatePost()
        {
            return Ok();
        }
    }
}
