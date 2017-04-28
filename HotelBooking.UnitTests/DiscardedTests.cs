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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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


        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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
    }
}
