using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual List<Booking> Bookings { get; set; }

    }
}