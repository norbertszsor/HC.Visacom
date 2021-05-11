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

        public string GetPicture(string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/Pictures/{fileName}";
            var fileExists = System.IO.File.Exists(filePath);
            if (!fileExists)
                throw new PictureDoesntExistException($"Zdjęcie o nazwie {fileName} nie istnieje");

            return filePath;
        }
    }
}
