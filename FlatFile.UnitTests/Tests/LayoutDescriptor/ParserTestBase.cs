using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.LayoutDescriptor
{
    public abstract class ParserTestBase<T> where T : new()
    {
        protected readonly int DefaultFieldWidth = 15;
        protected readonly ICollection<T> ParsedRows;
        private ICollection<T> expectedRows;

        protected ParserTestBase()
        {
            ParsedRows = ParseTestFile();
        }

        protected ICollection<T> ExpectedRows => expectedRows ?? (expectedRows = GetExpectedRows());


        protected void AssertAllRowsMatchExpected()
        {
            CollectionAssert.AreEqual((ICollection) ExpectedRows, (ICollection) ParsedRows);
        }

        protected void AssertFirstRowMatchesExpected()
        {
            // This is reference equals by default. Equals method is overriden in T to implement value equals vs. reference equals
            var expected = ExpectedRows.First();
            var actual = ParsedRows.First();

            // FAIL
            // floatTest
            //expected: -0.214748368
            //actual: -0.2147484
            Assert.AreEqual(expected, actual);
        }

        protected abstract ICollection<T> GetExpectedRows();
        protected abstract string GetFilePath();
        protected abstract IFlatFileLayoutDescriptor<T> GetLayout();

        private ICollection<T> ParseTestFile()
        {
            var parser = new FixedWidthFileParser<T>(GetLayout(), GetFilePath());
            return parser.ParseFile();
        }
    }
}