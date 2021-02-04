using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Services.Implementation;
using Services;
using Repository;
using Repository.Implementations;
using UnitOfWork;
using Data;

namespace CountriesAndHolidaysApp
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
            var settings = new ConnString
            {
                ConnectionString = Configuration.GetConnectionString("DefaultConnection")
        };

            services.AddSingleton(settings);


            services.AddScoped<ICountryHolidayServices,CountryHolidayServices>();
            services.AddScoped<ICountryRepository,CountryRepository>();
            services.AddScoped<IHolidayRepository,HolidayRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork.UnitOfWork>();
            services.AddScoped<IDBFactory, DBFactory>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
