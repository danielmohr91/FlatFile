using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TestForSkip
{
    public class SkipBlankAndFirstRows : ITestForSkip
    {
        public bool ShouldSkip(string row, int rowNumber)
        {
            return rowNumber == 0 || string.IsNullOrWhiteSpace(row);
        }
    }
}