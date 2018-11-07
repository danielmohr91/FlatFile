using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataMunging.Reporting.TestForSkip;
using DataMunging.Reporting.TypeConverters;
using DataMunging.Reporting.ViewModels;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;

namespace DataMunging.Reporting.Import
{
    public class WeatherImporter : FlatFileImporter<Point>
    {
        public WeatherImporter(string fileName) : base(fileName)
        {
        }

        public List<Point> GetWeatherSpreads()
        {
            // Could skip definitions be added to LayoutDescriptor instead? 
            var testForSkip = new WeatherReportSkipDefinitions();
            return GetRows(testForSkip).ToList();
        }

        public override IFlatFileLayoutDescriptor<Point> GetLayout(string fileName)
        {
            var dirtyIntTypeConverter = new DirtyIntTypeConverter();
            var layout = new LayoutDescriptor<Point>()
                .AppendField(x => x.Id, 4)
                .AppendField(x => x.X, 6, dirtyIntTypeConverter) // Max Temp
                .AppendField(x => x.Y, 6, dirtyIntTypeConverter); // Min Temp
            return layout;
        }
    }
}