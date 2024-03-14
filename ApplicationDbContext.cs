using Microsoft.EntityFrameworkCore;
using OLIMP.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OLIMP
{
    public class ApplicationDbContext : DbContext
    {
        public string ConnectionString  = @"Server=localhost\SQLEXPRESS;Database=OLIMP1;Trusted_Connection=True;Encrypt=false";
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Level> Level { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }

        public ApplicationDbContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString: ConnectionString);
        }
    }
}
