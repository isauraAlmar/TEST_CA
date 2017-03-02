using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelBooking.Models;
using HotelBooking.DAL;
using HotelBooking.BLL;
using HotelBooking.ViewModels;

namespace HotelBooking.Controllers
{
    public class BookingsController : Controller
    {
        private HotelBookingContext db = new HotelBookingContext();
        private static IRepository<Room> rr = new RoomRepository();
        private static IRepository<Customer> cr = new CustomerRepository();
        private static IRepository<Booking> br = new BookingRepository();
        private IBookingManager bm = new BookingManager();


        // GET: Bookings
        public ActionResult Index(int? id)
        {
            BookingViewModel bvm = new BookingViewModel
            {
                bookings = br.GetAll().ToList(),
                FullyOccupiedDates = bm.GetFullyOccupiedDates(bm.MinBookingDate(), bm.MaxBookingDate()),
                YeatToDisplay = bm.YearToDisplay(id)
            };

            return View(bvm);
        }

        // GET: Bookings/Details/5 THIS IS AWESOME
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StartDate,EndDate,CustomerId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                Booking newBooking = bm.CreateBooking(booking);
                if (newBooking != null)
                {
                    return RedirectToAction("Index");
                }
                    
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", booking.CustomerId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", booking.CustomerId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Description", booking.RoomId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StartDate,EndDate,IsActive,CustomerId,RoomId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", booking.CustomerId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "Description", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
