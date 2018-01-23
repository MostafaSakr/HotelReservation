using System;
using System.Collections.Generic;
using System.Linq;
using HotelR.Entities;

namespace HotelR.Services
{
    public interface IRoomRepo
    {
        Room Get(string number);
    }
    public class RoomRepo : IRoomRepo
    {
        private HotelReservationContext _context;
        public RoomRepo(HotelReservationContext context)
        {
            _context = context;
        }

        public Room Get(string number)
        {
           return _context.Rooms.Where(x => x.Number == number).FirstOrDefault();
        }
    }
}
