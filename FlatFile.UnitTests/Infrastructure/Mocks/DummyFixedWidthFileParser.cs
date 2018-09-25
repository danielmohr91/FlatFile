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

        public ICollection<TEntity> ParseFile()
        {
            return null;
        }
    }
}