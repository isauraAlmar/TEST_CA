using HotelBooking.BLL;
using HotelBooking.DAL;
using HotelBooking.Models;
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

        private BookingManager CreateBookingManager()
        {
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            RepositoriesFactory.BookingRepository = new FakeBookingRepository(start, end);
            RepositoriesFactory.RoomRepository = new FakeRoomRepository();
            return new BookingManager();
        }
    }
}
