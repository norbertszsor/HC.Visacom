using HotChocolateAPI.Entities;
using HotChocolateAPI.Models.DTO;
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
        [Authorize(Roles = "Admin,Blogger")]
        public IActionResult CreatePost([FromBody] Post post)
        {
            var id = _blogService.CreatePost(post);

            return Created($"api/blog/{id}", null);
        }
        [HttpGet]

        public IActionResult GetAllPosts()
        {
            var posts = _blogService.GetAllPosts();
            return Ok(posts);
        }
        [HttpGet("{id}")]

        public IActionResult GetPostById([FromRoute] int id)
        {
            var post = _blogService.GetPostById(id);
            return Ok(post);
        }
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin,Blogger")]
        public IActionResult DeletePostById([FromRoute] int id)
        {
            _blogService.Delete(id);
            return NoContent();
        }
        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin, Blogger")]
        public IActionResult UpdateBlog([FromRoute]int id,[FromBody] UpdateBlogDto dto)
        {
            return Ok();
        }
    }
}
