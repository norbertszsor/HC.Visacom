using AutoMapper;
using HotChocolateAPI.Entities;
using HotChocolateAPI.Models;
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
<<<<<<< HEAD
            CreateMap<User, UserList>().ReverseMap();

            CreateMap<Order, OrdersList>()

                .ForMember(o => o.Addresses, c => c.MapFrom(s => s.User.Address.Id))
                .ForMember(o=>o.Id, c=>c.MapFrom(s=>s.Id))
                .ForMember(o => o.FirstName, c => c.MapFrom(s => s.User.FirstName))
                .ForMember(o => o.LastName, c => c.MapFrom(s => s.User.LastName))
                .ReverseMap();
=======
            CreateMap<User, UserList>();
            CreateMap<Order, OrderListDto>();
>>>>>>> 267c76fa9a03754dd615349d1c8c98b318c248e5
        }
    }
}
