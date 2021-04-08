using HotChocolateAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Create([FromBody] CreateOrderDTO dto)
        {


            var id = restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }
    }
}
