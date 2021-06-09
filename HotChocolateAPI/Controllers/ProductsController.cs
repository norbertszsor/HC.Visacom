using HotChocolateAPI.Models;
using HotChocolateAPI.Models.Query;
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
    [ApiController]
    [Route("/api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("add")]
        [Authorize(Roles = "Admin,Warehouseman")]
        public ActionResult AddProduct([FromBody] CreateProductDto dto)
        {
            _productService.AddProduct(dto);

            return Ok();
        }
      
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin,Warehouseman")]
        public ActionResult DeleteProduct([FromRoute] int id)
        {
            _productService.DeleteProduct(id);

            return NoContent();
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Warehouseman")]
        public ActionResult UpdateProduct([FromRoute] int id, [FromBody]UpdateProductDto dto)
        {
            _productService.UpdateProduct(id, dto);

            return NoContent();
        }
        [HttpGet]
        public ActionResult GetAll([FromQuery]ProductQuery query)
        {
            var list = _productService.GetAll(query);

            return Ok(list);
        }
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] int id)
        {
            var product = _productService.Get(id);

            return Ok(product);
        }

        [HttpPost("addopinion/{id}")] 
        [Authorize]
        public ActionResult AddOpinion([FromBody] OpininDto dto, [FromRoute] int id)
        {
            _productService.AddOpinion(dto, id);
            return Ok();
        }
        [HttpGet("norbeczka")]
        public ActionResult TakeThreeProductsWithLowestAmount()
        {

            var prod = _productService.GetThreeLowestAmount();
            return Ok(prod);
        }
    }
}
