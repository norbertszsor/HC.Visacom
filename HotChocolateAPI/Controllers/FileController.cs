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
        public ActionResult AddPicture([FromBody] AddPictureDto dto)
        {
            _fileService.Add(dto);

            return Ok();
        }
        [HttpGet("{id}")]
        public ActionResult GetPictures([FromRoute] int id)
        {
            var pictures = _fileService.GetPictures(id);

            return Ok(pictures);
        }

    }
}
