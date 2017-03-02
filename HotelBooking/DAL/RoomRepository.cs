using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelBooking.DAL
{
    public class RoomRepository : IRepository<Room>
    {
        public Room Add(Room entity)
        {
            using (HotelBookingContext ctx = new HotelBookingContext())
            {
                Room newRoom = ctx.Rooms.Add(entity);
                ctx.SaveChanges();
                return newRoom;
            }
        }

        public Room Update(Room entity)
        {
            using (HotelBookingContext ctx = new HotelBookingContext())
            {
                Room roomDB = ctx.Entry(entity).Entity;

                if (roomDB == null)
                {
                    return null;
                }

                roomDB.Description = entity.Description;

                ctx.Entry(roomDB).State = EntityState.Modified;
                ctx.SaveChanges();
                return roomDB;
            }
        }

        public Room Get(int id)
        {
            using (HotelBookingContext ctx = new HotelBookingContext())
            {
                return ctx.Rooms.FirstOrDefault(r => r.Id == id);
            }
        }

        public IEnumerable<Room> GetAll()
        {
            using (HotelBookingContext ctx = new HotelBookingContext())
            {
                return ctx.Rooms.ToList();
            }
        }

        public void Delete(int id)
        {
            using (var ctx = new HotelBookingContext())
            {
                var cus = ctx.Rooms.FirstOrDefault(x => x.Id == id);
                ctx.Rooms.Remove(cus);
                ctx.SaveChanges();
            }
        }
    }
}