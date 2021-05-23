using AutoMapper;
using HotChocolateAPI.Entities;
using HotChocolateAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolateAPI.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace HotChocolateAPI.Services
{
    public interface IFileService
    {
        void AddForProduct(AddPictureDto dto);
        string GetPicture(string fileName);
    }
    public class FileService : IFileService
    {
        private readonly HotChocolateDbContext _context;
        private readonly IMapper _mapper;

        public FileService(HotChocolateDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public string GetPicture(string fileName)
        {
            var rootPath = "http://visacomhotchocolate.cba.pl/images/";
            var filePath = $"{rootPath}{fileName}.jpg";
     
            return filePath;
        }
        void IFileService.AddForProduct(AddPictureDto dto)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == dto.ProductId);
            if (product == null)
                throw new EmptyListException("Produkt nie istnieje");

            product.PictureURL = $"http://visacomhotchocolate.cba.pl/images/{dto.PictureName}.jpg";
            _context.SaveChanges();
        }
    }
}


