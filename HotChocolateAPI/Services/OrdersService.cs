using HotChocolateAPI.Entities;
using HotChocolateAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HotChocolateAPI.Exceptions;
using HotChocolateAPI.Models.ViewModels;

namespace HotChocolateAPI.Services
{
    public interface IOrdersService
    {

        int Create(CreateOrderDto dto);
        List<Order> GetAll();
    }

    public class OrdersService : IOrdersService
    {
        private readonly HotChocolateDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public OrdersService(HotChocolateDbContext context, IMapper mapper, IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }
        public int Create(CreateOrderDto dto)
        {

            var order = _mapper.Map<Order>(dto);    
           

            order.UserId = (int)_userContextService.GetUserId;

            _context.Orders.Add(order);
            
            _context.SaveChanges();
             
            return order.Id;
        }
        public List<Order> GetAll()
        {
            var listOfOrders = _context
                .Orders
                .Include(u => u.Address)
                
                .ToList();

            if (listOfOrders == null)
                throw new EmptyListException("No orders yet");

           
            

            return listOfOrders;

        }

    }
}
