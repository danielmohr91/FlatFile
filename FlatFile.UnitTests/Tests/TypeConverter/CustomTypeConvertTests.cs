using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.CustomTypeConverters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.TypeConverter
{
    [TestClass]
    public class CustomTypeConvertTests

    {
        //private readonly IGenericTypeConverter converter;
        //private readonly IFlatFileLayoutDescriptor<Day> settings;
        private readonly System.ComponentModel.TypeConverter converter;

        public CustomTypeConvertTests()
        {
            converter = new EnumTypeConverter();
        }

        [TestMethod]
        [ExpectedException(typeof( System.NotSupportedException), "Cannot convert from System.String")]
        public void Should_NotConvertStringToEnum_When_ValidInvalidStringIsUsed()
        {
           converter.ConvertFromString("garbage");
        }

        [TestMethod]
        public void Should_ConvertStringToEnum_When_ValidAbbreviatedStringIsUsed()
        {
            Assert.AreEqual(Day.Sun, converter.ConvertFromString("Sun"));
        }

        [TestMethod]
        public void Should_ConvertStringToEnum_When_ValidShortStringIsUsed()
        {
            Assert.AreEqual(Day.Mon, converter.ConvertFromString("M"));
        }

        [TestMethod]
        public void Should_ConvertStringToEnum_When_ValidLongStringIsUsed()
        {
            Assert.AreEqual(Day.Sat, converter.ConvertFromString("Saturday"));
        }
    }
}