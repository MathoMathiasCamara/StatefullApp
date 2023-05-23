
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace StatefullAPI.Models
{
    public class StateDbContext : DbContext
    {
        public StateDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<ProductModel> Products { get; set; }
        public DbSet<SaleModel> Sales { get; set; }

        public static async Task InitializeDb(StateDbContext stateDbContext)
        {
            if(stateDbContext == null) throw new ArgumentNullException(nameof(stateDbContext));

            await stateDbContext.Database.EnsureCreatedAsync();      
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name).IsRequired();
            });

            modelBuilder.Entity<SaleModel>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Client).IsRequired();
                builder.Property(x => x.SaleItems).IsRequired();

            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
