using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelR.Entities;
using HotelR.Models;
using HotelR.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelR.Controllers
{
    [Route("api/Reservation")]
    public class ReservationController : Controller
    {
        private IReservationRepo _reservationRepo;
        private IGuestRepo _guestRepo;
        private IRoomRepo _roomRepo;
        public ReservationController(IReservationRepo reservationRepo, IGuestRepo guestRepo, IRoomRepo roomRepo)
        {
            _reservationRepo = reservationRepo;
            _guestRepo = guestRepo;
            _roomRepo = roomRepo;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]ReservationDto value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var room = _roomRepo.Get(value.RoomNumber);
            if (room == null)
                return BadRequest($"Room {value.RoomNumber} is not exist");
            
            var guest = _guestRepo.Create(
                new Guest{Name=value.GuestName, Email=value.GuestEmail,Phone=value.GuestPhone}
            );
           return Ok(guest);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
