using HotChocolateAPI.Entities;
using HotChocolateAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotChocolateAPI.Exceptions;

namespace HotChocolateAPI.Services
{
    public interface IOrdersService
    {

        int Create(CreateOrderDto dto);
        public List<OrderListDto> GetAll();

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
        public List<OrderListDto> GetAll()
        {
            var list = _context.Orders.ToList();

            if (list == null)
                throw new BadRequestException("Empty list of Orders");

            var result = _mapper.Map<List<OrderListDto>>(list);

            return result;
        }

    }
}
