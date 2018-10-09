using ClaimsTraining.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClaimsTraining.Data
{
    public class DefaultContext : DbContext
    {
        private readonly IConfiguration IConfig;

        public DefaultContext(IConfiguration _IConfig)
        {
            IConfig = _IConfig;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            OptionsBuilder.UseSqlServer(IConfig.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder _Builder)
        {
            _Builder.Entity<User>().ToTable("Users");
            _Builder.Entity<Customer>().ToTable("Customers");
            _Builder.Entity<Role>().ToTable("Roles");
        }
    }
}
