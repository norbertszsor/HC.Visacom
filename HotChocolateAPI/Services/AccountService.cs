using System;
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

namespace HotChocolateAPI.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        bool Delete(int i);
        string GenerateJwt(LoginDto dto);
        void ChangeActivity(int id, ManageAccountDto dto);
        List<UserList> GetAll();
        public void ChangePassword(NewPasswordDto dto);


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
            var list = _context.Users.ToList();

            if (list == null)
                throw new BadRequestException("Empty list of Users");

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
                IsActivated = true
                
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
        public void ChangeActivity(int id, ManageAccountDto dto)
        {
            var user = _context.Users.Include(x=>x.Role).FirstOrDefault(x => x.Id == id);
            if (user == null)
                throw new BadRequestException("User doesnt exist");

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
    }
}
