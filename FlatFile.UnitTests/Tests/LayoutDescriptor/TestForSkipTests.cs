using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Implementation.TestForSkip;
using FlatFileParserUnitTests.Models;
using FlatFileParserUnitTests.Tests.RowSkippers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.LayoutDescriptor
{
    [TestClass]
    public class TestForSkipTests
    {
        [TestMethod]
        public void Should_SkipBlankRows_When_BlankRowSettingIsEnabled()
        {
            // Arrange
            var layout = new LayoutDescriptor<DummyStringModel>()
                .AppendField(x => x.Id, 5)
                .AppendField(x => x.Field1, 15)
                .AppendField(x => x.Field2, 15)
                .AppendField(x => x.Field3, 15)
                .WithSkipDefinition(new SkipBlankRows());
            var parser = new FixedWidthFileParser<DummyStringModel>(layout, GetFilePath("SkipBlankRows.dat"));

            // Act
            var model = parser.ParseFile();

            // Assert
            CollectionAssert.AreEqual(GetExpectedRows().ToList(), model.ToList());
        }

        [TestMethod]
        public void Should_SkipHeaderRow_When_SkipHeaderSettingIsEnabled()
        {
            // Arrange
            var layout = new LayoutDescriptor<DummyStringModel>()
                .AppendField(x => x.Id, 5)
                .AppendField(x => x.Field1, 15)
                .AppendField(x => x.Field2, 15)
                .AppendField(x => x.Field3, 15)
                .WithSkipDefinition(new SkipFirstRow());
            var parser = new FixedWidthFileParser<DummyStringModel>(layout, GetFilePath("SkipHeaderRow.dat"));

            // Act
            var model = parser.ParseFile();

            // Assert
            CollectionAssert.AreEqual(GetExpectedRows().ToList(), model.ToList());
        }

        [TestMethod]
        public void Should_SkipIgnoredField_When_AppendIgnoredFieldIsUsed()
        {
            // Arrange
            var layout = new LayoutDescriptor<DummyStringModel>()
                .AppendField(x => x.Id, 5)
                .AppendField(x => x.Field1, 15)
                .AppendField(x => x.Field2, 15)
                .AppendIgnoredField(30)
                .AppendField(x => x.Field3, 15);
            var parser = new FixedWidthFileParser<DummyStringModel>(layout, GetFilePath("SkipColumns.dat"));

            // Act
            var model = parser.ParseFile();

            // Assert
            CollectionAssert.AreEqual(GetExpectedRows().ToList(), model.ToList());
        }

        [TestMethod]
        public void Should_SkipAllDefinedRows_When_CollectionOfSkipDefinitionsIsUsed()
        {
            // Arrange
            var layout = new LayoutDescriptor<DummyStringModel>()
                .AppendField(x => x.Id, 5)
                .AppendField(x => x.Field1, 15)
                .AppendField(x => x.Field2, 15)
                .AppendField(x => x.Field3, 15)
                .WithSkipDefinition(new SkipBlankRows())
                .WithSkipDefinition(new SkipFirstRow()); 
            var parser = new FixedWidthFileParser<DummyStringModel>(layout, GetFilePath("SkipMultiple.dat"));

            // Act
            var model = parser.ParseFile();

            // Assert
            CollectionAssert.AreEqual(GetExpectedRows().ToList(), model.ToList());
        }

        [TestMethod]
        public void Should_TestForSkip_When_TestForSkipObjectIsDefined()
        {
            // Arrange
            var layout = new LayoutDescriptor<DummyStringModel>()
                .WithSkipDefinition(new DummyRowSkipper())
                .AppendField(x => x.Id, 5)
                .AppendField(x => x.Field1, 15)
                .AppendField(x => x.Field2, 15)
                .AppendField(x => x.Field3, 15);
            var parser = new FixedWidthFileParser<DummyStringModel>(layout, GetFilePath("TestForSkip.dat"));

            // Act
            var model = parser.ParseFile();

            // Assert
            CollectionAssert.AreEqual(GetExpectedRows().ToList(), model.ToList());
        }

        private ICollection<DummyStringModel> GetExpectedRows(bool includeHeader = false)
        {
            ICollection<DummyStringModel> generatedRows = new Collection<DummyStringModel>();

            if (includeHeader)
            {
                generatedRows.Add(new DummyStringModel
                {
                    Id = "ID",
                    Field1 = "Field1",
                    Field2 = "Field2",
                    Field3 = "Field3"
                });
            }

            for (var i = 0; i < 20; i++)
            {
                var rowNumber = i + 1;

                generatedRows.Add(new DummyStringModel
                {
                    Id = rowNumber.ToString(),
                    Field1 = $"Row{rowNumber}Field1",
                    Field2 = $"Row{rowNumber}Field2",
                    Field3 = $"Row{rowNumber}Field3"
                });
            }

            return generatedRows;
        }

        private string GetFilePath(string fileName)
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return $"{directory}\\InputFiles\\SettingsTests\\{fileName}"; // file properties should be "Content" and "Copy If Newer" (or similar)
        }
    }
}
