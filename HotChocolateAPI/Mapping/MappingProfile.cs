using AutoMapper;
using HotChocolateAPI.Entities;
using HotChocolateAPI.Models;
using HotChocolateAPI.Models.DTO;
using HotChocolateAPI.Models.ViewModels;
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
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Order, OrderView>().ReverseMap();
            CreateMap<OpinionView, Opinion>().ReverseMap().ForMember(o => o.UserName, s => s.MapFrom(x => x.User.FirstName));
            CreateMap<User, UserDetailsView>()
                .ForMember(x=>x.Address, s=>s.MapFrom(o=>o.Address));
            CreateMap<Pictures, AddPictureDto>().ReverseMap();

        }
    }
}
