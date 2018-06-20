using System.Collections.Generic;

namespace FlatFile.FixedWidth.Interfaces
{
    public interface IFixedWidthFileParser<TEntity, TFile>
    {
        TFile ParseFile();
    }
}