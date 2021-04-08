using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolateAPI.Entities;
using Microsoft.AspNetCore.Identity;

namespace HotChocolateAPI
{
    public class HotChocolateSeeder
    {
        private readonly HotChocolateDbContext _dbContext;
        public IPasswordHasher<User> _passwordHasher { get; }
        public HotChocolateSeeder(HotChocolateDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }
        public void Seed()
        {
            
            if(_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                var user = _dbContext.Users.FirstOrDefault(x => x.Email == "admin@gmail.com");
                if (user == null)
                {

                    var newAdmin = CreateAdmin();
                    

                    _dbContext.Users.AddRange(newAdmin);

                    _dbContext.SaveChanges();
                }
               
            }
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name="Customer"
                },
                new Role()
                {
                    Name="Warehouseman"
                },
                new Role()
                {
                    Name="Admin"
                }
            };
            return roles;
        }
        private User CreateAdmin()// created admin account just for testing (hard coded).
        {
            var newAdmin = new User()
            {
                Email = "admin@gmail.com",
                FirstName = "kek",
                LastName = "kek",
                IsActivated = true,
                RoleId = 3
            };

            var password = "admin1";

            var hashedPassword = _passwordHasher.HashPassword(newAdmin, password);

            newAdmin.PasswordHash = hashedPassword;

            return newAdmin;
        }
    }
   
}
