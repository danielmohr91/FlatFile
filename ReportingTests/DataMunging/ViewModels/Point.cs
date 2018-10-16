namespace DataMunging.Reporting.ViewModels
{
    public class Point
    {
        public Point(int id, int x, int y)
        {
            Id = id;
            X = x;
            Y = y;
        }

        // Parameterless constructor is required so model can be newed up when used as a generic type (e.g. in FlatFileImporter<T>)
        public Point()
        {
        }

        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}