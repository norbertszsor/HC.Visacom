﻿using HotChocolateAPI.Models;
using HotChocolateAPI.Services;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public ActionResult AddProduct([FromBody] CreateProductDto dto)
        {
            _productService.AddProduct(dto);
            
            return Ok();
        }
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteProduct([FromRoute] int id)
        {
            _productService.DeleteProduct(id);

            return NoContent();
        }
    }
}
