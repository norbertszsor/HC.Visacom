using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolateAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateAPI.Entities
{
    public class HotChocolateDbContext: DbContext
    {
        private string _connectionString = "Server=tcp:okiplantsdbserver.database.windows.net,1433;Initial Catalog=OkiPlantsDb;Persist Security Info=False;User ID=Admin1337;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderAmountProducts> OrderAmountProducts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostParts> PostParts { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(n => n.Email)
                .IsRequired()
                .HasMaxLength(35);
            modelBuilder.Entity<User>()
                .Property(n => n.FirstName)
                .IsRequired()
                .HasMaxLength(35);
            modelBuilder.Entity<User>()
                .Property(n => n.LastName)
                .IsRequired()
                .HasMaxLength(35);
            modelBuilder.Entity<User>()
                .Property(n => n.PasswordHash)
                .IsRequired();
            modelBuilder.Entity<Role>()
                .Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<Order>()
                .HasMany(e => e.Products)
                .WithMany(x => x.Orders);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
