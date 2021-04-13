using AutoMapper;
using HotChocolateAPI.Entities;
using HotChocolateAPI.Models;
using HotChocolateAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<User, UserList>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();

        }
    }
}
