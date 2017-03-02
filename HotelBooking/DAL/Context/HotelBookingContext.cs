using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelBooking.DAL
{
    public class HotelBookingContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public HotelBookingContext() : base("HotelBookingContext")
        {
        }

        public System.Data.Entity.DbSet<HotelBooking.Models.Booking> Bookings { get; set; }

        public System.Data.Entity.DbSet<HotelBooking.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<HotelBooking.Models.Room> Rooms { get; set; }
    }
}
