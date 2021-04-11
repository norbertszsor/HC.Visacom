using HotChocolateAPI.Models;
using HotChocolateAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotChocolateAPI.Controllers
{
    [Route("/api/order")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersSrevice;
        public OrdersController(IOrdersService OrdersService)
        {
            _ordersSrevice = OrdersService;
        }
        [HttpPost("create")]
        public ActionResult Create([FromBody] CreateOrderDto dto)
        {
            

            var result = _ordersSrevice.Create(dto);

            return Ok(result);
        }
<<<<<<< HEAD
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAllOrders()
        {
            var listOfOrders = _ordersSrevice.GetAll();

            return Ok(listOfOrders);
=======
        [HttpGet("getall")]
        [Authorize(Roles = "Admin")]
        public ActionResult GetUsers()
        {
            var listOfUsers = _ordersSrevice.GetAll();


            return Ok(listOfUsers);
>>>>>>> 267c76fa9a03754dd615349d1c8c98b318c248e5
        }
    }
}
