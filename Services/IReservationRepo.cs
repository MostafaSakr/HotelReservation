using System;
using HotelR.Entities;

namespace HotelR.Services
{
    public interface IReservationRepo
    {
        Reservation Create(Guest guest, Room room, DateTime arrivalDate, DateTime depatureDate);
    }
}