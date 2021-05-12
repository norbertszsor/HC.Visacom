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
            var products = _context.Products.Where(x => dto.ProductId.Keys.Contains(x.Id));

            if (products == null)
                throw new EmptyListException("Lista produktów jest pusta");

            foreach (var item in products)
            {
                if (item.Amount < dto.ProductId[item.Id])
                    throw new BadRequestException($"Brak danej ilości produktu {item.Name} w magazynie");
            }
            decimal suma = 0;

            var order = new Order()
            {
                UserId = (int)_userContextService.GetUserId,
                AddressId = dto.AddressId,
                Date = DateTime.Now.ToShortDateString(),
                OrderStatusId = 1,
                Products = products.ToList(),
                OrderAmountProducts = new List<OrderAmountProducts>()
            };

            foreach (var item in products)
            {
                item.Amount -= dto.ProductId[item.Id];
                suma += dto.ProductId[item.Id] * item.Price;
                order.OrderAmountProducts.Add(new OrderAmountProducts
                {
                    ProductId = item.Id,
                    Amount = dto.ProductId[item.Id]
                });
            }

            
            order.TotalCost = suma;
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.Id;
        }

        public List<OrderView> GetAll() // later aligator
        {
            var listOfOrders = _context.Orders.Include(x => x.OrderStatus).ToList();
            var list = _mapper.Map<List<OrderView>>(listOfOrders);
            return list;

        }
        public OrderDto GetOrder(int id)
        {
            var userid = _userContextService.GetUserId;
            
            var userrole = _userContextService.User.IsInRole("Admin");

            var order = _context.Orders
                .Include(p => p.Products).Include(u => u.User).Include(a => a.Address).Include(x => x.OrderAmountProducts).Include(x=>x.OrderStatus)
                .FirstOrDefault(x => x.Id == id );

            if (!(order.UserId == userid || userrole))
                throw new NoAccess("Brak dostępu do zasobu");
            if (order == null)
                throw new EmptyListException("Te zamówienie nie istnieje");
            

            var orderdto = new OrderDto();
            orderdto.OrderId = id;
            orderdto.User = _mapper.Map<UserDto>(order.User);
            orderdto.Address = order.Address;
            orderdto.TotalCost = order.TotalCost;
            var ditios = new List<CreateProductDto>();
            foreach (var item in order.Products)
            {
                var tmp = _mapper.Map<CreateProductDto>(item);
                tmp.Amount = order.OrderAmountProducts.FirstOrDefault(x => x.ProductId == item.Id).Amount;
                ditios.Add(tmp);
            }
            orderdto.Products = ditios;
            orderdto.Status = order.OrderStatus.Name;
            return orderdto;
        }
        public void ChangeStatusForOrder(int id, OrderStatusDto statusDto)
        {

            var order = _context.Orders
                .Include(x=>x.OrderAmountProducts)
                .Include(x=>x.Products)
                .FirstOrDefault(x => x.Id == id);

            if (order == null)
                throw new EmptyListException($"Zamówienie o id:{id} nie istnieje");
            if (order.OrderStatusId == 5)
                throw new BadRequestException("Nie można zmienić statusu zamówienia");
            if(statusDto.StatusId==5)
            {
                foreach (var item in order.Products)
                {
                    item.Amount += order.OrderAmountProducts.FirstOrDefault(x => x.ProductId==item.Id).Amount;
                }
            }
            order.OrderStatusId = statusDto.StatusId;

            _context.SaveChanges();

        }

    }
}
