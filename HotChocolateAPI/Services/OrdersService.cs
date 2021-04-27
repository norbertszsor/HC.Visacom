using HotChocolateAPI.Entities;
using HotChocolateAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HotChocolateAPI.Exceptions;
using HotChocolateAPI.Models.DTO;
using HotChocolateAPI.Models.ViewModels;


namespace HotChocolateAPI.Services
{
    public interface IOrdersService
    {

        int Create(CreateOrderDto dto);
        List<OrderView> GetAll();
        OrderDto GetOrder(int id);
        void ChangeStatusForOrder(int id, OrderStatusDto dto);

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
            decimal suma = 0;
            foreach (var item in products)
            {
                suma += item.Price;
            }
            var order = new Order()
            {
                UserId = (int)_userContextService.GetUserId,
                AddressId = dto.AddressId,
                Date = DateTime.Now.ToShortDateString(),
                OrderStatusId = 1,
                Products = products.ToList(),
                TotalCost = suma
        };

            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.Id;
        }

        public List<OrderView> GetAll() // later aligator
        {
            var listOfOrders = _context.Orders.ToList();
            var list = _mapper.Map<List<OrderView>>(listOfOrders);
            return list;

        }
        public OrderDto GetOrder(int id)
        {
            var userid = _userContextService.GetUserId;
            
            var userrole = _userContextService.User.IsInRole("Admin");

            var order = _context.Orders
                .Include(p => p.Products).Include(u => u.User).Include(a => a.Address)
                .FirstOrDefault(x => x.Id == id );

            if(order == null)
                throw new EmptyListException("Te zamówienie nie istnieje");
            if (!(order.UserId == userid || userrole))
                throw new NoAccess("Brak dostępu do zasobu");

            var orderdto = new OrderDto();
            orderdto.OrderId = id;
            orderdto.User = _mapper.Map<UserDto>(order.User);
            orderdto.Address = order.Address;
            orderdto.TotalCost = order.TotalCost;
            orderdto.Products = _mapper.Map<List<CreateProductDto>>(order.Products);

            return orderdto;
        }
        public void ChangeStatusForOrder(int id, OrderStatusDto statusDto)
        {

            var order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
                throw new EmptyListException($"Zamówienie od id:{id} nie istnieje");

            order.OrderStatusId = statusDto.StatusId;

            _context.SaveChanges();

        }

    }
}
