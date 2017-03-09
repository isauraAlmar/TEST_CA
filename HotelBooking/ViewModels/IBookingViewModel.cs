using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.ViewModels
{
    public interface IBookingViewModel
    {
        List<Booking> bookings { get; set; }
        List<DateTime> FullyOccupiedDates { get; set; }
        int YeatToDisplay { get; set; }

        string GetMonth(int month);
    }
}
