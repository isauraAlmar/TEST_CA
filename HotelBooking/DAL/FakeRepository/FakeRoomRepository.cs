using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.DAL
{
    public class FakeRoomRepository : IRepository<Room>
    {
        public Room Add(Room entity)
        {
            Room room = new Room
            {
                Id = 1,
                Description = "This is a nice room"
            };
            return room;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Room Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> GetAll()
        {
            List<Room> rooms = new List<Room>
            {
                new Room { Id = 1, Description = "Room 1 is nice" },
                new Room { Id = 2, Description = "Room 2 is nice" }
            };
            return rooms;
        }

        public Room Update(Room entity)
        {
            throw new NotImplementedException();
        }
    }
}