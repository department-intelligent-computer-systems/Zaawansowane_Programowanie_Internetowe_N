using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebStore.Model;

namespace WebStore.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() {}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
          
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<StationaryStore> StationaryStores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.ProductId, op.OrderId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(p => p.Product)
                .WithMany(op => op.OrderProducts)
                .HasForeignKey(ord => ord.ProductId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<OrderProduct>()
                .HasOne(o => o.Order)
                .WithMany(op => op.OrderProducts)
                .HasForeignKey(ord => ord.OrderId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Invoice)
                .WithMany(i => i.Orders)
                .HasForeignKey(o => o.InvoiceId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<User>()
                .HasDiscriminator<int>("UserType")
                .HasValue<Customer>(1)
                .HasValue<Supplier>(2)
                .HasValue<StationaryStoreEmployee>(3);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Path.GetFullPath("../WebStore.Web/"))
                .AddJsonFile("appsettings.json")
                .Build();


                var connectionString = config.GetConnectionString("DbConnection");
                if (connectionString != null)
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
                else
                {
                    Console.WriteLine("Connection string is null or empty. Please configure it in appsettings.json.");
                }
            }
        }

    }
}