using AutoMapper;
using HotChocolateAPI.Entities;
using HotChocolateAPI.Exceptions;
using HotChocolateAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Services
{
    public interface IProductService
    {
        void AddProduct(CreateProductDto dto);
        void DeleteProduct(int id);
        void UpdateProduct(int id, UpdateProductDto dto);
    }
    public class ProductsService : IProductService
    {
        private readonly HotChocolateDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public ProductsService(HotChocolateDbContext context, IUserContextService userContextService, IMapper mapper)
        {
            _context = context;
            _userContextService = userContextService;
            _mapper = mapper;
        }
        public void AddProduct(CreateProductDto dto)
        {
            var productExist = _context.Products.FirstOrDefault(x => x.Name == dto.Name);
            if (productExist != null)
                throw new ProductAlreadyExistException("Produkt o takiej nazwie już istnieje");
            if (dto.Price < 0)
                throw new ProductAlreadyExistException("Cena musi być wieksza od 0");

            var product = _mapper.Map<Product>(dto);

            _context.Products.Add(product);

            _context.SaveChanges();

        }
        public void DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) 
                throw new ProductAlreadyExistException($"Produkt o id: {id} nie istnieje");
                
            

            var result = _context.Products.Remove(product);
         
            _context.SaveChanges();
           
        }
        public void UpdateProduct(int id, UpdateProductDto dto)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                product.Name = dto.Name;
                product.Description = dto.Description;
                product.Price = dto.Price;
                
                _context.SaveChanges();
            }
        }
    }
}
