﻿using AutoMapper;
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
        void Add(IFormFile file);
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
        public void Add(IFormFile file)
        {
            string[] types = { "image/jpg", "image/png", "image/jpeg" };

            if (!(file != null && file.Length > 0 && (file.ContentType==types[0] || file.ContentType==types[1]|| file.ContentType==types[2])))
                throw new AlreadyExists("Plik nie jest w formacie JPG/JPEG/PNG lub plik jest pusty");

            var rootPath = Directory.GetCurrentDirectory();
            var fileName = file.FileName.Replace(" ","_");
            var fullPath = $"{rootPath}/Pictures/{fileName}";
            if (File.Exists(fullPath))
                throw new AlreadyExists($"To zdjęcie już istnieje na serwerze : {fileName}");
            

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            };

           
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
