using HotelR.Entities;
using HotelR.Models;

namespace HotelR.Services
{
    public class GuestRepo : IGuestRepo
    {
        private IUnitOfWork<Guest> _context;
        public GuestRepo(IUnitOfWork<Guest> context)
        {
            _context = context;
        }
        public Guest Create(Guest guest)
        {
           var result =  _context.Insert(guest);
            _context.SaveChanges();
            guest.Id = result.Id;
            return guest;
        }
        public GuestAccount GetFees(Reservation reservation)
        {
            var room = reservation.Room;

            var account = new GuestAccount();
            account.BookingNumber = reservation.Id;
            account.ActuallPaid = reservation.Fees;
            account.BookingStatus = reservation.Status;
            account.CancelationFees = room.Rate * room.CancellationFeeNightsCount;
            account.BookingFees = reservation.Days() * room.Rate ;
            account.DepositFees = reservation.Days() * (room.Rate * room.DepositFeePercentage / 100);

            return account;
        }
    }
}
