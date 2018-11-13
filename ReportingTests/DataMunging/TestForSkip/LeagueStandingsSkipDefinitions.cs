using FlatFile.FixedWidth.Interfaces;

namespace DataMunging.Reporting.TestForSkip
{
    public class LeagueStandingsSkipDefinitions : ITestForSkip
    {
        public bool ShouldSkip(string row, int rowNumber)
        {
            return rowNumber == 0 || row.StartsWith("   --") || string.IsNullOrWhiteSpace(row);
        }
    }
}