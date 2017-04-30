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

        //||
        //startDate <= b.StartDate && endDate >= b.EndDate

        public int FindAvailableRoom(DateTime startDate, DateTime endDate)
        {
           /*3*/ if (startDate <= DateTime.Today || startDate > endDate || endDate <= DateTime.Today)
           /*4*/    throw new ArgumentException("Start and end date cannot be set to before current date, and the start date later than the end date");

           /*5*/ foreach (var room in rr.GetAll()) {
           /*6*/    if (!br.GetAll().Any(
                        b => b.RoomId == room.Id && b.IsActive &&
                        (startDate >= b.StartDate && startDate <= b.EndDate ||
                        endDate >= b.StartDate && endDate <= b.EndDate ||
                        startDate <= b.StartDate && endDate >= b.EndDate)
           /*11*/         )) {
           /*12*/            return room.Id;
           /*13*/         }
           /*14*/    }
           /*15*/ return -1;
        }

        //-------------------Index------------------------------
        public List<DateTime> GetFullyOccupiedDates(DateTime startDate, DateTime endDate)
        {
            /*3*/if (startDate > endDate)
            /*4*/    throw new ArgumentException("The start date cannot be later than the end date.");

            /*5*/List<DateTime> fullyOccupiedDates = new List<DateTime>();
            /*6*/int noOfRooms = rr.GetAll().Count();
            /*7*/var bookings = br.GetAll();

            /*8*/if (bookings.Any())
            /*9*/{
            /*10*/    for (DateTime d = startDate; d <= endDate; d = d.AddDays(1))
            /*11*/    {
            /*12*/        var noOfBookings = from b in bookings
            /*13*/                           where b.IsActive && d >= b.StartDate && d <= b.EndDate
            /*14*/                           select b;
            /*15*/        if (noOfBookings.Count() >= noOfRooms)
            /*16*/           fullyOccupiedDates.Add(d);
            /*17*/   }
            /*18*/}
            /*19*/return fullyOccupiedDates;
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