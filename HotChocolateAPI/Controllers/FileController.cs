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
        [Authorize(Roles = "Admin,Blogger,Warehouseman")]
        public ActionResult AddPicture([FromForm] IFormFile file)
        {
            var link = _fileService.Add(file);

            return Ok(link);

        }
        [HttpPost("addforproduct/{id}")]
        [Authorize(Roles = "Admin,Warehouseman")]
        public ActionResult AddPictureForProduct([FromForm] IFormFile file, [FromRoute]int id)
        {
            _fileService.AddForProduct(file, id);

            return Ok();

        }
        [HttpGet("{fileName}")]
        public ActionResult GetPictures([FromRoute] string fileName)
        {
            var picture = _fileService.GetPicture(fileName);

            return Ok(picture);
        }

    }
}
