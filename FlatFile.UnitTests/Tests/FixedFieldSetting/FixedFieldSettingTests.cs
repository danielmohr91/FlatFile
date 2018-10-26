using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
            var layout = new LayoutDescriptor<DummyStringModel>();
            var parser = new FixedWidthFileParser<DummyStringModel>(layout, GetFilePath());
        }

        [TestMethod]
        public void Should_SkipHeaderRow_When_SkipHeaderSettingIsEnabled()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Should_TestForSkip_When_TestForSkipObjectIsDefined()
        {
            throw new NotImplementedException();
        }
        private string GetFilePath()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return $"{directory}\\InputFiles\\SettingsTest.dat"; // file properties should be "Content" and "Copy If Newer" (or similar)
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

    }
}
