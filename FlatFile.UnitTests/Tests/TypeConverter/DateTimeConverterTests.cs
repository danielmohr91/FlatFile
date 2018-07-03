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
        public void Should_ConvertStringToDateTime_When_TypeConverterDefined()
        {
            var model = new DateTimeModel();
            var date = converter.GetConvertedValue("1/25/01", model.GetType().GetProperty("DateTime1"));
            Assert.AreEqual((DateTime) date, new DateTime(2001, 1, 25));
        }
    }
}