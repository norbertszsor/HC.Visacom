﻿using HotChocolateAPI.Entities;
using HotChocolateAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HotChocolateAPI.Exceptions;
using HotChocolateAPI.Models.ViewModels;
using HotChocolateAPI.Models.DTO;

namespace HotChocolateAPI.Services
{
    public interface IOrdersService
    {

        int Create(CreateOrderDto dto);
        List<Order> GetAll();
        Order GetOrder(int id);

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
            var products = _context.Products.Where(x => dto.ProductId.Contains(x.Id));

            if (products == null)
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


            decimal suma = 0;
            foreach (var item in products)
            {
                suma += item.Price;
                _context.ProductsForOrders.Add(new ProductsForOrder { OrderId = order.Id, ProductId = item.Id });
            }
            order.TotalCost = suma;

            _context.SaveChanges();

            return order.Id;
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
        public Order GetOrder(int id)
        {
            var order = _context.ProductsForOrders
                .Include(o => o.Order)
                .Include(p=>p.Product)
                .Where(x => x.OrderId == id).ToList();


            if(order == null)
                throw new EmptyListException("Te zamówienie nie istnieje");

            var mappedOrder = _mapper.Map<Order>(order);

            return mappedOrder;
        }

    }
}
