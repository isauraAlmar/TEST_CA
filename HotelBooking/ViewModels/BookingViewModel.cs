using HotelBooking.BLL;
using HotelBooking.DAL;
using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.ViewModels
{
    public class BookingViewModel
    {      
        public List<Booking> bookings { get; set; }
        public List<DateTime> FullyOccupiedDates { get; set; }
        public int YeatToDisplay { get; set; }

        public string GetMonth(int month)
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month);
        }

    }
}