using System.Collections.Generic;
using FlatFile.FixedWidth.Interfaces;

namespace FlatFileParserUnitTests.Infrastructure.Mocks
{
    public class DummyFixedWidthFileParser<TEntity> : IFixedWidthFileParser<TEntity> where TEntity : new()
    {
        private IFlatFileLayoutDescriptor<TEntity> layout;

        public DummyFixedWidthFileParser(IFlatFileLayoutDescriptor<TEntity> layout)
        {
            this.layout = layout;
        }

        // Lee - would 'GetParsedFile' be a better method name? 
        public ICollection<TEntity> ParseFile()
        {
            return null;
        }
    }
}