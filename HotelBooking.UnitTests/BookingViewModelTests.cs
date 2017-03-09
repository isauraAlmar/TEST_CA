using HotelBooking.BLL;
using HotelBooking.DAL;
using HotelBooking.Models;
using HotelBooking.ViewModels;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.UnitTests
{
    class BookingViewModelTests
    {
        [Test]
        public void GetMonth_2_Returnsfeb()
        {
            BookingViewModel model = CreateBookingViewModel();
            var feb = model.GetMonth(2);
            Assert.AreEqual("feb", feb);
        }

        [Test]
        public void DateIsOccupied_DateIsValid_NotOccupied()
        {
            BookingViewModel model = CreateBookingViewModel();
            var date = model.DateIsOccupied(2016, 3, 31);
            Assert.IsFalse(date);
        }

        [Test]
        public void DateIsOccupied_DateIsValid_IsOccupied()
        {
            BookingViewModel model = CreateBookingViewModel();
            var date = DateTime.Today.AddDays(15);
            var testDate = model.DateIsOccupied(date.Year, date.Month, date.Day);
            Assert.IsTrue(testDate);
        }

        private BookingViewModel CreateBookingViewModel()
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
            bookingRepository.GetAll().Returns(bookings);
            bookingRepository.Get(2).Returns(bookings[1]);

            // Create a fake RoomRepository using NSubstitute
            IRepository<Room> roomRepository = Substitute.For<IRepository<Room>>();
            roomRepository.GetAll().Returns(rooms);

            var manager = new BookingManager(bookingRepository, roomRepository);

            return new BookingViewModel();
        }
    }
}
