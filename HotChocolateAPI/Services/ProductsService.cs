using AutoMapper;
using HotChocolateAPI.Entities;
using HotChocolateAPI.Exceptions;
using HotChocolateAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateAPI.Services
{
    public interface IProductService
    {
        void AddProduct(CreateProductDto dto);
        public void AddOpinion(OpininDto dto, int idProduct);


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
        public void AddOpinion(OpininDto dto, int idProduct)
        {
            var iduser = (int)_userContextService.GetUserId;
            var orders = _context.Orders.Include(x => x.productsForOrder).FirstOrDefault(x => x.UserId == iduser && x.productsForOrder.ProductId == idProduct);
            if (orders == null)
                throw new ProductAlreadyExistException("Nie możesz dodać opinni dla tego produktu bez zakupu");
            var opinion = _context.Opinions.FirstOrDefault(x => x.UserId == iduser && x.ProductId == idProduct);
            if (opinion == null)
            {
                opinion = new Opinion();
                opinion.Date = DateTime.Now.ToShortDateString();
                opinion.UserId = iduser;
                opinion.Stars = dto.Stars;
                opinion.DescriptionOfOpinion = dto.DescriptionOfOpinion;
                _context.Opinions.Add(opinion);
                _context.SaveChanges();
            }
            else
            {
                opinion.Date = DateTime.Now.ToShortDateString();
                opinion.Stars = dto.Stars;
                opinion.DescriptionOfOpinion = dto.DescriptionOfOpinion;
                _context.SaveChanges();
            }
        }
    }
}
