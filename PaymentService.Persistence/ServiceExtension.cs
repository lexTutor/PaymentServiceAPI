using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Persistence
{
    public static class ServiceExtension
    {
        public static void AddDbConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<PaymentDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
