using System;
using System.Linq;
using HotelR.Entities;
using HotelR.Services;
using Moq;
using NUnit.Framework;

namespace HotelR.Test
{
    [TestFixture]
    public class ReservationTest
    {
        private Mock<ReservationRepo> reservationRepoMocked;
        [SetUp]
        public void Setup()
        {
            reservationRepoMocked = new Mock<ReservationRepo>();
        }

       
    }

}
