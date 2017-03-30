using HotelBooking.DAL;
using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.BLL
{
    public class BookingManager : IBookingManager
    {
        private IRepository<Booking> br;
        private IRepository<Room> rr;

        
        public BookingManager(IRepository<Booking> bookingRepository, IRepository<Room> roomRepository)
        {
            br = bookingRepository;
            rr = roomRepository;
        }

        //---------------------Create----------------------------
        public Booking CreateBooking(Booking booking)
        {
            int roomId = FindAvailableRoom(booking.StartDate, booking.EndDate);

            if (roomId >= 0)
            {
                booking.RoomId = roomId;
                booking.IsActive = true;
                Booking newBooking = br.Add(booking);
                return newBooking;
            }
            else
            {
                return null;
            }
        }

        public int FindAvailableRoom(DateTime startDate, DateTime endDate)
        {
            if (startDate <= DateTime.Today || startDate > endDate || endDate <= DateTime.Today)
                throw new ArgumentException("Start and end date cannot be set to before current date, and end date should not be later than start date");
            var romms = rr.GetAll();
            var books = br.GetAll();
            foreach (var room in rr.GetAll())
            {
                if (!br.GetAll().Any(
                        b => b.RoomId == room.Id && b.IsActive &&
                        (startDate >= b.StartDate && startDate <= b.EndDate ||
                        endDate >= b.StartDate && endDate <= b.EndDate || 
                        startDate <= b.StartDate && endDate >= b.EndDate)
                    ))
                {
                    return room.Id;
                }
            }
            return -1;
        }

        //-------------------Index------------------------------

        public List<DateTime> GetFullyOccupiedDates(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("The start date cannot be later than the end date.");

            List<DateTime> fullyOccupiedDates = new List<DateTime>();
            int noOfRooms = rr.GetAll().Count();
            var bookings = br.GetAll();

            if (bookings.Any())
            {
                for (DateTime d = startDate; d <= endDate; d = d.AddDays(1))
                {
                    var noOfBookings = from b in bookings
                                       where b.IsActive && d >= b.StartDate && d <= b.EndDate
                                       select b;
                    if (noOfBookings.Count() >= noOfRooms)
                        fullyOccupiedDates.Add(d);
                }
            }
            return fullyOccupiedDates;
        }

        public int YearToDisplay(int? id)
        {
                int minBookingYear = MinBookingDate().Year;
                int maxBookingYear = MaxBookingDate().Year;
                if (id <= minBookingYear)
                {
                    return minBookingYear;
                }                    
                else if (id >= maxBookingYear)
                {
                    return maxBookingYear;
                }                    
                else
                {
                    return DateTime.Today.Year;
                }                  

        }

        public DateTime MinBookingDate()
        {
            var bookingStartDates = br.GetAll().Select(b => b.StartDate);
            return bookingStartDates.Any() ? bookingStartDates.Min() : DateTime.MinValue;

        }

        public DateTime MaxBookingDate()
        {
            var bookingEndDates = br.GetAll().Select(b => b.EndDate);
            return bookingEndDates.Any() ? bookingEndDates.Max() : DateTime.MaxValue;

        }
    }
}