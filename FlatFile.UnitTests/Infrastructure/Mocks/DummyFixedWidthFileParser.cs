using System.Collections.Generic;
using System.Collections.ObjectModel;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.Models;

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
            // Hardcoding a test set for now
            if (typeof(TEntity) == typeof(PrimitiveTypes))
            {
                var rows = new Collection<PrimitiveTypes>
                {
                    new PrimitiveTypes
                    {
                        id = 0,
                        charTest = 'a',
                        stringTest = "string1",
                        boolTest = true,
                        doubleTest = 42
                    },
                    new PrimitiveTypes
                    {
                        id = 1,
                        charTest = 'b',
                        stringTest = "string2",
                        boolTest = false,
                        doubleTest = 36
                    }
                };
                return (ICollection<TEntity>) rows;
            }

            return null;
        }
    }
}