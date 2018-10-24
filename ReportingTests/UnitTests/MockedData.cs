using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DataMunging.Reporting.ViewModels;

namespace DataMunging.UnitTests
{
    public static class MockedData
    {

        public static Collection<Point> GetExpectedPoints()
        {
            // Mocked expected result based on ~\DataMunging\UnitTests\InputFiles\weather.dat
            return new Collection<Point>
            {
                new Point(1, 88, 59),
                new Point(2, 79, 63),
                new Point(3, 77, 55),
                new Point(4, 77, 59),
                new Point(5, 90, 66),
                new Point(6, 81, 61),
                new Point(7, 73, 57),
                new Point(8, 75, 54),
                new Point(9, 86, 32),
                new Point(10, 84, 64),
                new Point(11, 91, 59),
                new Point(12, 88, 73),
                new Point(13, 70, 59),
                new Point(14, 61, 59),
                new Point(15, 64, 55),
                new Point(16, 79, 59),
                new Point(17, 81, 57),
                new Point(18, 82, 52),
                new Point(19, 81, 61),
                new Point(20, 84, 57),
                new Point(21, 86, 59),
                new Point(22, 90, 64),
                new Point(23, 90, 68),
                new Point(24, 90, 77),
                new Point(25, 90, 72),
                new Point(26, 97, 64),
                new Point(27, 91, 72),
                new Point(28, 84, 68),
                new Point(29, 88, 66),
                new Point(30, 90, 45)
            };
        }
    }
}
