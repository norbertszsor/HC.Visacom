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

        List<int> Create(CreateOrderDto dto);
        List<Order> GetAll();
        void Create2(List<int> list);
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
        public List<int> Create(CreateOrderDto dto)
        {
            if (dto.ProductId == null)
                throw new EmptyListException("Lista produktów jest pusta");

            var order = new Order()
            {
                
                UserId = (int)_userContextService.GetUserId,
                AddressId = dto.AddressId,
                Date = DateTime.Now.ToShortDateString(),
                Status = "W trakcie Realizacji",

            };

            _context.Orders.Add(order);

            _context.SaveChanges();

            var list = dto.ProductId.ToList();

            list.Add(order.Id);

            return list;
        }

        public void Create2(List<int> list)
        {
            var orderId = list.Last();
            list.RemoveAt(list.Count() - 1);
            foreach (var item in list)
            {
                _context.ProductsForOrders.Add(new ProductsForOrder { OrderId = orderId, ProductId = item });
            }
            _context.SaveChanges();
        }


        public List<Order> GetAll() // later aligator
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
