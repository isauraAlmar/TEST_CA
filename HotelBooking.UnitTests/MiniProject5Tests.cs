using HotelBooking.BLL;
using HotelBooking.DAL;
using HotelBooking.Models;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.UnitTests
{
    [TestFixture]
    public class MiniProject5Tests
    {
        [Test]
        public void FindAvailableRoom_StartdateLargerThanEnddate_ThrowException()
        {
            BookingManager manager = CreateBookingManager();
            DateTime start = DateTime.Today.AddDays(2);
            DateTime end = DateTime.Today.AddDays(1);

            var availableRoom = Assert.Throws<ArgumentException>(()
              => manager.FindAvailableRoom(start, end));
            StringAssert.Contains("Start and end date cannot be set to before current date, and the start date later than the end date", availableRoom.Message);
        }

        [Test]
        public void FindAvailableRoom_NoAvailableRoom_ReturnMinusOne()
        {
            BookingManager manager = CreateBookingManager();
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);

            int result = manager.FindAvailableRoom(start, end);
            int noRoomId = -1;
            Assert.AreEqual(noRoomId, result);
        }

        [Test]
        public void FindAvailableRoom_AtLeastOneAvailableRoom_ReturnGreaterThanMinusOne()
        {
            BookingManager manager = CreateBookingManager();
            DateTime start = DateTime.Today.AddDays(1);
            DateTime end = DateTime.Today.AddDays(2);

            int result = manager.FindAvailableRoom(start, end);
            int noRoomId = -1;
            Assert.IsTrue(result > noRoomId);
        }

        [Test]
        public void GetFullyOccupiedDates_StartdateLargerThanEnddate_ThrowException()
        {
            BookingManager manager = CreateBookingManager();
            DateTime start = DateTime.Today.AddDays(2);
            DateTime end = DateTime.Today.AddDays(1);

            var fullyOccupied = Assert.Throws<ArgumentException>(()
              => manager.GetFullyOccupiedDates(start, end));
            StringAssert.Contains("The start date cannot be later than the end date.", fullyOccupied.Message);
        }

        [Test]
        public void GetFullyOccupiedDates_NoBookings_ReturnEmptyList()
        {
            BookingManager manager = CreateBookingManager();
            DateTime start = DateTime.Today.AddDays(1);
            DateTime end = DateTime.Today.AddDays(2);

            List<DateTime> fullyOccupiedList = manager.GetFullyOccupiedDates(start, end);

            Assert.IsEmpty(fullyOccupiedList);
        }

        [Test]
        public void GetFullyOccupiedDates_MinimumOneRoomAvailable_ReturnEmptyList()
        {
            BookingManager manager = CreateBookingManager();
            DateTime start = DateTime.Today.AddDays(6);
            DateTime end = DateTime.Today.AddDays(9);

            List<DateTime> fullyOccupiedList = manager.GetFullyOccupiedDates(start, end);

            Assert.IsEmpty(fullyOccupiedList);
        }

        [Test]
        public void GetFullyOccupiedDates_AllRoomsOccupied_ReturnNoOfDates()
        {
            BookingManager manager = CreateBookingManager();
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);

            List<DateTime> fullyOccupiedList = manager.GetFullyOccupiedDates(start, end);
            int noOfDates = 11;
            Assert.AreEqual(noOfDates, fullyOccupiedList.Count());
        }


        private BookingManager CreateBookingManager()
        {
            DateTime FOstart = DateTime.Today.AddDays(10);
            DateTime FOend = DateTime.Today.AddDays(20);
            DateTime OFstart = DateTime.Today.AddDays(6);
            DateTime OFend = DateTime.Today.AddDays(9);

            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id=1, StartDate=FOstart, EndDate=FOend, IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { Id=2, StartDate=FOstart, EndDate=FOend, IsActive=true, CustomerId=2, RoomId=2 },
                new Booking { Id=2, StartDate=OFstart, EndDate=OFend, IsActive=true, CustomerId=2, RoomId=1 }
            };

            List<Room> rooms = new List<Room>
            {
                new Room { Id = 1 },
                new Room { Id = 2 }
            };

            // Fake BookingRepository using NSubstitute
            IRepository<Booking> bookingRepository = Substitute.For<IRepository<Booking>>();
            bookingRepository.GetAll().Returns(bookings);

            // Fake RoomRepository using NSubstitute
            IRepository<Room> roomRepository = Substitute.For<IRepository<Room>>();
            roomRepository.GetAll().Returns(rooms);

            return new BookingManager(bookingRepository, roomRepository);
        }
    }
}
