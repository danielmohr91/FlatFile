using System;
using FlatFile.FixedWidth.Implementation.TypeConverters;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.TypeConverter
{
    [TestClass]
    public class DateTimeTest
    {
        private readonly ITypeConverter converter;

        public DateTimeTest()
        {
            throw new NotImplementedException("Implement DateTimeConverter");
//            converter = new GenericTypeConverterBase();
        }

        [TestMethod]
        public void Should_ConvertLongStringDateTimeToDateTime_When_DefaultTypeConverterIsUsed()
        {
            var model = new DateTimeModel();
            var date = converter.ConvertFromString("01-25-1991 14:30:15.123", model.GetType().GetProperty("DateTime1"));
            Assert.AreEqual((DateTime) date, new DateTime(1991, 1, 25, 14, 30, 15, 123));
        }

        [TestMethod]
        public void Should_ConvertShortStringDateTimeToDateTime_When_DefaultTypeConverterIsUsed()
        {
            var model = new DateTimeModel();
            var date = converter.ConvertFromString("1/25/01 14:30:00", model.GetType().GetProperty("DateTime1"));
            Assert.AreEqual((DateTime) date, new DateTime(2001, 1, 25, 14, 30, 0));
        }

        [TestMethod]
        public void Should_ConvertStringDateToDateTime_When_DefaultTypeConverterIsUsed()
        {
            var model = new DateTimeModel();
            var date = converter.ConvertFromString("1/25/01", model.GetType().GetProperty("DateTime1"));
            Assert.AreEqual((DateTime) date, new DateTime(2001, 1, 25));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "String was not recognized as a valid DateTime")]
        public void Should_ThrowFormatException_When_InvalidMonthIsParsed()
        {
            var model = new DateTimeModel();
            var date = converter.ConvertFromString("13-25-1991 14:30:15.123", model.GetType().GetProperty("DateTime1"));
        }
    }
}