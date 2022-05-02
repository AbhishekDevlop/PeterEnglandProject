using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeterEngland2.Models;
using PeterEngland2.ModelView;


namespace PeterEngland2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }
        public DbSet<Users> Users { get; set; }
       
        public DbSet<Inventry> Inventry { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Cart> Cart { get; set; }
        public DbSet<PeterEngland2.Models.Ordered> Ordered { get; set; }

        public DbSet<PeterEngland2.ModelView.InventryModel> InventryModel { get; set; }
    }
}
