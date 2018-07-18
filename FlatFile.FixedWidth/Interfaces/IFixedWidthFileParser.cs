using System.Collections.Generic;

namespace FlatFile.FixedWidth.Interfaces
{
    public interface IFixedWidthFileParser<T>
    {
        ICollection<T> ParseFile();
    }
}