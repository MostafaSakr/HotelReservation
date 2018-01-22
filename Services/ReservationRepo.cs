using System;
using HotelR.Entities;

namespace HotelR.Services
{
    public class ReservationRepo : IReservationRepo
    {
        private HotelReservationContext _context;

        public ReservationRepo(HotelReservationContext context)
        {
            _context = context;
        }
        public Reservation Create(Guest guest, Room room, DateTime arrivalDate, DateTime depatureDate)
        {
            var fees = (arrivalDate - depatureDate).Days * (room.Rate * room.DepositFeePercentage / 100);
            var reservation = new Reservation
            {
                Guest = guest,
                Room = room,
                ArrivalDate = arrivalDate,
                DepartureDate = depatureDate,
                Status = ReservationStatus.Booked.ToString(),
                Fees = fees
            };

           var result = _context.Reservations.Add(reservation);
            _context.SaveChanges();
            reservation.Id = result.Entity.Id;

           return reservation;
        }
    }
}
