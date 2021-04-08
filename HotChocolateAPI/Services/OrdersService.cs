using HotChocolateAPI.Entities;
using HotChocolateAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Services
{
    public interface IOrdersService
    {
        
        CreateOrderDto Create(CreateOrderDto dto);
    }
    public class OrdersService : IOrdersService
    {
        private readonly HotChocolateDbContext _context;
        public OrdersService(HotChocolateDbContext context)
        {
            _context = context;

        }
        public CreateOrderDto Create(CreateOrderDto dto)
        {
            return 
        }

    }
}
