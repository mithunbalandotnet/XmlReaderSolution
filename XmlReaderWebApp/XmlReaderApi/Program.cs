using XmlReaderApi.Infrastructure;

namespace XmlReaderApi
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                builder => builder.WithOrigins("http://localhost:4200") // For localhost
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors("AllowAngularApp");
            app.UseMiddleware<CustomExceptionHandler>();
            app.MapControllers();

            app.Run();
        }
    }
}