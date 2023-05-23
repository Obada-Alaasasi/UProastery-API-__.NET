using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UProastery.Data.User;

namespace UProastery.Data.DB {
    
    // create a context for Uproastery database
    public class UP_context : IdentityDbContext<ApiUser> {
        
        //constructor
        public UP_context(DbContextOptions<UP_context> options) : base(options) {

        }

        //tables 
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>().HasData(
                new Item {
                    Id = 1,
                    Title = "Spanish Latte",
                    Price = 2.5,
                    Added = DateTime.Now,
                    TotalOrders = 0
                },
                new Item {
                    Id = 2,
                    Title = "Cookies",
                    Price = 1.0,
                    Added = DateTime.Now,
                    TotalOrders = 0,
                    Stock = 30
                },
                new Item {
                    Id = 3,
                    Title = "Espresso",
                    Price = 1.0,
                    Added = DateTime.Now,
                    TotalOrders = 0
                },
                new Item {
                    Id = 4,
                    Title = "Ethiopian Coffee Beans (500g)",
                    Price = 6.5,
                    Added = DateTime.Now,
                    TotalOrders = 0,
                    Stock = 30
                }
            );
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole {
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },
                new IdentityRole {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            );

            // OrderItem: Foreign key to Item
            modelBuilder.Entity<Item>()
                .HasMany(p => p.OrderItems)
                .WithOne(p => p.Item)
                .HasForeignKey(p => p.ItemId);

            // OrderItem: Foreign key to Order
            modelBuilder.Entity<Order>()
                .HasMany(p => p.OrderItems)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            // Order: Foreign key to User
            modelBuilder.Entity<ApiUser>()
                .HasMany(p => p.MyOrders)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .HasPrincipalKey(p => p._Id);

            modelBuilder.Entity<OrderItem>()
                .ToTable(tb => tb.HasTrigger("deduct_stock"));
        }
    }
}
