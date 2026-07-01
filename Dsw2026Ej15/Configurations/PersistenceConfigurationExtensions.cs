using Dsw2026Ej15.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Api.Configurations
{
    public static class PersistenceConfigurationExtensions
    {
        public static IServiceCollection AddApplicationPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Dsw2026Ej15DbContext>(options => {
                options.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}
