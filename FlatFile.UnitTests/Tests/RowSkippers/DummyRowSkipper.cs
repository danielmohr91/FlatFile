using FlatFile.FixedWidth.Interfaces;

namespace FlatFileParserUnitTests.Tests.RowSkippers
{
    public class DummyRowSkipper : ITestForSkip
    {
        public bool ShouldSkip(string row)
        {
            return row.StartsWith("thisrowshouldbeskipped");
        }
    }
}