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

namespace HotChocolateAPI.Services
{
    public interface IProductService
    {
        void AddProduct(CreateProductDto dto);

        public void AddOpinion(OpininDto dto, int idProduct);

        void DeleteProduct(int id);
        void UpdateProduct(int id, UpdateProductDto dto, IFormFile file);
        List<ProductsView> GetAll();
        ProductDto Get(int id);

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
            var orders = _context.Orders.Include(x => x.Products).FirstOrDefault(x => x.UserId == iduser && (null != (x.Products.FirstOrDefault(s => s.Id == idProduct))));

            if (orders == null)
                throw new ProductAlreadyExistException("Nie możesz dodać opinni dla tego produktu bez zakupu");

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
        public void UpdateProduct(int id, UpdateProductDto dto, IFormFile file)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                if (file != null)
                {
                    string[] types = { "image/jpg", "image/png", "image/jpeg" };

                    if (!(file != null && file.Length > 0 && (file.ContentType == types[0] || file.ContentType == types[1] || file.ContentType == types[2])))
                        throw new AlreadyExists("Plik nie jest w formacie JPG/JPEG/PNG lub plik jest pusty");

                    var rootPath = Directory.GetCurrentDirectory();
                    var fileName = file.FileName.Replace(" ", "_");
                    var fullPath = $"{rootPath}\\Pictures\\{fileName}";
                    if (File.Exists(fullPath))
                        throw new AlreadyExists($"To zdjęcie już istnieje na serwerze : {fileName}");


                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    };
                    product.PictureURL = fullPath;
                }


                product.Name = dto.Name;
                product.Description = dto.Description;
                product.Price = dto.Price;
                product.Amount = dto.Amount;
                if (product.Amount < 0)
                    product.Amount = 0;
                _context.SaveChanges();
            }
        }
        public List<ProductsView> GetAll()
        {
            var products = _context.Products.ToList();
            var list = _mapper.Map<List<ProductsView>>(products);
            return list;
        }
        public ProductDto Get(int id)
        {
            var products = _context.Opinions.Include(x => x.Product).Include(u=>u.User).Where(x => x.ProductId == id).ToList();

            if(products.Count == 0)
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
    }
}
