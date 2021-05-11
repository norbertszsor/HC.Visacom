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
            var result =_fileService.Add(file);
            if (result)
            {  
            return Ok();
            }
            return BadRequest();
        }
        [HttpGet("{fileName}")]
        public ActionResult GetPictures([FromRoute] string fileName)
        {
            var picture = _fileService.GetPicture(fileName);

            return Ok(picture);
        }

    }
}
