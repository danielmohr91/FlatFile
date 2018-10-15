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

        public override IFlatFileLayoutDescriptor<Point> GetLayout(string fileName)
        {
            var dirtyIntTypeConverter = new DirtyIntTypeConverter();
            var layout = new LayoutDescriptor<Point>()
                .AppendField(x => x.Id, 4)
                .AppendField(x => x.X, 6, dirtyIntTypeConverter)  // Max Temp
                .AppendField(x => x.Y, 6, dirtyIntTypeConverter); // Min Temp
            return layout;
        }
    }
}
