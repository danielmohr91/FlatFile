namespace FlatFile.FixedWidth.Interfaces
{
    public interface ITestForSkip
    {
        bool ShouldSkip(string row, int rowNumber);

        /*
         * These are both unecessary now, since ShouldSkip test above handles these. Probably removing these...
         */
        //bool SkipFirstRow { get; set; }
        //bool SkipBlankRows { get; set; }
    }
}