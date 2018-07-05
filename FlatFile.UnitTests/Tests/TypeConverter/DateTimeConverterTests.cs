using System;
using FlatFile.FixedWidth.Implementation.TypeConverters;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.LayoutDescriptor
{
    [TestClass]
    public class DateTimeTest
    {
        private readonly IGenericTypeConverter converter;

        public DateTimeTest()
        {
            converter = new GenericTypeConverter();
        }

        [TestMethod]
        public void Should_ConvertStringDateToDateTime_When_DefaultTypeConverterIsUsed()
        {
            var model = new DateTimeModel();
            var date = converter.GetConvertedValue("1/25/01", model.GetType().GetProperty("DateTime1"));
            Assert.AreEqual((DateTime) date, new DateTime(2001, 1, 25));
        }

        [TestMethod]
        public void Should_ConvertShortStringDateTimeToDateTime_When_DefaultTypeConverterIsUsed()
        {
            var model = new DateTimeModel();
            var date = converter.GetConvertedValue("1/25/01 14:30:00", model.GetType().GetProperty("DateTime1"));
            Assert.AreEqual((DateTime) date, new DateTime(2001, 1, 25, 14, 30, 0));
        }

        [TestMethod]
        public void Should_ConvertLongStringDateTimeToDateTime_When_DefaultTypeConverterIsUsed()
        {
            var model = new DateTimeModel();
            var date = converter.GetConvertedValue("01-25-1991 14:30:15.123", model.GetType().GetProperty("DateTime1"));
            Assert.AreEqual((DateTime)date, new DateTime(1991, 1, 25, 14, 30, 15, 123));
        }
        
        [TestMethod]
        [ExpectedException(typeof(FormatException), "String was not recognized as a valid DateTime")]
        public void Should_ThrowFormatException_When_InvalidMonthIsParsed()
        {
            var model = new DateTimeModel();
            var date = converter.GetConvertedValue("13-25-1991 14:30:15.123", model.GetType().GetProperty("DateTime1"));
        }
    }
}