using Application.Services;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("database")));

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Company API", Version = "v1" }));

            // Регистрация репозиториев
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();

            // Регистрация сервисов
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<IBuildingService, BuildingService>();

            builder.Services.AddEndpointsApiExplorer();
            var app = builder.Build();

            // Конфигурация HTTP-запросов
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                //app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c =>
                //{
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company API v1");
                //c.RoutePrefix = string.Empty;
                //});
            }

            //app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
