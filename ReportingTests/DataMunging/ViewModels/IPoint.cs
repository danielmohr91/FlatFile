namespace DataMunging.Reporting.ViewModels
{
    public interface IPoint
    {
        int Id { get; set; }
        int X { get; set; }
        int Y { get; set; }

        bool Equals(object obj);
        int GetHashCode();
    }
}