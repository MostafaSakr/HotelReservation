using System;
using HotelR.Entities;
using HotelR.Models;

namespace HotelR.Services
{
    public interface IGuestRepo
    {
        Guest Create(Guest guest);
        GuestAccount GetFees(Reservation reservation);
    }
}
