using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.Models;
using FlatFileParserUnitTests.Tests.LayoutDescriptor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.FixedWidthParser
{
    [TestClass]
    public class StringTest : ParserTestBase<DummyStringModel>
    {
        protected override ICollection<DummyStringModel> GetExpectedRows()
        {
            ICollection<DummyStringModel> generatedRows = new Collection<DummyStringModel>();
            for (var i = 0; i < 250; i++)
            {
                var rowNumber = i + 1;
               
                generatedRows.Add(new DummyStringModel
                {
                    Id = rowNumber.ToString().PadLeft(FieldWidth),
                    Field1 = $"Row{rowNumber}Field1",
                    Field2 = $"Row{rowNumber}Field2",
                    Field3 = $"Row{rowNumber}Field3",
                    Field4 = $"Row{rowNumber}Field4"
                });
            }

            return generatedRows;
        }

        [TestMethod]
        public void Should_Read250Rows_When_InputFileHas250Rows()
        {
            Assert.AreEqual(ParsedRows.Count, 250);
        }

        [TestMethod]
        public void Should_ParseFirstRowMatchingExpected_When_ParseFileIsCalled()
        {
            AssertFirstRowMatchesExpected();
        }

        [TestMethod]
        public void Should_ParseAllFieldsMatchingExpected_When_ParseFileIsCalled()
        {
            AssertAllRowsMatchExpected();
        }

        protected override string GetFilePath()
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return $"{directory}\\InputFiles\\StringTest.dat"; // file properties should be "Content" and "Copy If Newer" (or similar)
        }

        protected override IFlatFileLayoutDescriptor<DummyStringModel> GetLayout()
        {
            return new LayoutDescriptor<DummyStringModel>()
                .AppendField(x => x.Id, FieldWidth)
                .AppendField(x => x.Field1, FieldWidth)
                .AppendField(x => x.Field2, FieldWidth)
                .AppendField(x => x.Field3, FieldWidth)
                .AppendField(x => x.Field4, FieldWidth);
        }
    }
}