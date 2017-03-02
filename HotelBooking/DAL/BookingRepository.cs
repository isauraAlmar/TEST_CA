using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace HotelBooking.DAL
{
    public class BookingRepository : IRepository<Booking>
    {
        public Booking Add(Booking entity)
        {
            using (var ctx = new HotelBookingContext())
            {
                var result = ctx.Bookings.Add(entity);

                ctx.SaveChanges();

                return result;
            }
        }

        public void Delete(int id)
        {
            using (var ctx = new HotelBookingContext())
            {
                var cus = ctx.Bookings.FirstOrDefault(x => x.Id == id);
                ctx.Bookings.Remove(cus);
                ctx.SaveChanges();
            }
        }

        public Booking Get(int id)
        {
            using (var ctx = new HotelBookingContext())
            {
                return ctx.Bookings.FirstOrDefault(c => c.Id == id);
            }
        }

        public IEnumerable<Booking> GetAll()
        {
            using (var ctx = new HotelBookingContext())
            {
                return ctx.Bookings.Include(b => b.Customer).Include(b => b.Room).ToList();
            }
        }

        public Booking Update(Booking entity)
        {
            using (var ctx = new HotelBookingContext())
            {
                Booking bookingDB = ctx.Entry(entity).Entity;

                if (bookingDB == null)
                {
                    return null;
                }

                //var bookingDB = ctx.Bookings.FirstOrDefault(c => c.Id == booking.Id);
                bookingDB.IsActive = entity.IsActive;
                bookingDB.StartDate = entity.StartDate;
                bookingDB.EndDate = entity.EndDate;
                bookingDB.RoomId = entity.RoomId;
                bookingDB.CustomerId = entity.CustomerId;

                ctx.Entry(bookingDB).State = EntityState.Modified;
                ctx.SaveChanges();
                return bookingDB;
            }
        }
    }
}