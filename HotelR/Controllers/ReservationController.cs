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
      
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]ReservationDto value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var room = _roomRepo.Get(value.RoomNumber);
            if (room == null)
                return BadRequest($"Room {value.RoomNumber} is not exist");

            if (value.ArrivalDate >= value.DepartureDate)
                return BadRequest("Arrival date can not be the same or after the Depature date");
            
            var guest = _guestRepo.Create(
                new Guest{Name=value.GuestName, Email=value.GuestEmail,Phone=value.GuestPhone}
            );

            var reservation = _reservationRepo.Book(guest, room, value.ArrivalDate, value.DepartureDate);

           return Ok(reservation);
        }

        [HttpPut("{id}/CheckOut")]
        public IActionResult CheckOut(int id,DateTime? date)
        {
            var reservation = _reservationRepo.Get(id);
            if (reservation == null)
                return BadRequest("reservation not exist");
            if (reservation.Status != ReservationStatus.CheckedIn.ToString())
                return BadRequest("unvalid reservation status");
            
            var result = _reservationRepo.CheckOut(reservation, date);
            return Ok(result);
        }

        [HttpPut("{id}/CheckIn")]
        public IActionResult CheckIn(int id)
        {
            var reservation = _reservationRepo.Get(id);
            if (reservation == null)
                return BadRequest("reservation not exist");
            if (reservation.Status != ReservationStatus.Booked.ToString())
                return BadRequest("unvalid reservation status");
            
            var result = _reservationRepo.CheckIn(reservation);
            return Ok(result);
        }

        [HttpPut("{id}/Cancel")]
        public IActionResult Cancel(int id)
        {
            var reservation = _reservationRepo.Get(id);
            if (reservation == null)
                return BadRequest("reservation not exist");
            if (reservation.Status != ReservationStatus.Booked.ToString())
                return BadRequest("unvalid reservation status");
            
           var result =  _reservationRepo.Cancel(reservation);
            return Ok(result);
        }

        [HttpGet("status/{status}")]
        public List<Reservation> Get(string status)
        {
            return _reservationRepo.Get(status);
        }
        [HttpGet("guest")]
        public List<Reservation> Get(string name, string email, string phone)
        {
            return _reservationRepo.Get(name, email, phone);
        }
        [HttpGet("date")]
        public List<Reservation> Get(DateTime? toArrivalDate, DateTime? fromArrivalDate)
        {
            return _reservationRepo.Get(toArrivalDate, fromArrivalDate);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_reservationRepo.Get(id));
        }

    }
}
