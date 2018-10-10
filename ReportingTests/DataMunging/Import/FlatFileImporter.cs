using System.Collections.Generic;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;

namespace DataMunging.Reporting.Import
{
    public abstract class FlatFileImporter<T> where T : new()
    {
        private readonly IFixedWidthFileParser<T> parser;
        private ICollection<T> rows;

        protected FlatFileImporter(string fileName)
        {
            parser = new FixedWidthFileParser<T>(GetLayout(fileName), fileName);
        }

        public ICollection<T> GetRows()
        {
            // TODO: Resume here - Input string is not in correct format. 
            // Need custom type converter because the first row is all strings (for column headers), and the last row has totals. 
            // Also, rows in between are not consistently formatted as numerical values, some have asterisks denoting outliers.
            // Next steps: 
            //     - Update FlatFileParser with option to ignore n number of rows starting off
            //     - Update FlatFileParser with option to ignore n number of rows at end
            //     - Update FlatFileParser with mechanism for custom parsing numbers - DONE - will implement a custom type converter that strips out asterisks. 
            // Updating FlatFileParser to ignore lines would require changing the line by line reading from a stream. Will use a type converter to read in invalid lines as null instead. 
            // Will consider adding in an option to ignore header row, and ignore blank rows
            return rows ?? (rows = parser.ParseFile(true, true));
        }

        public abstract IFlatFileLayoutDescriptor<T> GetLayout(string fileName);
    }
}