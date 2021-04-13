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

            CreateMap<Product, CreateProductDto>().ReverseMap();

            CreateMap<ProductsForOrder, OrderDto>()
                .ForMember(x => x.Products, s => s.MapFrom(o => o.Product))
                .ForMember(x => x.UserId, s => s.MapFrom(o => o.Order.UserId));
            



        }
    }
}
