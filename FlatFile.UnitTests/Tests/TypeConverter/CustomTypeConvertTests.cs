﻿using FlatFileParserUnitTests.CustomTypeConverters;
using FlatFileParserUnitTests.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.TypeConverter
{
    [TestClass]
    public class CustomTypeConvertTests
    {
        private readonly FlatFile.FixedWidth.Interfaces.ITypeConverter<Day> converter;

        public CustomTypeConvertTests()
        {
            converter = new DummyEnumTypeConverter();
        }

        [TestMethod]
        public void Should_ConvertStringToEnum_When_ValidAbbreviatedStringIsUsed()
        {
            Assert.AreEqual(Day.Sun, converter.ConvertFromString("Sun"));
        }

        [TestMethod]
        public void Should_ConvertStringToEnum_When_ValidLongStringIsUsed()
        {
            Assert.AreEqual(Day.Sat, converter.ConvertFromString("Saturday"));
        }

        [TestMethod]
        public void Should_ConvertStringToEnum_When_ValidShortStringIsUsed()
        {
            Assert.AreEqual(Day.Mon, converter.ConvertFromString("M"));
        }

        [TestMethod]
        [ExpectedException(typeof(System.NotSupportedException), "Cannot convert from System.String")]
        public void Should_NotConvertStringToEnum_When_ValidInvalidStringIsUsed()
        {
            converter.ConvertFromString("garbage");
        }
    }
}