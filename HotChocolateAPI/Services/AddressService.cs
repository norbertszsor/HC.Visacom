using AutoMapper;
using HotChocolateAPI.Entities;
using HotChocolateAPI.Exceptions;
using HotChocolateAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Services
{
    public interface IAddressService
    {
        public void AddAddress(AddressDto dto);
        public List<Address> GetMyAddresses();
    }
    public class AddressService : IAddressService
    {
        private readonly HotChocolateDbContext _context;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;


        public AddressService(HotChocolateDbContext context, AuthenticationSettings authenticationSettings, IMapper mapper, IUserContextService userContextService)
        {
            _context = context;
            _authenticationSettings = authenticationSettings;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public void AddAddress(AddressDto dto)
        {
            var userId = _userContextService.GetUserId;
            var address = new Address()
            {
                Town = dto.Town,
                Street = dto.Street,
                HouseNumber = dto.HouseNumber,
                PostalCode = dto.PostalCode
            };
            var user = _context.Users.Include(s => s.Address).FirstOrDefault(x => x.Id == userId);
            _context.Addresses.Add(address);
            user.Address.Add(address);
            _context.SaveChanges();

        }
        public List<Address> GetMyAddresses()
        {
            var userId = _userContextService.GetUserId;
            var addresses = _context.Users.Include(s => s.Address).FirstOrDefault(x => x.Id == userId);
            if (!addresses.Address.Any())
                throw new EmptyListException("Nie masz zapisanych adresów");
            return addresses.Address.ToList();
        }
    }
}
