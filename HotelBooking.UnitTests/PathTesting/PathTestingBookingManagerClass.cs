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
    class PathTestingBookingManagerClass
    {
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



                                                     // FindAvailableRoom
        [Test]
        public void AtleastOnAvailableroomInFindAvailableRoom()
        {
            BookingManager bk = CreateBookingManager();
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.AddDays(2);
            int roomId = bk.FindAvailableRoom(startDate, endDate);
            Assert.AreNotEqual(-1, roomId);
        }
        [Test]
        public void NoAvailableRoomInFindAvailableRoomMethod()
        {
            BookingManager bk = CreateBookingManager();
            DateTime startDate = DateTime.Today.AddDays(10);
            DateTime endDate = DateTime.Today.AddDays(20);
            int roomId = bk.FindAvailableRoom(startDate, endDate);
            Assert.AreEqual(-1, roomId);
        }
        [Test]
        public void FindAvailableRoomStartDateisMoreThanEndDate()
        {
            BookingManager bk = CreateBookingManager();

            DateTime startDate = DateTime.Today.AddDays(2);
            DateTime endDate = DateTime.Today.AddDays(1);

            int roomId = bk.FindAvailableRoom(startDate, endDate);

            var availableRoom = Assert.Throws<ArgumentException>(()
             => bk.FindAvailableRoom(startDate, endDate));

            StringAssert.Contains("Start and end date cannot be set to before current date, and end date should not be later than start date", availableRoom.Message);

        }


                                                      //GetFullyOccupiedDates

        [Test]
        public void StartDateisMoreThanEndDateForGetFullyOccupiedDateException()
        {
            BookingManager bk = CreateBookingManager();
            DateTime start = DateTime.Today.AddDays(2);
            DateTime end = DateTime.Today.AddDays(1);

            var occupied = Assert.Throws<ArgumentException>(()
              => bk.GetFullyOccupiedDates(start, end));
            StringAssert.Contains("The start date cannot be later than the end date.", occupied.Message);
        }

        [Test]
        public void NoBookingsEmptyListGetFullyOccupiedDates()
        {
            BookingManager bk = CreateBookingManager();
            DateTime start = DateTime.Today.AddDays(1);
            DateTime end = DateTime.Today.AddDays(2);

            List<DateTime> occupiedList = bk.GetFullyOccupiedDates(start, end);

            Assert.IsEmpty(occupiedList);
        }

        [Test]
        public void AtleastOneRoomAvailableGetFullyOccupiedDates()
        {
            BookingManager bk = CreateBookingManager();
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(16);

            List<DateTime> fullyOccupiedList = bk.GetFullyOccupiedDates(start, end);

            Assert.IsEmpty(fullyOccupiedList);
        }

        [Test]
        public void AllRoomsOccupiedNoDatesInGetFullyOccupiedDates()
        {
            BookingManager bk = CreateBookingManager();
            DateTime start = DateTime.Today.AddDays(5);
            DateTime end = DateTime.Today.AddDays(15);

            List<DateTime> fullyOccupiedList = bk.GetFullyOccupiedDates(start, end);
            int noOfDates = 6;
            Assert.AreEqual(noOfDates, fullyOccupiedList.Count());
        }







        
    }
}