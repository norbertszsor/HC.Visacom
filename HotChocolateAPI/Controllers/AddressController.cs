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
    [Route("/api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpPost("add")]
        [Authorize]
        public ActionResult AddAddress([FromBody] AddressDto dto)
        {
            _addressService.AddAddress(dto);
            return Ok();
        }
        [HttpGet("getmyaddresses")]
        [Authorize]
        public ActionResult GetAddresses()
        {
            var addresses = _addressService.GetMyAddresses();
            return Ok(addresses);
        }
        
    }
}
