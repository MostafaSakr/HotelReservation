
namespace HotelR.Models
{
    
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
