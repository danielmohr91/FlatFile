using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using FlatFile.FixedWidth.Implementation;
using FlatFileParserUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.FixedFieldSetting
{
    [TestClass]
    public class FixedFieldSettingTests
    {
        [TestMethod]
        public void Should_SkipBlankRows_When_BlankRowSettingIsEnabled()
        {
            // Arrange
            var layout = new LayoutDescriptor<DummyStringModel>()
                .AppendField(x => x.Id, 5)
                .AppendField(x => x.Field1, 15)
                .AppendField(x => x.Field2, 15)
                .AppendField(x => x.Field3, 15);
            var parser = new FixedWidthFileParser<DummyStringModel>(layout, GetFilePath("SkipBlankRows.dat"));

            // Act
            var model = parser.ParseFile(null, false, true); // Skip blank rows

            // Assert
            var expected = GetExpectedRows().ToList();
            var actual = model.ToList();
            CollectionAssert.AreEqual(GetExpectedRows().ToList(), model.ToList());
        }

        [TestMethod]
        public void Should_SkipHeaderRow_When_SkipHeaderSettingIsEnabled()
        {
            throw new NotImplementedException("Test SkipHeaderRow.dat");
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
        public void Should_TestForSkip_When_TestForSkipObjectIsDefined()
        {
            throw new NotImplementedException("TestForSkip.dat");
        }

        private ICollection<DummyStringModel> GetExpectedRows()
        {
            ICollection<DummyStringModel> generatedRows = new Collection<DummyStringModel>();
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