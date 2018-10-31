using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TestForSkip
{
    public class SkipBlankRows : ITestForSkip
    {
        public bool ShouldSkip(string row, int rowNumber)
        {
            return string.IsNullOrWhiteSpace(row);
        }
    }
}