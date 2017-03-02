using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.BLL
{
    public interface IBookingManager
    {
        //---------Create----------
        Booking CreateBooking(Booking booking);
        int FindAvailableRoom(DateTime startDate, DateTime endDate);

        //--------Index--------------
        List<DateTime> GetFullyOccupiedDates(DateTime startDate, DateTime endDate);
        int YearToDisplay(int? id);
        DateTime MinBookingDate();
        DateTime MaxBookingDate();

    }
}
