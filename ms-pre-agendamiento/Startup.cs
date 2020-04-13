using System;
using System.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ms_pre_agendamiento.Service;

namespace ms_pre_agendamiento
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var healthCareFacilitiesUri = Configuration.GetSection("AppSettings")["HealthcareFacilitiesUri"];
            services.AddHttpClient("HealthcareFacilitiesAPI",
                c => c.BaseAddress = new Uri(healthCareFacilitiesUri));
            services.AddTransient<IBusyCalendarTimeSlotsRepository, BusyCalendarTimeSlotsRepository>();
            services.AddTransient<IAllCalendarTimeSlotsRepository, AllCalendarTimeSlotsRepository>();
            services.AddTransient<ICalendarAvailabilityService, CalendarAvailabilityService>();
            services.AddTransient<IHealthcareFacilityService, HealthcareFacilityService>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsProduction() || env.IsStaging())
            {
                app.UseHttpsRedirection();
            }
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}