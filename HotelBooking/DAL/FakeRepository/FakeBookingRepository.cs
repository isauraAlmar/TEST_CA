using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.DAL
{
    public class FakeBookingRepository : IRepository<Booking>
    {
        private DateTime fullyOccupiedStartDate;
        private DateTime fullyOccupiedEndDate;

        public FakeBookingRepository(DateTime start, DateTime end)
        {
            fullyOccupiedStartDate = start;
            fullyOccupiedEndDate = end;
        }

        public Booking Add(Booking entity)
        {
            Booking booking = new Booking
            {
                Id = 1,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                IsActive = true,
                CustomerId = entity.CustomerId,
                RoomId = 1
            };
            return booking;
        }

        public IEnumerable<Booking> GetAll()
        {
            List<Booking> bookings = new List<Booking>
           {
               new Booking { Id=1, StartDate=fullyOccupiedStartDate, EndDate=fullyOccupiedEndDate, IsActive=true, CustomerId=1, RoomId=1 },
               new Booking { Id=2, StartDate=fullyOccupiedStartDate, EndDate=fullyOccupiedEndDate, IsActive=true, CustomerId=2, RoomId=2 },
           };
            return bookings;
        }

        public Booking Get(int id)
        {
            throw new NotImplementedException();
        }

        public Booking Update(Booking entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }

}