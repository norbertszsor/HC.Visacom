using HotChocolateAPI.Entities;
using HotChocolateAPI.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IBlogSerivce _blogService;
        public BlogController(IBlogSerivce blogService)
        {
            _blogService = blogService;
        }
        [HttpPost("add")]
        [Authorize]
        public IActionResult CreatePost([FromBody]Post post)
        {
            _blogService.CreatePost(post);
            return Ok();
        }
    }
}
