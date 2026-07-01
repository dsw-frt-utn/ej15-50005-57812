using Dsw2026Ej15.Api.Configurations;
using Dsw2026Ej15.Api.ExceptionHandler;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Data.Extensions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Registro de Servicios
            builder.Services.AddApplicationPersistence(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHealthChecks();
            builder.Services.AddScoped<IPersistence, PersistenceEf>();

            var app = builder.Build();

            //Middleware
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Configuración del entorno de desarrollo
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapHealthChecks("/health-check");
            app.MapControllers();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<Dsw2026Ej15DbContext>();
            context.SeedworkSpecialities(@"specialities.json");

            app.Run();
        }
    }
}
