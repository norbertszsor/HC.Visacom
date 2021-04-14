using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolateAPI.Models;
using HotChocolateAPI.Services;
using Microsoft.AspNetCore.Authorization;
using HotChocolateAPI.Models.ViewModels;

namespace HotChocolateAPI.Controllers
{
    [Route("/api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public ActionResult RegisterUrer([FromBody]RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete([FromRoute]int id)
        {
            var IsDeleted = _accountService.Delete(id);
            if (IsDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody]LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);

            return Ok(token);
        }

        [HttpPut("manage/{id}")]

        [Authorize(Roles ="Admin")]

        public ActionResult ManageAccount([FromRoute] int id,[FromBody] ManageAccountDto dto)
        {
            _accountService.ChangeActivity(id,dto);

            return Ok();
        }
        [HttpGet("manage/getusers")]
        [Authorize(Roles ="Admin")]
        public ActionResult GetUsers()
        {
            var listOfUsers = _accountService.GetAll();


            return Ok(listOfUsers);
        }
        [HttpPatch("changepassword")]
        [Authorize]
        public ActionResult ChangePassword([FromBody] NewPasswordDto dto)
        {
            _accountService.ChangePassword(dto);
            return Ok();
        }
        [HttpGet("userdetails/{id}")]
        [Authorize]
        public ActionResult GetUser([FromRoute]int id)
        {
            var user = _accountService.GetUser(id);

            return Ok(user);
        }
    }
}
