using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.DAL
{
    // This class implements the Factory Class pattern. The class can thus be
    // used to perform dependency injection. The class does also implement the
    // Facade pattern, as it provides a single point of entry to the DAL layer.
    public static class RepositoriesFactory
    {
        private static IRepository<Booking> bookingRepository = new BookingRepository();
        private static IRepository<Room> roomRepository = new RoomRepository();
        private static IRepository<Customer> customerRepository = new CustomerRepository();

        public static IRepository<Booking> BookingRepository
        {
            get { return bookingRepository; }
            set { bookingRepository = value; }
        }

        public static IRepository<Room> RoomRepository
        {
            get { return roomRepository; }
            set { roomRepository = value; }
        }

        public static IRepository<Customer> CustomerRepository
        {
            get { return customerRepository; }
            set { customerRepository = value; }
        }
    }

}