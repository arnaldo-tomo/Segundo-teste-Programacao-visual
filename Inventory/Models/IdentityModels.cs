using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Inventory.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<Suppliers> Supplierss { get; set; }
        public DbSet<MyStock> MyStocks { get; set; }
        public DbSet<Total_Stock_Order> Total_Stock_Orders { get; set; }
        public DbSet<MyStockNotification> MyStockNotifications { get; set; }
        public DbSet<TotalItemsQuantity> TotalItemsQuantitys { get; set; }
        public DbSet<Sell_Item> Sell_Items { get; set; }
        public DbSet<Sell_Items_To_Customer> Sell_Items_To_Customers { get; set; }

        public DbSet<Total_Customer_Order> Total_Customer_Orders { get; set; }
        public DbSet<login> logins { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}