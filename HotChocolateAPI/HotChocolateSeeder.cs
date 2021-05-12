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

            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    foreach (var item in roles)
                    {
                        _dbContext.Add(item);
                        _dbContext.SaveChanges();
                    }
                }

                var user = _dbContext.Users.FirstOrDefault(x => x.Email == "admin@gmail.com");
                if (user == null)
                {
                    var newAdmin = CreateAdmin();
                    _dbContext.Users.Add(newAdmin);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Products.Any())
                {
                    var products = GetProducts();
                    _dbContext.AddRange(products);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.OrderStatuses.Any())
                {
                    var statuses = GetStatuses();
                    foreach (var item in statuses)
                    {
                        _dbContext.Add(item);
                        _dbContext.SaveChanges();
                    }
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
        private List<Product> GetProducts()
        {
            var products = new List<Product>()
            {
                new Product()
                {
                    Name = "Kwiatek",
                    Price = 12,
                    Description = "Tulipan jakiś tam",
                    Amount = 10
                },
                new Product()
                {
                    Name = "Doniczka",
                    Price = 8,
                    Description = "Plastikowa",
                    Amount = 10
                },
                new Product()
                {
                    Name = "Nawóz",
                    Price = 33,
                    Description = "Saletra",
                    Amount = 10
                }
            };
            return products;
        }
        private List<OrderStatus> GetStatuses()
        {
            var roles = new List<OrderStatus>()
            {
                new OrderStatus()
                {
                    Name="W trakcie realizacji"
                },
                new OrderStatus()
                {
                    Name="Czeka na nadanie"
                },
                new OrderStatus()
                {
                    Name="Przekazane kurierowi"
                },
                new OrderStatus()
                {
                    Name="Zakończone"
                },
                new OrderStatus()
                {
                    Name="Anulowano"
                }
            };
            return roles;
        }
    }
}