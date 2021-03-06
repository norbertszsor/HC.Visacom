﻿using HotChocolateAPI.Models;
using HotChocolateAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HotChocolateAPI.Entities;
using HotChocolateAPI.Models.DTO;

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
        [HttpPost("add2")]
        [Authorize]
        public ActionResult Create([FromBody] CreateOrderDto dto)
        {


            var id = _ordersSrevice.Create(dto);

            return Created($"/api/orders/{id}", null);
        }
        [HttpPost("add")]
        [Authorize]
        public ActionResult Create2([FromBody]CreateOrderDto2 dto)
        {
            var id = _ordersSrevice.Create2(dto);
            return Created($"/api/orders/{id}", null);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult GetOrderById([FromRoute] int id)
        {
            var order = _ordersSrevice.GetOrder(id);
            return Ok(order);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Warehouseman")]
        public ActionResult GetAllOrders()
        {
            var listOfOrders = _ordersSrevice.GetAll();

            return Ok(listOfOrders);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Warehouseman")]
        public ActionResult ChangeStatus([FromRoute]int id,[FromBody]OrderStatusDto dto)
        {
            _ordersSrevice.ChangeStatusForOrder(id,dto);

            return NoContent();
        }
        [HttpDelete("delete/{id}")]
        [Authorize(Roles= "Admin,Warehouseman")]
        public ActionResult DeleteOrderById([FromRoute]int id)
        {
            _ordersSrevice.Delete(id);
            return NoContent();
        }
          
    }
}

