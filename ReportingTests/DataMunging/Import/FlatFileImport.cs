using System.Collections.Generic;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;

namespace DataMunging.Reporting.Import
{
    public class FlatFileImport<T> where T : new()
    {
        private readonly IFixedWidthFileParser<T> parser;
        private ICollection<T> rows;

        public FlatFileImport(string fileName, IFlatFileLayoutDescriptor<T> layout)
        {
            parser = new FixedWidthFileParser<T>(layout, fileName);
        }

        public ICollection<T> GetRows()
        {
            return rows ?? (rows = parser.ParseFile());
        }
    }
}