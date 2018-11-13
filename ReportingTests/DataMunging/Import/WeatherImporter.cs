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
    public class WeatherImporter : FlatFileImporter<DailyTemperature>
    {
        public WeatherImporter(string fileName) : base(fileName)
        {
        }

        public List<DailyTemperature> GetWeatherReport()
        {
            return GetRows().ToList();
        }

        public override IFlatFileLayoutDescriptor<DailyTemperature> GetLayout(string fileName)
        {
            var dirtyIntTypeConverter = new DirtyIntTypeConverter();
            var layout = new LayoutDescriptor<DailyTemperature>()
                .AppendField(x => x.DayId, 4)
                .AppendField(x => x.LowTemperature, 6, dirtyIntTypeConverter) // Max Temp
                .AppendField(x => x.HighTemperature, 6, dirtyIntTypeConverter) // Min Temp
                .WithSkipDefinition(new WeatherReportSkipDefinitions());
            return layout;
        }
    }
}