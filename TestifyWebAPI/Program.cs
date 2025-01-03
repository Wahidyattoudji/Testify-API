
using Microsoft.EntityFrameworkCore;
using Testify.Core.Interfaces;
using Testify.Infrastructure;
using Testify.Infrastructure.UnitOfWork;
using TestifyWebAPI.Services;
using TestifyWebAPI.Services.Contracts;

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

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITestService, TestService>();
            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<IQuestionOptionService, QuestionOptionService>();
            builder.Services.AddScoped<ISubmissionService, SubmissionService>();
            builder.Services.AddScoped<ISubmissionAnswerService, SubmissionAnswerService>();
            builder.Services.AddScoped<IEvaluationService, EvaluationService>();


            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(typeof(Program));



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
