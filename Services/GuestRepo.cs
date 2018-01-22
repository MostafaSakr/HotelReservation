using System;
using HotelR.Entities;

namespace HotelR.Services
{
    public interface IGuestRepo
    {
        Guest Create(Guest guest);
    }
    public class GuestRepo : IGuestRepo
    {
        private HotelReservationContext _context;
        public GuestRepo(HotelReservationContext context)
        {
            _context = context;
        }
        public Guest Create(Guest guest)
        {
           var result =  _context.Guests.Add(guest);
            _context.SaveChanges();
            guest.Id = result.Entity.Id;
            return guest;
        }
    }
}
