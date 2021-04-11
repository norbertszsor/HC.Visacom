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
            CreateMap<User, UserList>().ReverseMap();

            CreateMap<Order, OrdersList>()

                .ForMember(o => o.Addresses, c => c.MapFrom(s => s.User.Address.Id))
                .ForMember(o=>o.Id, c=>c.MapFrom(s=>s.Id))
                .ForMember(o => o.FirstName, c => c.MapFrom(s => s.User.FirstName))
                .ForMember(o => o.LastName, c => c.MapFrom(s => s.User.LastName))
                .ReverseMap();
        }
    }
}
