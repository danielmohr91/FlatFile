using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DataMunging.Reporting.ViewModels;

namespace DataMunging.UnitTests
{
    public static class MockedData
    {

        public static Collection<DailyTemperatures> GetExpectedPoints()
        {
            // Mocked expected result based on ~\DataMunging\UnitTests\InputFiles\weather.dat
            return new Collection<DailyTemperatures>
            {
                new DailyTemperatures(1, 88, 59),
                new DailyTemperatures(2, 79, 63),
                new DailyTemperatures(3, 77, 55),
                new DailyTemperatures(4, 77, 59),
                new DailyTemperatures(5, 90, 66),
                new DailyTemperatures(6, 81, 61),
                new DailyTemperatures(7, 73, 57),
                new DailyTemperatures(8, 75, 54),
                new DailyTemperatures(9, 86, 32),
                new DailyTemperatures(10, 84, 64),
                new DailyTemperatures(11, 91, 59),
                new DailyTemperatures(12, 88, 73),
                new DailyTemperatures(13, 70, 59),
                new DailyTemperatures(14, 61, 59),
                new DailyTemperatures(15, 64, 55),
                new DailyTemperatures(16, 79, 59),
                new DailyTemperatures(17, 81, 57),
                new DailyTemperatures(18, 82, 52),
                new DailyTemperatures(19, 81, 61),
                new DailyTemperatures(20, 84, 57),
                new DailyTemperatures(21, 86, 59),
                new DailyTemperatures(22, 90, 64),
                new DailyTemperatures(23, 90, 68),
                new DailyTemperatures(24, 90, 77),
                new DailyTemperatures(25, 90, 72),
                new DailyTemperatures(26, 97, 64),
                new DailyTemperatures(27, 91, 72),
                new DailyTemperatures(28, 84, 68),
                new DailyTemperatures(29, 88, 66),
                new DailyTemperatures(30, 90, 45)
            };
        }
    }
}
