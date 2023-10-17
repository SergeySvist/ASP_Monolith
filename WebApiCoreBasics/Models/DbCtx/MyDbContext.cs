using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApiCoreBasics.Models.Entities;

namespace WebApiCoreBasics.Models.DbCtx
{
    public class MyDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDbConnection"));
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }

}
