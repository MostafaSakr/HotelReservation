using System;
using System.ComponentModel.DataAnnotations;

namespace HotelR.Models
{
    public class ReservationDto
    {
        [Required]
        public string RoomNumber { get; set; }
        [Required]
        public DateTime ArrivalDate { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }
        [Required]
        public string GuestName { get; set; }
        [EmailAddress]
        public string GuestEmail { get; set; }
        public string GuestPhone { get; set; }
    }
}
