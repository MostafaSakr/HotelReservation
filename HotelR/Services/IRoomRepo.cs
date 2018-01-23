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
}
