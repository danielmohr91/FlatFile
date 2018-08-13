using FlatFileParserUnitTests.Models;
using FlatFileParserUnitTests.Tests.TypeConverter.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.FixedWidthFileWriter
{
    [TestClass]
    public class PrimitiveTypeExpectationsTest
    {
        [TestMethod]
        public void Should_TruncateFloatToEightDigits_When_GetTruncatedFloatingPointNumberIsCalled()
        {
            var logic = new PrimitiveTypeExpectations();
            var number = float.MaxValue;

            var test = new PrimitiveTypesModel
            {
                floatTest = logic.GetTruncatedFloatingPointNumber(number)
            };
            // TODO: resume here testing the GetTruncatedFloatingPointNumber on floats. Input file is incorrect. 
            Assert.AreEqual(number, test.floatTest);
        }

        [TestMethod]
        public void Should_TruncateDouble_When_GetTruncatedFloatingPointNumberIsCalled()
        {
            var logic = new PrimitiveTypeExpectations();
            var number = double.MaxValue;
            var doubleTest = logic.GetTruncatedFloatingPointNumber(number);

            Assert.AreEqual((double)1.7976931348E+308, doubleTest);
        }
    }
}