using HotelBooking.BLL;
using HotelBooking.DAL;
using HotelBooking.Models;
using HotelBooking.ViewModels;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.UnitTests
{
    class BookingViewModelTests
    {
        [Test]
        public void GetMonth_Insert2ForFeb_Returnsfeb()
        {
            BookingViewModel model = CreateBookingViewModel();
            var feb = model.GetMonth(2);
            Assert.AreEqual("feb", feb);
        }

        [Test]
        public void GetMonth_Insert3ForMarch_Returnsfeb()
        {
            BookingViewModel model = CreateBookingViewModel();
            var result = model.GetMonth(3);
            Assert.AreEqual("mar", result);
        }

        [Test]
        public void GetMonth_Insert13ForInvalidMonth_ReturnsEmptyString()
        {
            BookingViewModel model = CreateBookingViewModel();
            var result = model.GetMonth(13);
            Assert.AreEqual("", result);
        }

        private BookingViewModel CreateBookingViewModel()
        {
            return new BookingViewModel();
        }
    }
}
