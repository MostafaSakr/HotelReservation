using System;
using System.Collections.Generic;
using System.Linq;
using HotelR.Entities;

namespace HotelR.Services
{
    public interface IRoomRepo 
    {
        Room Get(string number);
        List<Room> Get();
    }

    public class RoomRepo : IRoomRepo
    {
        private IUnitOfWork<Room> _context;
        public RoomRepo(IUnitOfWork<Room> context)
        {
            _context = context;
        }

        public Room Get(string number)
        {
            return _context.Get(x => x.Number == number).FirstOrDefault();
        }

        public List<Room> Get()
        {
            return _context.Get(x => true == true).ToList();
        }
    }
}
