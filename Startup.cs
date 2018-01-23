using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelR.Entities;
using HotelR.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HotelR
{
    public class Startup
    {
        public static IConfiguration configuration { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var connection = "Password=123;Persist Security Info=True;User ID=task;Initial Catalog=taskDB;Data Source=52.178.217.7";
            services.AddDbContext<HotelReservationContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IGuestRepo, GuestRepo>();
            services.AddScoped<IReservationRepo, ReservationRepo>();
            services.AddScoped<IRoomRepo, RoomRepo>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
