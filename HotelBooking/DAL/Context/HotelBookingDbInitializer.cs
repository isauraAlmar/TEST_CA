using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HotelBooking.Models;


namespace HotelBooking.DAL
{
    public class HotelBookingDbInitializer : DropCreateDatabaseAlways<HotelBookingContext>
    {
        private List<Customer> customers = new List<Customer>
        {
            new Customer { Id=1, Name="John Smith", Email="js@gmail.com" },
            new Customer { Id=2, Name="Jane Doe", Email="jd@gmail.com" }
        };

        private List<Room> rooms = new List<Room>
        {
            new Room { Id=1, Description="A" },
            new Room { Id=2, Description="B" },
            new Room { Id=3, Description="C" }
        };

        protected override void Seed(HotelBookingContext context)
        {
            DateTime date = new DateTime(2017, 2, 4);
            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id=1, StartDate=date, EndDate=date.AddDays(14), IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { Id=2, StartDate=date, EndDate=date.AddDays(14), IsActive=true, CustomerId=2, RoomId=2 },
                new Booking { Id=3, StartDate=date, EndDate=date.AddDays(14), IsActive=true, CustomerId=1, RoomId=3 }
            };

            context.Customers.AddRange(customers);
            context.Rooms.AddRange(rooms);
            context.Bookings.AddRange(bookings);

            base.Seed(context);
        }

    }
}