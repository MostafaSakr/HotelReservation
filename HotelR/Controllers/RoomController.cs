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
    [Route("api/room")]
    public class RoomController : Controller
    {
        private IRoomRepo _roomRepo;
        public RoomController(IRoomRepo roomRepo)
        {
            _roomRepo = roomRepo;
        }

        // GET api/values/5
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_roomRepo.Get());
        }

    }
}
