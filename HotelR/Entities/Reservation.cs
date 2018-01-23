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
}
