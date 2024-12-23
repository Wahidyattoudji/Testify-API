
using Microsoft.EntityFrameworkCore;
using Testify.Core.Interfaces;
using Testify.Infrastructure;
using Testify.Infrastructure.UnitOfWork;

namespace TestifyWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<TestifyDbContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
                );


            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


            builder.Services.AddControllers();

            builder.Services.AddCors(); // open the conection from othr networks

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
            //middlewares

            app.UseHttpsRedirection();

            app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); //allow the access to api from anywere 

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
