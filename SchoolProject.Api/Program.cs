using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure;
using SchoolProject.Service;
using SchoolProject.Core;

namespace SchoolProject.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDBContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            #region Dependencies Injection
            builder.Services.AddInfrastructureDependencies()
                            .AddServiceDependencies()
                            .AddCoreDependencies();
            #endregion


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
