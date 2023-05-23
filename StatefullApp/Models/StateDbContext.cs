
using System.Configuration;
using System.Data.Entity;
using System.Data.SQLite;

namespace StatefullApp.Models
{
    public class StateDbContext : DbContext
    {
        public StateDbContext(string databasePath) : base(new SQLiteConnection
        {
            ConnectionString = new SQLiteConnectionStringBuilder { DataSource = databasePath, FailIfMissing = false }.ConnectionString
        }, true)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbContext>());
            Database.Initialize(false);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<SaleModel> Sales { get; set; }
    }
}
