using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolateAPI.Models;
using HotChocolateAPI.Services;
using Microsoft.AspNetCore.Authorization;
using HotChocolateAPI.Models.ViewModels;
using HotChocolateAPI.Models.DTO;

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
            _accountService.ManageAccount(id,dto);

            return Ok();
        }
        [HttpGet]
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
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult GetUser([FromRoute]int id)
        {
            var user = _accountService.GetUser(id);

            return Ok(user);
        }

        [HttpPut("EditProfile")]
        [Authorize]
        public ActionResult EditDetails([FromBody] UpdateDetailsDto dto)
        {
            _accountService.EditDetails(dto);

            return Ok();
        }
        [HttpGet("Details")]
        [Authorize]
        public ActionResult MyAccountDetails()
        {
            var accountDetails = _accountService.MyAccountDetails();

            return Ok(accountDetails);
        }
        [HttpPost("add")]
        [Authorize(Roles ="Admin")]
        public ActionResult CreateAccountByAdmin([FromBody] CreateAccountDto dto)
        {
            var account = _accountService.CreateAccount(dto);
            return Ok(account);
        }
       
       
    }
}
