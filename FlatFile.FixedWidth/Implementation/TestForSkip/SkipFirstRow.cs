using FlatFile.FixedWidth.Interfaces;

namespace FlatFile.FixedWidth.Implementation.TestForSkip
{
    public class SkipFirstRow : ITestForSkip
    {
        public bool ShouldSkip(string row, int rowNumber)
        {
            return rowNumber == 0;
        }
    }
}