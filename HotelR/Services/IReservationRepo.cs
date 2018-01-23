using System;
using System.Collections.Generic;
using HotelR.Entities;

namespace HotelR.Services
{
    public interface IReservationRepo
    {
        Reservation Book(Guest guest, Room room, DateTime arrivalDate, DateTime depatureDate);
        Reservation Cancel(Reservation reservation);
        Reservation Get(int id);
        Reservation CheckOut(Reservation reservation, DateTime? date);
        Reservation CheckIn(Reservation reservation);
        List<Reservation> Get(DateTime? toArrivalDate, DateTime? fromArrivalDate);
        List<Reservation> Get(string status);
        List<Reservation> Get(string guestName, string guestEmail, string guestPhone);
    }
}