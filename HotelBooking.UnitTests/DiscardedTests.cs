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
    class DiscardedTests
    {
        //[Test]
        public void FindAvailableRoom_StartDateBeforeToday_ThrowsExceptionError()
        {
            BookingManager manager = CreateBookingManager();
            DateTime today = DateTime.Today;
            var ex = Assert.Throws<ArgumentException>(()
                => manager.FindAvailableRoom(today.AddDays(-1), today));
            StringAssert.Contains("Start and end date cannot be set to before current date, and end date should not be later than start date", ex.Message);
        }

        //[Test]
        public void FindAvailableRoom_EndDateBeforeToday_ThrowsExceptionError()
        {
            BookingManager manager = CreateBookingManager();
            DateTime today = DateTime.Today;
            var ex = Assert.Throws<ArgumentException>(()
                => manager.FindAvailableRoom(today, today.AddDays(-1)));
            StringAssert.Contains("Start and end date cannot be set to before current date, and end date should not be later than start date", ex.Message);
        }

       // [Test]
        public void CreateBooking_StartDatePlusOne_EndDatePlusOne()
        {
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.AddDays(1);
            Booking newBooking = new Booking
            {
                Id = 4,
                StartDate = startDate,
                EndDate = endDate,
                CustomerId = 1,
                RoomId = 1,
                IsActive = true
            };
            BookingManager bookingManager = CreateBookingManager(newBooking);
            var createdBooking = bookingManager.CreateBooking(newBooking);
            Assert.AreEqual(newBooking, createdBooking);
            Assert.IsInstanceOf<Booking>(createdBooking);
        }

       // [Test]
        public void CreateBooking_StartDateMinusOne_EndDatePlusOne()
        {
            DateTime startDate = DateTime.Today.AddDays(-1);
            DateTime endDate = DateTime.Today.AddDays(1);
            Booking newBooking = new Booking
            {
                Id = 4,
                StartDate = startDate,
                EndDate = endDate,
                CustomerId = 1,
                RoomId = 1,
                IsActive = true
            };
            BookingManager bookingManager = CreateBookingManager(newBooking);
            var createdBooking = Assert.Throws<ArgumentException>(() => bookingManager.CreateBooking(newBooking));
            StringAssert.Contains("Start and end date cannot be set to before current date, and end date should not be later than start date", createdBooking.Message);
        }

       // [Test]
        public void CreateBooking_StartDatePlusOne_EndDatePlusNine()
        {
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.AddDays(9);
            Booking newBooking = new Booking
            {
                Id = 4,
                StartDate = startDate,
                EndDate = endDate,
                CustomerId = 1,
                RoomId = 1,
                IsActive = true
            };
            BookingManager bookingManager = CreateBookingManager(newBooking);
            var createdBooking = bookingManager.CreateBooking(newBooking);
            Assert.AreEqual(newBooking, createdBooking);
        }

       // [Test]
        public void CreateBooking_StartDatePlusTwentyOne_EndDatePlusTwentyTwo()
        {
            DateTime startDate = DateTime.Today.AddDays(21);
            DateTime endDate = DateTime.Today.AddDays(22);
            Booking newBooking = new Booking
            {
                Id = 4,
                StartDate = startDate,
                EndDate = endDate,
                CustomerId = 1,
                RoomId = 1,
                IsActive = true
            };
            BookingManager bookingManager = CreateBookingManager(newBooking);
            var createdBooking = bookingManager.CreateBooking(newBooking);
            Assert.AreEqual(newBooking, createdBooking);
        }

        //[Test]
        public void CreateBooking_StartDatePlusNine_EndDatePlusTwentyOne()
        {
            DateTime startDate = DateTime.Today.AddDays(9);
            DateTime endDate = DateTime.Today.AddDays(21);
            Booking newBooking = new Booking
            {
                Id = 4,
                StartDate = startDate,
                EndDate = endDate,
                CustomerId = 1,
                RoomId = 1,
                IsActive = true
            };
            BookingManager bookingManager = CreateBookingManager(newBooking);
            var createdBooking = bookingManager.CreateBooking(newBooking);
            Assert.AreEqual(null, createdBooking);
            Assert.IsNull(createdBooking);
        }

        //[Test]
        public void CreateBooking_EndDateIsLargerThanStartDate_BookingIsCreated()
        {
            DateTime date = DateTime.Today.AddDays(1);
            DateTime date2 = DateTime.Today.AddDays(2);
            Booking newBooking = new Booking
            {
                Id = 4,
                StartDate = date,
                EndDate = date2,
                CustomerId = 1,
                RoomId = 1,
                IsActive = true
            };
            BookingManager manager = CreateBookingManager(newBooking);
            var createdBooking = manager.CreateBooking(newBooking);
            Assert.AreEqual(newBooking, createdBooking);
        }


       // [Test]
        public void CreateBooking_StartDateIsLargerThanEndDate_NotCreated()
        {
            DateTime date = DateTime.Today.AddDays(1);
            DateTime date2 = DateTime.Today.AddDays(2);
            Booking newBooking = new Booking
            {
                Id = 4,
                StartDate = date2,
                EndDate = date,
                CustomerId = 1,
                RoomId = 1,
                IsActive = true
            };
            BookingManager manager = CreateBookingManager(newBooking);
            var createdBooking = Assert.Throws<ArgumentException>(()
               => manager.CreateBooking(newBooking));
            StringAssert.Contains("Start and end date cannot be set to before current date, and end date should not be later than start date", createdBooking.Message);
        }

        //[Test]
        public void CreateBooking_StartEqualsEndDateMinInOccupied_NotCreated()
        {
            DateTime date = DateTime.Today.AddDays(10);
            DateTime date2 = DateTime.Today.AddDays(10);
            Booking newBooking = new Booking
            {
                Id = 4,
                StartDate = date2,
                EndDate = date,
                CustomerId = 1,
                RoomId = 1,
                IsActive = true
            };
            BookingManager manager = CreateBookingManager(newBooking);
            var createdBooking = manager.CreateBooking(newBooking);
            Assert.AreEqual(null, createdBooking);
        }

       // [Test]
        public void CreateBooking_DatesEqualsOccupiedDates_NotCreated()
        {
            DateTime date = DateTime.Today.AddDays(20);
            DateTime date2 = DateTime.Today.AddDays(20);
            Booking newBooking = new Booking
            {
                Id = 4,
                StartDate = date2,
                EndDate = date,
                CustomerId = 1,
                RoomId = 1,
                IsActive = true
            };
            BookingManager manager = CreateBookingManager(newBooking);
            var createdBooking = manager.CreateBooking(newBooking);
            Assert.AreEqual(null, createdBooking);
        }

        //[Test]
        public void CreateBooking_StartEqualsEndDateMaxInOccupied_NotCreated()
        {
            DateTime date = DateTime.Today.AddDays(20);
            DateTime date2 = DateTime.Today.AddDays(20);
            Booking newBooking = new Booking
            {
                Id = 4,
                StartDate = date2,
                EndDate = date,
                CustomerId = 1,
                RoomId = 1,
                IsActive = true
            };
            BookingManager manager = CreateBookingManager(newBooking);
            var createdBooking = manager.CreateBooking(newBooking);
            Assert.AreEqual(null, createdBooking);
        }

        private BookingManager CreateBookingManager(Booking booking)
        {
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);

            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id=1, StartDate=start, EndDate=end, IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { Id=2, StartDate=start, EndDate=end, IsActive=true, CustomerId=2, RoomId=2 }
            };

            List<Room> rooms = new List<Room>
            {
                new Room { Id = 1 },
                new Room { Id = 2 }
            };

            // Fake RoomRepository using NSubstitute
            IRepository<Room> roomRepository = Substitute.For<IRepository<Room>>();
            roomRepository.GetAll().Returns(rooms);

            // Fake BookingRepository using NSubstitute
            IRepository<Booking> bookingRepository = Substitute.For<IRepository<Booking>>();
            bookingRepository.Add(booking).Returns(booking);
            bookingRepository.GetAll().Returns(bookings);


            return new BookingManager(bookingRepository, roomRepository);
        }

        private BookingManager CreateBookingManager()
        {
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            DateTime LastYearStart = new DateTime(2016, 02, 02);
            DateTime LastYearEnd = new DateTime(2016, 02, 22);

            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id=1, StartDate=start, EndDate=end, IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { Id=2, StartDate=start, EndDate=end, IsActive=true, CustomerId=2, RoomId=2 },
                new Booking {Id=3, StartDate = LastYearStart, EndDate = LastYearEnd, IsActive=true, CustomerId = 3, RoomId = 3 },
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


        //[Test]
        //public void FindAvailableRoom_AtLeastOneAvailableRoom_ReturnsRoomId()
        //{
        //    BookingManager manager = CreateBookingManager();
        //    DateTime date = SystemTime.Today.AddDays(21);
        //    int roomId = manager.FindAvailableRoom(date, date);
        //    Assert.AreNotEqual(-1, roomId);
        //}

        //[Test]
        //public void FindAvailableRoom_NoAvailableRoom_ReturnsMinusOne()
        //{
        //    BookingManager manager = CreateBookingManager();
        //    DateTime date = SystemTime.Today.AddDays(10);
        //    int roomId = manager.FindAvailableRoom(date, date.AddDays(2));
        //    Assert.AreEqual(-1, roomId);
        //}

        //[Test]
        //public void GetFullyOccupiedDates_NoBookings_ReturnsEmptyList()
        //{
        //    BookingManager manager = CreateBookingManager();
        //    var date = SystemTime.Today;
        //    DateTime dateTest = date.AddDays(40);
        //    var myList = new List<DateTime>();
        //    var datesList = manager.GetFullyOccupiedDates(dateTest, dateTest.AddDays(1));
        //    Assert.AreEqual(myList, datesList);
        //}

        //[Test]
        //public void GetFullyOccupiedDates_NotAllRoomsOccupied_ReturnsEmptyList()
        //{
        //    BookingManager manager = CreateBookingManager();
        //    var date = SystemTime.Today;
        //    DateTime dateTest = date.AddDays(35);
        //    var myList = new List<DateTime>();
        //    var datesList = manager.GetFullyOccupiedDates(dateTest, dateTest.AddDays(1));
        //    Assert.AreEqual(myList, datesList);

        //}

        //[Test]
        //public void GetFullyOccupoedDates_NoAvailableRooms_ReturnsList()
        //{
        //    BookingManager manager = CreateBookingManager();
        //    var date = SystemTime.Today;
        //    DateTime dateTest = date.AddDays(10);
        //    var datesList = manager.GetFullyOccupiedDates(dateTest, dateTest.AddDays(2));
        //    Assert.AreEqual(3, datesList.Count());
        //}
    }
}
