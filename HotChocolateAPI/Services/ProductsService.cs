using AutoMapper;
using HotChocolateAPI.Entities;
using HotChocolateAPI.Exceptions;
using HotChocolateAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotChocolateAPI.Models.DTO;
using HotChocolateAPI.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using HotChocolateAPI.Models.Query;
using System.Linq.Expressions;

namespace HotChocolateAPI.Services
{
    public interface IProductService
    {
        void AddProduct(CreateProductDto dto);

        public void AddOpinion(OpininDto dto, int idProduct);

        void DeleteProduct(int id);
        void UpdateProduct(int id, UpdateProductDto dto);
        List<ProductsView> GetAll(ProductQuery query);
        ProductDto Get(int id);
        List<ProductsView> GetThreeLowestAmount();

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
            if (product.Amount < 0)
                product.Amount = 0;

            _context.Products.Add(product);

            _context.SaveChanges();

        }

        public void AddOpinion(OpininDto dto, int idProduct)
        {
            var iduser = (int)_userContextService.GetUserId;

            var opinion = _context.Opinions.FirstOrDefault(x => x.UserId == iduser && x.ProductId == idProduct);

            if (opinion == null)
            {
                opinion = new Opinion();
                opinion.Date = DateTime.Now.ToShortDateString();
                opinion.UserId = iduser;
                opinion.Stars = dto.Stars;
                opinion.ProductId = idProduct;
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
            if (product == null)
                throw new EmptyListException("Nie znaleziono produktu o takim id");
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Amount = dto.Amount;
            product.PictureURL = dto.PictureURL;
            if (product.Amount < 0)
                product.Amount = 0;
            _context.SaveChanges();
        }
        public List<ProductsView> GetAll(ProductQuery query)
        {
            var products = _context.Products.Where(p => query.ProductName == null || p.Name.ToLower().Contains(query.ProductName.ToLower()));

            var opinions = _context.Opinions.ToList();

            var list = _mapper.Map<List<ProductsView>>(products.ToList());

            foreach (var item in list)
            {
                var opinionsProducts = opinions.Where(x => x.ProductId == item.Id).ToList();
                if (opinionsProducts.Count == 0)
                {
                    item.Stars = 0;
                }
                else
                {
                    var stars = opinionsProducts.Select(x => x.Stars).ToList();
                    item.Stars = stars.Average();
                }
            }



            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<ProductsView, object>>>
                    {
                        { nameof(ProductsView.Name).ToLower(),r=>r.Name},
                        { nameof(ProductsView.Price).ToLower(),r=>r.Price},
                        { nameof(ProductsView.Amount).ToLower(),r=>r.Amount},
                        { nameof(ProductsView.Stars).ToLower(),r=>r.Stars}
                    };

                var selectedColumn = columnsSelector[query.SortBy];

                var test = list.AsQueryable();

                var cos = query.SortDirection == SortDirection.ASC ? test.OrderBy(selectedColumn) :
                    test.OrderByDescending(selectedColumn);
                return cos.ToList();
            }
            else
            {
                var test = list.AsQueryable();
                var cos = query.SortDirection == SortDirection.ASC ? test.OrderBy(x => x.Name) :
                                   test.OrderByDescending(x => x.Name);
                return cos.ToList();
            }

        }
        public ProductDto Get(int id)
        {
            var products = _context.Opinions.Include(x => x.Product).Include(u => u.User).Where(x => x.ProductId == id).ToList();

            if (products.Count == 0)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == id);
                if (product == null)
                    throw new BadRequestException("Produkt nie istnieje");
                return _mapper.Map<ProductDto>(product);
            }

            var prod = _mapper.Map<ProductDto>(products.First().Product);
            List<OpinionView> opinions = new List<OpinionView>();

            foreach (var item in products)
            {
                opinions.Add(_mapper.Map<OpinionView>(item));
            }
            prod.Opinions = opinions;
            return prod;
        }
        public List<ProductsView> GetThreeLowestAmount()
        {
            var products = _context.Products.Where(x => x.Amount != 0);
            if (products == null)
                throw new EmptyListException("Pusta lista produktów");
            var ebe = products.OrderBy(x => x.Amount).ToList();

            int zordon = 0;
            var toMap = new List<ProductsView>();

            foreach (var item in ebe)
            {
                if (zordon < 3)
                    toMap.Add(_mapper.Map<ProductsView>(item));
                else
                    break;
                zordon++;
            }

            var cos = toMap.Select(x => x.Id).ToList();

            var opinions = _context.Opinions.Where(x => cos.Contains(x.Id));
            if (opinions != null)
            {
                foreach (var item in toMap)
                {
                    var pom = opinions.Where(x => x.Id == item.Id).ToList();
                    if (pom.Count == 0)
                    {
                        item.Stars = 0;
                    }
                    else
                    {
                        var stars = pom.Select(x => x.Stars).ToList();
                        item.Stars = stars.Average();
                    }
                }
            }
            return toMap;
        }
    }
}