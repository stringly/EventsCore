using EventsCore.Application.Common.Interfaces;
using EventsCore.Common;
using EventsCore.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebUI;

namespace EventsCore.WebUI.IntegrationTests.Common
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
        {
        }
        protected override void ConfigureAuth(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Test Scheme"; // has to match scheme in TestAuthenticationExtensions
                options.DefaultChallengeScheme = "Test Scheme";
            }).AddTestAuth(o => { });
        }
        protected override void ConfigurePersistence(IServiceCollection services)
        {
            var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();
            services.AddDbContext<EventsCoreDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
                options.UseInternalServiceProvider(serviceProvider);
            });            
            services.AddScoped<IEventsCoreDbContext, EventsCoreDbContext>();
            services.AddAntiforgery(t =>
            {
                t.Cookie.Name = AntiForgeryTokenExtractor.AntiForgeryCookieName;
                t.FormFieldName = AntiForgeryTokenExtractor.AntiForgeryFieldName;
            });
            SeedData(services);
        }
        protected override void ConfigureDependencies(IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserServiceTest>();
            services.AddScoped<IDateTime, DateTimeTestProvider>();
        }
        private void SeedData(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<EventsCoreDbContext>();
            

            // Ensure the database is created.
            context.Database.EnsureCreated();
            // Seed the database with test data.
            Utilities.InitializeDbForTests(context);
            
        }
    }
}
