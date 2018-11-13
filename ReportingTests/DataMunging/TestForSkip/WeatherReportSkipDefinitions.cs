using FlatFile.FixedWidth.Interfaces;

namespace DataMunging.Reporting.TestForSkip
{
    public class WeatherReportSkipDefinitions : ITestForSkip
    {
        public bool ShouldSkip(string row, int rowNumber)
        {
            return rowNumber == 0 || row.StartsWith("  mo") || string.IsNullOrWhiteSpace(row);
        }
    }
}