using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.CustomTypeConverters;
using FlatFileParserUnitTests.Enum;
using FlatFileParserUnitTests.Models;
using FlatFileParserUnitTests.Tests.LayoutDescriptor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlatFileParserUnitTests.Tests.ParseWithTypeConverter
{
    [TestClass]
    public class ParseWithCustomTypeConverterTests : ParserTestBase<DummyEnumModel>
    {
        private readonly ITypeConverter<Day> converter;

        public ParseWithCustomTypeConverterTests()
        {
            converter = new DummyEnumTypeConverter();
        }

        [TestMethod]
        public void Should_ConvertStringToEnum_When_TypeConverterDefined()
        {
            CollectionAssert.AllItemsAreInstancesOfType(
                ParsedRows
                    .Select(x => x.DayTest)
                    .ToList(),
                typeof(Day));
        }

        [TestMethod]
        public void Should_ParseAllFieldsMatchingExpected_When_ParseFileIsCalled()
        {
            AssertAllRowsMatchExpected();
        }

        protected override ICollection<DummyEnumModel> GetExpectedRows()
        {
            return new Collection<DummyEnumModel>
            {
                new DummyEnumModel
                {
                    Id = 1,
                    DayTest = Day.Sun,
                    StringTest = "Working"
                },
                new DummyEnumModel
                {
                    Id = 2,
                    DayTest = Day.Mon,
                    StringTest = "Working"
                },
                new DummyEnumModel
                {
                    Id = 3,
                    DayTest = Day.Tues,
                    StringTest = "Working"
                },
                new DummyEnumModel
                {
                    Id = 4,
                    DayTest = Day.Wed,
                    StringTest = "Working"
                }
            };
        }

        protected override string GetFilePath()
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return $"{directory}\\InputFiles\\EnumTest.dat";
        }

        protected override IFlatFileLayoutDescriptor<DummyEnumModel> GetLayout()
        {
            ITypeConverter<Day> enumConverter = new DummyEnumTypeConverter();
            return new LayoutDescriptor<DummyEnumModel>()
                .AppendField(x => x.Id, 2)
                .AppendField(x => x.DayTest, 10) // enumConverter)
                .AppendField(x => x.StringTest, 7);
        }
    }
}