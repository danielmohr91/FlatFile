namespace DataMunging.Reporting.ViewModels
{
    public class DailyTemperature : IDailyTemperature
    {
        public DailyTemperature(int dayId, int lowTemperature, int highTemperature)
        {
            DayId = dayId;
            LowTemperature = lowTemperature;
            HighTemperature = highTemperature;
        }

        // Parameterless constructor is required so model can be newed up when used as a generic type (e.g. in FlatFileImporter<T>)
        public DailyTemperature()
        {
        }

        public int DayId { get; set; }
        public int LowTemperature { get; set; }
        public int HighTemperature { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DailyTemperature point &&
                   DayId == point.DayId &&
                   LowTemperature == point.LowTemperature &&
                   HighTemperature == point.HighTemperature;
        }

        public override int GetHashCode()
        {
            var hashCode = 1426304211;
            hashCode = hashCode * -1521134295 + DayId.GetHashCode();
            hashCode = hashCode * -1521134295 + LowTemperature.GetHashCode();
            hashCode = hashCode * -1521134295 + HighTemperature.GetHashCode();
            return hashCode;
        }
    }
}