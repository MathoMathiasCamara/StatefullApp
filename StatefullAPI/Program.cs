using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StatefullAPI.Models;
using StatefullAPI.Stores;

namespace StatefullAPI
{
    public class Program
    {
        public static  void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContextFactory<StateDbContext>(option => option.UseSqlite("Data source=./statefull.db"));
            builder.Services.AddScoped<StateDbContext>(options => options.GetRequiredService<IDbContextFactory<StateDbContext>>().CreateDbContext());
            builder.Services.AddScoped<SaleStore>();
            builder.Services.AddScoped<ProductStore>();


            var app = builder.Build();

            InitializeDb(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static async void InitializeDb(WebApplication app)
        {
            await StateDbContext.InitializeDb(app.Services.GetRequiredService<IDbContextFactory<StateDbContext>>().CreateDbContext());
        }
    }
}