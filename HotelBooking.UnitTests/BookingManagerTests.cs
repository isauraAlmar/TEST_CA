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
    public class BookingManagerTests
    {

        [Test]
        public void FindAvailableRoom_RoomAvailable_RoomIdNotMinusOne()
        {
            BookingManager manager = CreateBookingManager();
            DateTime date = DateTime.Today.AddDays(5);

            int roomId = manager.FindAvailableRoom(date, date);

            Assert.AreNotEqual(-1, roomId);
        }

        [Test]
        public void FindAvailableRoom_StartDateBeforeToday_ThrowsExceptionError()
        {
            BookingManager manager = CreateBookingManager();
            DateTime today = DateTime.Today;
            var ex = Assert.Throws<ArgumentException>(()
                => manager.FindAvailableRoom(today.AddDays(-1), today));
            StringAssert.Contains("Error", ex.Message);
        }

        [Test]
        public void FindAvailableRoom_EndDateBeforeToday_ThrowsExceptionError()
        {
            BookingManager manager = CreateBookingManager();
            DateTime today = DateTime.Today;
            var ex = Assert.Throws<ArgumentException>(()
                => manager.FindAvailableRoom(today, today.AddDays(-1)));
            StringAssert.Contains("Error", ex.Message);
        }

        [Test]
        public void GetFullyOccupiedDates_NoOccupiedDates_ReturnsEmptyListOfDates()
        {
            BookingManager manager = CreateBookingManager();
            var date = DateTime.Today;
            DateTime dateTest = date.AddDays(35);
            var myList = new List<DateTime>();
            var datesList = manager.GetFullyOccupiedDates(dateTest, dateTest.AddDays(1));
            Assert.AreEqual(myList, datesList);
        }

        [Test]
        public void GetFullyOccupiedDates_StartDateIsLaterThanEndDate_ThrowsExceptionStartDateIsGreater()
        {
            BookingManager manager = CreateBookingManager();
            DateTime today = DateTime.Today;
            var ex = Assert.Throws<ArgumentException>(()
                => manager.GetFullyOccupiedDates(today.AddDays(1), today));
            Assert.That(ex.Message,
                Is.EqualTo("Error"));
        }
        /*
        private BookingManager CreateBookingManager()
        {
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            RepositoriesFactory.BookingRepository = new FakeBookingRepository(start, end);
            RepositoriesFactory.RoomRepository = new FakeRoomRepository();
            return new BookingManager();
        }*/

        private BookingManager CreateBookingManager()
        {
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);

            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id=1, StartDate=start, EndDate=end, IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { Id=2, StartDate=start, EndDate=end, IsActive=true, CustomerId=2, RoomId=2 },
            };
            List<Room> rooms = new List<Room>
            {
                new Room { Id = 1 },
                new Room { Id = 2 }
            };

            // Create a fake BookingRepository using NSubstitute
            IRepository<Booking> bookingRepository = Substitute.For<IRepository<Booking>>();
            // Set a return value for GetAll() 
            bookingRepository.GetAll().Returns(bookings);
            // Set a return value for Get() - not used
            bookingRepository.Get(2).Returns(bookings[1]);
            // Set a return value for Add() - not used
            bookingRepository.Add(Arg.Any<Booking>()).Returns(bookings[1]);

            // Create a fake RoomRepository using NSubstitute
            IRepository<Room> roomRepository = Substitute.For<IRepository<Room>>();
            // Set a return value for GetAll() 
            roomRepository.GetAll().Returns(rooms);


            RepositoriesFactory.BookingRepository = bookingRepository;
            RepositoriesFactory.RoomRepository = roomRepository;
            return new BookingManager();
        }
    }


}

