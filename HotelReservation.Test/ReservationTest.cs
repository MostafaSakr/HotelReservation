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
        private Mock<UnitOfWork<Reservation>> unitOfWorkMocked;
        private Guest guest;
        private Room room;
        [SetUp]
        public void Setup()
        {
            unitOfWorkMocked = new Mock<UnitOfWork<Reservation>>();
            unitOfWorkMocked.Setup(x => x.SaveChanges()).Returns(1);
            guest = new Guest { Name = "sasa", Id = 1 };
            room = new Room { Number = "1", Rate = 100, CancellationFeeNightsCount = 1, DepositFeePercentage = 70 };
            HotelReservationContext.connection = "x";
        }

        [Test]
        public void BookReservation_Test()
        {
            //Arrange
            unitOfWorkMocked.Setup(x => x.Insert(It.IsAny<Reservation>())).Returns<Reservation>(x => x);
            var reservationRepo = new ReservationRepo(unitOfWorkMocked.Object);

            var arrivalDate = DateTime.Now;
            var depatureDate = arrivalDate.AddDays(5);           

            //Act
            var reservation = reservationRepo.Book(guest, room, arrivalDate, depatureDate);

            //Assert
            Assert.IsTrue(reservation.Status == ReservationStatus.Booked.ToString());
        }

        [Test]
        public void BookReservationFees_Test()
        {
            //Arrange
            unitOfWorkMocked.Setup(x => x.Insert(It.IsAny<Reservation>())).Returns<Reservation>(x => x);
            var reservationRepo = new ReservationRepo(unitOfWorkMocked.Object);

            var arrivalDate = DateTime.Now;
            var depatureDate = arrivalDate.AddDays(5);

            //Act
            var reservation = reservationRepo.Book(guest, room, arrivalDate, depatureDate);

            //Assert
            Assert.IsTrue(reservation.Fees == 5 * room.Rate * room.DepositFeePercentage / 100);
        }

        [Test]
        public void BookReservationCheckIn_Test()
        {
            //Arrange
            var reservationRepo = new ReservationRepo(unitOfWorkMocked.Object);

            var arrivalDate = DateTime.Now;
            var depatureDate = arrivalDate.AddDays(5);

            //Act
            var reservation = new Reservation() { Guest = guest, Room = room, Status = ReservationStatus.Booked.ToString() };
            var checkedIn = reservationRepo.CheckIn(reservation);

            //Assert
            Assert.IsTrue(checkedIn.Status == ReservationStatus.CheckedIn.ToString());
        }

        [Test]
        public void BookReservationCheckOut_Test()
        {
            //Arrange
            var reservationRepo = new ReservationRepo(unitOfWorkMocked.Object);

            var arrivalDate = DateTime.Now;
            var depatureDate = arrivalDate.AddDays(5);

            //Act
            var reservation = new Reservation() { Guest = guest, Room = room, Status = ReservationStatus.CheckedIn.ToString(), ArrivalDate = arrivalDate, DepartureDate = depatureDate };
            var checkedIn = reservationRepo.CheckOut(reservation, null);

            //Assert
            Assert.IsTrue(checkedIn.Status == ReservationStatus.CheckedOut.ToString());
            Assert.IsTrue(checkedIn.Fees == room.Rate * (depatureDate - arrivalDate).Days);
        }

        [Test]
        public void BookReservationCancel_Test()
        {
            //Arrange
            var reservationRepo = new ReservationRepo(unitOfWorkMocked.Object);

            var arrivalDate = DateTime.Now;
            var depatureDate = arrivalDate.AddDays(5);

            //Act
            var reservation = new Reservation() { Guest = guest, Room = room, Status = ReservationStatus.Canceled.ToString(), ArrivalDate = arrivalDate, DepartureDate = depatureDate };
            var checkedIn = reservationRepo.Cancel(reservation);

            //Assert
            Assert.IsTrue(checkedIn.Status == ReservationStatus.Canceled.ToString());
            Assert.IsTrue(checkedIn.Fees == room.Rate * room.CancellationFeeNightsCount);
        }
    }

}
