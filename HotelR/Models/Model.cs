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
        public string GuestEmail { get; set; }
        public string GuestPhone { get; set; }
    }
    public class GuestAccount
    {
        public int BookingNumber { get; set; }
        public double BookingFees { get; set; }
        public double DepositFees { get; set; }
        public double CancelationFees { get; set; }
        public double ActuallPaid { get; set; }
        public string BookingStatus { get; set; }
    }
}
