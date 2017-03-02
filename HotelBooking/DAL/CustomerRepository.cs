using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelBooking.DAL
{
    public class CustomerRepository : IRepository<Customer>
    {
        public Customer Add(Customer entity)
        {
            using (var ctx = new HotelBookingContext())
            {
                var result = ctx.Customers.Add(entity);

                ctx.SaveChanges();

                return result;
            }
        }

        public void Delete(int id)
        {
            using (var ctx = new HotelBookingContext())
            {
                var cus = ctx.Customers.FirstOrDefault(x => x.Id == id);
                ctx.Customers.Remove(cus);
                ctx.SaveChanges();
            }
        }

        public Customer Get(int id)
        {
            using (var ctx = new HotelBookingContext())
            {
                return ctx.Customers.FirstOrDefault(c => c.Id == id);
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            using (var ctx = new HotelBookingContext())
            {
                return ctx.Customers.ToList();
            }
        }

        public Customer Update(Customer entity)
        {
            using (var ctx = new HotelBookingContext())
            {
                Customer customerDB = ctx.Entry(entity).Entity;

                if (customerDB == null)
                {
                    return null;
                }

                //var customerDB = ctx.Customers.FirstOrDefault(c => c.Id == customer.Id);
                customerDB.Email = entity.Email;
                customerDB.Name = entity.Name;

                ctx.Entry(customerDB).State = EntityState.Modified;
                ctx.SaveChanges();
                return customerDB;
            }
        }
    }
}