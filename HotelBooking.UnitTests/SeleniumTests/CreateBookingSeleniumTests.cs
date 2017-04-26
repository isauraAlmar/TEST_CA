﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace HotelBooking.UnitTests.SeleniumTests
{
    [TestFixture]
    public class CreateBookingSeleniumTests
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost:1247/";
            verificationErrors = new StringBuilder();

        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        [TestCase("07-04-2017", "08-04-2017")]
        [TestCase("07-04-2017", "07-04-2017")]
        [TestCase("07-04-2017", "09-04-2017")]
        public void CreateBooking_ValidInput_ReturnHomePage(string startDate, string endDate)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl(baseURL + "/Bookings/Create");

            driver.FindElement(By.Id("StartDate")).SendKeys(startDate);

            driver.FindElement(By.Id("EndDate")).SendKeys(endDate);
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            String currentURL = driver.Url;
            Assert.AreEqual("http://localhost:1247/", currentURL);
        }

        [Test]
        [TestCase("9-04-2017", "10-04-2017")]
        [TestCase("9-04-2017", "20-04-2017")]
        [TestCase("9-04-2017", "21-04-2017")]
        [TestCase("10-04-2017", "20-04-2017")]
        [TestCase("10-04-2017", "10-04-2017")]
        [TestCase("20-04-2017", "20-04-2017")]
        [TestCase("20-04-2017", "21-04-2017")]
        public void CreateBooking_InvalidInOccupied_ReturnCreateBookingPage(string startDate, string endDate)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl(baseURL + "/Bookings/Create");

            driver.FindElement(By.Id("StartDate")).SendKeys(startDate);

            driver.FindElement(By.Id("EndDate")).SendKeys(endDate);
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            String currentURL = driver.Url;
            Assert.AreEqual(baseURL + "Bookings/Create", currentURL);
        }


        [Test]
        [TestCase("05-04-2017", "08-04-2017")]
        [TestCase("06-04-2017", "08-04-2017")]
        [TestCase("08-04-2017", "07-04-2017")]
        public void CreateBooking_InvalidInput_ThrowArgumentError(string startDate, string endDate)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl(baseURL + "/Bookings/Create");

            driver.FindElement(By.Id("StartDate")).SendKeys(startDate);

            driver.FindElement(By.Id("EndDate")).SendKeys(endDate);
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            String currentURL = driver.Url;
            Assert.AreEqual(baseURL + "Bookings/Create", currentURL);
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
