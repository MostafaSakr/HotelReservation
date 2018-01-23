using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace HotelR.Entities
{
    public class HotelReservationContext : DbContext
    {
        public HotelReservationContext() : base(new DbContextOptionsBuilder().UseSqlServer(connection).Options)
        {
        }

        public HotelReservationContext(DbContextOptions options)
            : base(options)
        {
            
        }
        public static string connection;
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
