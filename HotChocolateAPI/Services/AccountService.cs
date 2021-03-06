﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolateAPI.Models;
using HotChocolateAPI.Entities;
using Microsoft.AspNetCore.Identity;
using HotChocolateAPI.Exceptions;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using HotChocolateAPI.Models.ViewModels;
using HotChocolateAPI.Models.DTO;

namespace HotChocolateAPI.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        bool Delete(int i);
        string GenerateJwt(LoginDto dto);
        void ManageAccount(int id, ManageAccountDto dto);
        List<UserList> GetAll();
        void ChangePassword(NewPasswordDto dto);
        UserDetailsView GetUser(int id);
        void EditDetails(UpdateDetailsDto dto);
        MyAccountDetailsView MyAccountDetails();
        int CreateAccount(CreateAccountDto dto);

        List<OrderDto> GetOrders();

    }
    public class AccountService : IAccountService
    {
        private readonly HotChocolateDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;


        public AccountService(HotChocolateDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IMapper mapper, IUserContextService userContextService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _mapper = mapper;
            _userContextService = userContextService;
        }
        
        public List<UserList> GetAll()
        {
            var list = _context.Users.Include(x=>x.Role).ToList();

            if (list == null)
                throw new EmptyListException("Empty list of Users");

            var result = _mapper.Map<List<UserList>>(list);
            
            return result;
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RoleId = 1,
                IsActivated = true,
                PhoneNumber = dto.PhoneNumber
                
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
        public bool Delete(int i)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Id == i);
            if (user is null)
            {
                return false;
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            if(user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }
           

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }
            if (user.IsActivated == false)
                throw new BadImageFormatException("Account banned");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssurl,
                _authenticationSettings.JwtIssurl,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
        public void ManageAccount(int id, ManageAccountDto dto)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(x => x.Id == id);
            if (user == null)
                throw new BadRequestException($"Użytkownik o id: {id} nie istnieje");

            if (dto.Email!=null)
            {
                var emailInDb = _context.Users.FirstOrDefault(x => x.Email == dto.Email);
                if(emailInDb!=null)
                {
                    throw new AlreadyExists("Email jest już zajęty!");
                }
                user.Email = dto.Email;  
            }
            if(dto.Password!=null)
            { 
                if (dto.Password.Length < 8)
                { 
                    throw new Exception("Hasło musi mieć min. 8 znaków");
                }
                var newhashedPassword = _passwordHasher.HashPassword(user, dto.Password);
                user.PasswordHash = newhashedPassword;
            }
         
            if (user.RoleId == 3)
                throw new BadRequestException("Nie możesz zarządzać tym kontem");
            if (dto.RoleId > 4 || dto.RoleId <= 0)
                throw new BadRequestException($"Podano niewlaściwe RoleId: {dto.RoleId}");
            if (dto.RoleId == 3)
                throw new BadRequestException("Nie możesz nominować nowego admina.");
            if(dto.FirstName!=null)
                user.FirstName = dto.FirstName;
            if (dto.LastName != null)
                user.LastName = dto.LastName;
            if (dto.PhoneNumber.Length < 9)
                throw new Exception("Numer musi mieć min 9 cyfr.");

         
            user.RoleId = dto.RoleId;
            user.IsActivated = dto.IsActivated;
            _context.SaveChanges();
        }
        public void ChangePassword(NewPasswordDto dto)
        {
            var iduser = (int)_userContextService.GetUserId;

            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Id == iduser);
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.OldPassword);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Incorrect password");
            }
            var newhashedPassword = _passwordHasher.HashPassword(user, dto.NewPassword);
            user.PasswordHash = newhashedPassword;
            _context.SaveChanges();

        }
        public UserDetailsView GetUser(int id)
        {
            
            var user = _context.Users.Include(u=>u.Address).FirstOrDefault(x => x.Id == id);

            var userRole = _userContextService.User.IsInRole("Admin");

            if (!userRole)
                throw new NoAccess("Nie masz dostępu!");

            if (user == null)
                throw new BadRequestException($"Użytkownik o id: {id} nie istnieje");
    
            var details = _mapper.Map<UserDetailsView>(user);

            return details;
        }

        public void EditDetails(UpdateDetailsDto dto)
        {
            var userId = _userContextService.GetUserId;

            if (userId == null)
                throw new NoAccess("Musisz się zalogowac by edytować profil");

            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if(dto.FirstName!=null)
                user.FirstName = dto.FirstName;
            if(dto.LastName!=null)
                user.LastName = dto.LastName;
            if(dto.PhoneNumber!=null)
                user.PhoneNumber = dto.PhoneNumber;


            _context.SaveChanges();
        }
        public MyAccountDetailsView MyAccountDetails()
        {
            var userId = _userContextService.GetUserId;
            var user = _context.Users.Include(x=>x.Role).FirstOrDefault(x => x.Id == userId);
            var details = _mapper.Map<MyAccountDetailsView>(user);
            details.Role = user.Role.Name;

            return details;
        }

        public int CreateAccount(CreateAccountDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RoleId = dto.RoleId,
                IsActivated = true,
                PhoneNumber = dto.PhoneNumber
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
            
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser.Id;
        }
        public List<OrderDto> GetOrders()
        {
            var userid = _userContextService.GetUserId;

            var order = _context.Orders
                .Include(p => p.Products).Include(u => u.User).Include(a => a.Address).Include(x => x.OrderAmountProducts).Include(x => x.OrderStatus)
                .Where(x => x.UserId == userid);

            if (order == null)
                throw new EmptyListException("Nie masz żadnych zamówień");

            var orderList = new List<OrderDto>();
            foreach (var item in order)
            {
                var orderdto = new OrderDto();
                orderdto.OrderId = item.Id;
                orderdto.User = _mapper.Map<UserDto>(item.User);
                orderdto.Address = item.Address;
                orderdto.TotalCost = item.TotalCost;
                var ditios = new List<CreateProductDto>();
                foreach (var item2 in item.Products)
                {
                    var tmp = _mapper.Map<CreateProductDto>(item2);
                    tmp.Amount = item.OrderAmountProducts.FirstOrDefault(x => x.ProductId == item2.Id).Amount;
                    ditios.Add(tmp);
                }
                orderdto.Products = ditios;
                orderdto.Status = item.OrderStatus.Name;
                orderList.Add(orderdto);
            }
            return orderList;







            /*var iduser = (int)_userContextService.GetUserId;

            var orders = _context.Orders
                .Include(x=>x.Products)
                .Include(u => u.User)
                .Include(a => a.Address)
                .Include(x => x.OrderAmountProducts)
                .Include(x => x.OrderStatus)
                .Where(x => x.User.Id == iduser).ToList();

            if (orders == null)
                throw new EmptyListException("Nie masz jeszcze żadnych zamówień");
          

            return _mapper.Map<List<MyOrdersDto>>(orders);*/

        }
        
    }
}
