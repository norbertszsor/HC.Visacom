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

            CreateMap<User, UserList>().ReverseMap().ForMember(o=>o.Role, s=>s.MapFrom(x=>x.RoleName));
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, MyAccountDetailsView>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<ProductsForMyOrdersDto, Product>().ReverseMap().ForMember(o => o.Amount, s => s.MapFrom(x => x.Amount));

            CreateMap<OrderAmountProducts, ProductsForMyOrdersDto>().ForMember(o => o.Amount, s => s.MapFrom(x => x.Amount)).ReverseMap();
           
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<OrderView, Order>().ReverseMap().ForMember(o => o.Status, s => s.MapFrom(x => x.OrderStatus.Name));
            CreateMap<OpinionView, Opinion>().ReverseMap().ForMember(o => o.UserName, s => s.MapFrom(x => x.User.FirstName));
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<MyOrdersDto, Order>().ReverseMap().ForMember(o => o.Status, s => s.MapFrom(x => x.OrderStatus.Name));
            CreateMap<User, UserDetailsView>();
            CreateMap<Product, ProductsView>().ReverseMap();
            CreateMap<PostView, Post>().ReverseMap().ForMember(o => o.Date, s => s.MapFrom(x => x.Date.ToShortDateString()));
            

        }
    }
}
