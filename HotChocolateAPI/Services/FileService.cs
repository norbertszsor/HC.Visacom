using AutoMapper;
using HotChocolateAPI.Entities;
using HotChocolateAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolateAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateAPI.Services
{
    public interface IFileService
    {
        void Add(AddPictureDto dto);
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
        public void Add(AddPictureDto dto)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == dto.ProductId);
            if (product == null)
                throw new EmptyListException($"Product o id:{dto.ProductId} nie istnieje");

            var picture = _mapper.Map<Pictures>(dto);

            _context.Pictures.Add(picture);
            product.Pictures.Add(picture);

            _context.SaveChanges();
            
          

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
