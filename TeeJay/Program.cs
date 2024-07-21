
using Core.AdminUserServices;
using Data.DbContext;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TeeJay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IAdminUserService, AdminUserService>();
            builder.Services.AddControllers();
            builder.Services.AddIdentity<AdminUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
                            .AddDefaultTokenProviders();
            var connectionString = builder.Configuration.GetConnectionString("TeeJayConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
