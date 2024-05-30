
using courses.Api.DbContexts;
using courses.Api.Services;
using courses.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace courses.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;

                options.RespectBrowserAcceptHeader = true;
            }).AddXmlDataContractSerializerFormatters();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Add services to the container.
            builder.Services.AddScoped<ICourseLibraryRepository,CourseLibraryRepository>();
            builder.Services.AddDbContext<CourseLibraryContext>(options =>
            options.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=CourseLibraryDB;Trusted_Connection=True;"));



            
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
            app.UseRouting();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}