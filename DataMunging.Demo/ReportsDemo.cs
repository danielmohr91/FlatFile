using System;
using System.IO;
using System.Reflection;
using DataMunging.Reporting.Import;
using DataMunging.Reporting.Reports;
using DataMunging.Reporting.ViewModels;

namespace DataMunging.Demo
{
    internal class ReportsDemo
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(GetWeatherReport());
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static string GetWeatherReport()
        {
            var weatherData = new WeatherImporter(GetOriginalImportFilePath());
            var report = new WeatherReport<IDailyTemperature>(weatherData.GetWeatherReport());

            var day = report.GetDayWithSmallestSpread();

            return $"Day with smallest temperature difference\n\tDay #: {day.DayId}\n\tHigh: {day.HighTemperature}\n\tLow: {day.LowTemperature}";
        }

        private static string GetOriginalImportFilePath()
        {
            // Resume here... get correct directory
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.GetFullPath(Path.Combine(directory, @"..\..\..\..\ReportingTests\UnitTests\InputFiles\weather.dat"));
            return path;
        }
    }
}
