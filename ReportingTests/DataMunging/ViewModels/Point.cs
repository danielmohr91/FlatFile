namespace DataMunging.Reporting.ViewModels
{
    public class Point : IPoint
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

        public override bool Equals(object obj)
        {
            var point = obj as Point;
            return point != null &&
                   Id == point.Id &&
                   X == point.X &&
                   Y == point.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1426304211;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}