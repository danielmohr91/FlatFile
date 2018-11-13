using System.IO;
using System.Linq;

namespace DataMunging.Reporting.Transformations
{
    public class SanitizeFile
    {
        public void SaveCopyWithFilteredRows(string inputFile, string outputFile, string excludeRowsStartingWith)
        {
            var allowedLines = File.ReadLines(inputFile)
                .Where(x => !x.StartsWith(excludeRowsStartingWith)).ToList();

            File.WriteAllLines(outputFile, allowedLines);
        }
    }
}