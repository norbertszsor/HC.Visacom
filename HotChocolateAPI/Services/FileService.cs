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
        bool Add(IFormFile file);
        List<string> GetPictures(int id);
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
        public bool Add(IFormFile file)
        {
            if(file !=null && file.Length > 0) { 
            var rootPath = Directory.GetCurrentDirectory();
            var fileName = file.FileName;
            var fullPath = $"{rootPath}/Pictures/{fileName}";

            using(var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            };
                return true;
            }
            return false;
        }

        public List<string> GetPictures(int id)
        {
            var pic = _context.Products.Include(x=>x.Pictures).FirstOrDefault(x => x.Id == id);
            if (pic == null)
                throw new EmptyListException($"Product o id:{id} nie istnieje");
            if (pic.Pictures == null)
                throw new EmptyListException("Ten produkt nie ma zdjęć");

            List<string> links = new List<string>();
            foreach (var item in pic.Pictures)
            {
                links.Add(item.Link);
            }
           
            return links;
        }
    }
}
