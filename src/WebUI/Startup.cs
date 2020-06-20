using EventsCore.Application;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Common;
using EventsCore.Persistence;
using EventsCore.WebUI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace WebUI
{
    public class Startup
    {
        private IServiceCollection _services;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        protected virtual void ConfigureAuth(IServiceCollection services)
        {
            services.AddAuthentication(Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme);
            services.AddScoped<IClaimsTransformation, ClaimsLoader>();
        }
        protected virtual void ConfigureAppInsights(IServiceCollection services)
        {

        }
        protected virtual void ConfigureDependencies(IServiceCollection services)
        {        
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IDateTime, DateTimeProvider>();
            
        }
        protected virtual void ConfigurePersistence(IServiceCollection services)
        {
            services.AddPersistence(Configuration);

        }
        public void ConfigureServices(IServiceCollection services)
        {
            this.ConfigureAuth(services);
            this.ConfigureAppInsights(services);
            this.ConfigureDependencies(services);
            this.ConfigurePersistence(services);
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            services.AddApplication();
            services.AddHealthChecks()
                .AddDbContextCheck<EventsCoreDbContext>();
            services.AddMvc(options => options.EnableEndpointRouting = false).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IEventsCoreDbContext>());
            _services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                RegisteredServicesPage(app);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
        }
        private void RegisteredServicesPage(IApplicationBuilder app)
        {
            app.Map("/services", builder => builder.Run(async context =>
            {
                var sb = new StringBuilder();
                sb.Append("<h1>Registered Services</h1>");
                sb.Append("<table><thead>");
                sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
                sb.Append("</thead><tbody>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
                await context.Response.WriteAsync(sb.ToString());
            }));
        }
    }
}
