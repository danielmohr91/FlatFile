using FlatFileParserUnitTests.Models;
using FlatFileParserUnitTests.Tests.TypeConverter.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.FixedWidthFileWriter
{
    [TestClass]
    internal class PrimitiveTypeExpectationsTest
    {
        [TestMethod]
        public void _____________Should_TruncateFloatToTenDigits_When_GetTruncatedFloatingPointNumberIsCalled()
        {
            var logic = new PrimitiveTypeExpectations();
            var number = float.MaxValue;

            var test = new PrimitiveTypesModel
            {
                floatTest = logic.GetTruncatedFloatingPointNumber(number)
            };
            // TODO: resume here testing the GetTruncatedFloatingPointNumber on floats. INput file is incorrect. 
            Assert.AreEqual(number, test.floatTest);
        }
    }
}