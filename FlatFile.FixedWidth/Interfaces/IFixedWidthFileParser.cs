using System.Collections.Generic;

namespace FlatFile.FixedWidth.Interfaces
{
    public interface IFixedWidthFileParser<T>
    {
        ICollection<T> ParseFile();
        ICollection<T> ParseFile(ITestForSkip testForSkip);
    }
}