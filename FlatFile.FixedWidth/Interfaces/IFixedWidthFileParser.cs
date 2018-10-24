using System.Collections.Generic;

namespace FlatFile.FixedWidth.Interfaces
{
    public interface IFixedWidthFileParser<T>
    {
        ICollection<T> ParseFile(bool ignoreFirstRow = false, bool ignoreBlankRows = false);
        ICollection<T> ParseFile(ITestForSkip testForSkip, bool ignoreFirstRow = false, bool ignoreBlankRows = false);
    }
}