using Calendar.Api.DatabaseConnection;
using Calendar.Api.Logics;
using Calendar.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Api
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
            builder.Services.AddDbContext<CalendarContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<UserAssignmentLogic>();
            builder.Services.AddScoped<CalendarLogic>();
            builder.Services.AddScoped<CalendarEventLogic>();
            builder.Services.AddScoped<Base64Resizer>();
            builder.Services.AddScoped<EmailSender>();
            builder.Services.AddScoped<FilePath>();

            var app = builder.Build();
           
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.MapControllers();

            app.Run();
        }
    }
}
