using HotChocolateAPI.Models.DTO;
using HotChocolateAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Controllers
{
    [Route("api/pictures")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public ActionResult AddPicture([FromForm] IFormFile file)
        {
            var  result =_fileService.Add(file);
            if (result)
            {  
            return Ok();
            }
            return BadRequest();
        }
        [HttpGet("{id}")]
        public ActionResult GetPictures([FromRoute] int id)
        {
            var pictures = _fileService.GetPictures(id);

            return Ok(pictures);
        }

    }
}
