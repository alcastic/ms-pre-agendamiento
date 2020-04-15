using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ms_pre_agendamiento.Service;

namespace ms_pre_agendamiento
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins(
                                "https://localhost:3000",
                                "http://pre-agendamiento-front.azurewebsites.net",
                                "http://52.141.211.84/calendaravailability/",
                                "http://52.141.211.84/")
                            .WithMethods("GET", "POST");
                    });
            });

            services.AddControllers();

            var healthCareFacilitiesUri = Configuration.GetSection("AppSettings")["HealthcareFacilitiesUri"];
            services.AddHttpClient("HealthcareFacilitiesAPI",
                c => c.BaseAddress = new Uri(healthCareFacilitiesUri));
            services.AddTransient<IBusyCalendarTimeSlotsRepository, BusyCalendarTimeSlotsRepository>();
            services.AddTransient<IAllCalendarTimeSlotsRepository, AllCalendarTimeSlotsRepository>();
            services.AddTransient<ICalendarAvailabilityService, CalendarAvailabilityService>();
            services.AddTransient<IHealthcareFacilityService, HealthcareFacilityService>();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"}); });
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

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}