﻿using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Connection
{
    public class DbContextConnection:DbContext
    {
        public DbContextConnection(DbContextOptions<DbContextConnection> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}