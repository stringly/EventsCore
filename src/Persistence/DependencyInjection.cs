using EventsCore.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace EventsCore.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EventsCoreDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EventsCoreDatabase")));

            services.AddScoped<IEventsCoreDbContext>(provider => provider.GetService<EventsCoreDbContext>());
            return services;
        }
    }
}
