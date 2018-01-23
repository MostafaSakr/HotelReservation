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
            var connection = HotelReservationContext.connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<HotelReservationContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IGuestRepo, GuestRepo>();
            services.AddScoped<IReservationRepo, ReservationRepo>();
            services.AddScoped<IRoomRepo, RoomRepo>();
            services.AddScoped<IUnitOfWork<Reservation>, UnitOfWork<Reservation>>();
            services.AddScoped<IUnitOfWork<Room>, UnitOfWork<Room>>();
            services.AddScoped<IUnitOfWork<Guest>, UnitOfWork<Guest>>(); 
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
