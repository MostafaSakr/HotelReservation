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
    [Route("api/gest")]
    public class GuestController : Controller
    {
        private IReservationRepo _reservationRepo;
        private IGuestRepo _guestRepo;
        private IRoomRepo _roomRepo;
        public GuestController(IReservationRepo reservationRepo, IGuestRepo guestRepo, IRoomRepo roomRepo)
        {
            _reservationRepo = reservationRepo;
            _guestRepo = guestRepo;
            _roomRepo = roomRepo;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("fees/{id}")]
        public IActionResult GuestAccount(int id)
        {
            var reservation = _reservationRepo.Get(id);
            if (reservation == null)
                return BadRequest($"Reservation {id} is not exist");
            var account = _guestRepo.GetFees(reservation);
            return Ok(account);
        }

       

    }
}
