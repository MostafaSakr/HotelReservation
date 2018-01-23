using System;
using System.Collections.Generic;
using System.Linq;
using HotelR.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelR.Services
{
    
    public class ReservationRepo : IReservationRepo
    {
        private IUnitOfWork<Reservation> _context;

        public ReservationRepo(IUnitOfWork<Reservation> context)
        {
            _context = context;
        }
        public Reservation Book(Guest guest, Room room, DateTime arrivalDate, DateTime depatureDate)
        {
            var fees = (depatureDate - arrivalDate).Days * (room.Rate * room.DepositFeePercentage / 100);
            var reservation = new Reservation
            {
                GuestId = guest.Id,
                RoomId = room.Id,
                ArrivalDate = arrivalDate,
                DepartureDate = depatureDate,
                Status = ReservationStatus.Booked.ToString(),
                Fees = fees
            };

           var result = _context.Insert(reservation);
            _context.SaveChanges();
            reservation.Id = result.Id;

           return reservation;
        }

        public Reservation CheckIn(Reservation reservation)
        {
            reservation.Status = ReservationStatus.CheckedIn.ToString();         
            _context.Update(reservation);
            _context.SaveChanges();

            return reservation;
        }

        public Reservation CheckOut(Reservation reservation, DateTime? date)
        {
            var room = reservation.Room;
            if (date != reservation.DepartureDate && date.HasValue)
                reservation.DepartureDate = date.Value;
            
            var fees = reservation.Days() * room.Rate;
            reservation.Fees = fees;
            reservation.Status = ReservationStatus.CheckedOut.ToString();

            _context.Update(reservation);
            _context.SaveChanges();

            return reservation;
        }

        public Reservation Cancel(Reservation reservation)
        {
            var room = reservation.Room;
            var cancelPelanty = room.CancellationFeeNightsCount;
            if (reservation.Days() < cancelPelanty)
                cancelPelanty = reservation.Days();
            var fees = cancelPelanty * room.Rate;
            reservation.Fees = fees;
            reservation.Status = ReservationStatus.Canceled.ToString();

            _context.Update(reservation);
            _context.SaveChanges();

            return reservation;
        }

        public Reservation Get(int id)
        {
            return _context.Get(x => x.Id == id).Include(x=>x.Room).FirstOrDefault();
        }
        public List<Reservation> Get(string status)
        {
            return _context.Get(x => x.Status == status).Include(x => x.Room).ToList();
        }
        public List<Reservation> Get(DateTime? toArrivalDate, DateTime? fromArrivalDate)
        {
            return _context.Get
                           (x => (toArrivalDate.HasValue && x.ArrivalDate <= toArrivalDate)
                                  ||   (fromArrivalDate.HasValue && x.ArrivalDate >= fromArrivalDate)
                                 )
                           .Include(x => x.Room).ToList();
        }
        public List<Reservation> Get(string guestName, string guestEmail, string guestPhone)
        {
            return _context
                           .Get(x =>  (guestName != null &&  x.Guest.Name == guestName)
                                  || (guestEmail != null && x.Guest.Email == guestEmail)
                                  || (guestPhone != null && x.Guest.Phone == guestPhone)
                                 )
                           .Include(x => x.Room).ToList();
        }

    }
}
