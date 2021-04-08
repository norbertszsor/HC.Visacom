using HotChocolateAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Services
{
    public interface IOrdersService
    {

    }
    public class OrdersService : IOrdersService
    {
        private readonly HotChocolateDbContext _context;
        public OrdersService(HotChocolateDbContext context)
        {
            _context = context;

        }

    }
}
