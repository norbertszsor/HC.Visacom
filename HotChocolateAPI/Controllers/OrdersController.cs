using HotChocolateAPI.Models;
using HotChocolateAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HotChocolateAPI.Entities;

namespace HotChocolateAPI.Controllers
{
    [Route("/api/orders")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersSrevice;
        public OrdersController(IOrdersService OrdersService)
        {
            _ordersSrevice = OrdersService;
        }
        [HttpPost("create")]
        [Authorize]
        
        public ActionResult Create([FromBody] CreateOrderDto dto)
        {


            var list = _ordersSrevice.Create(dto);

             var id = _ordersSrevice.Create2(list);

            return Created($"/api/orders/{id}", null);
        }
        

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public ActionResult GetAllOrders()
        {
            var listOfOrders = _ordersSrevice.GetAll();

            return Ok(listOfOrders);
        }

          
    }
}

