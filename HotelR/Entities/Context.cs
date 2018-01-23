using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

// it should be every entity in a separate class 
// but i did by this way because:
// it is a task and i wanted to make it easy to read 
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

    public class Guest 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
       
    }
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guest Guest { get; set; }
        public int GuestId { get; set; }
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Status { get; set; }
        public double Fees { get; set; }
        public int Days()
        {
            return (DepartureDate - ArrivalDate).Days;
        }
    }
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public double DepositFeePercentage { get; set; }
        public int CancellationFeeNightsCount { get; set; }
    }
    public enum ReservationStatus{
        Booked = 1,
        Canceled,
        CheckedIn,
        CheckedOut
    }
}
