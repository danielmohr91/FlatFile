using System.Collections.ObjectModel;
using FlatFile.FixedWidth.Implementation;
using FlatFile.FixedWidth.Interfaces;
using FlatFileParserUnitTests.Models;

namespace FlatFileParserUnitTests.Tests.TypeConverter.Helpers
{
    public class PrimitiveTypeExpectations
    {
        protected int BoolFieldLength = 7;
        protected int IdFieldLength = 5;
        protected int NumberFieldLength = 35;
        protected int StringFieldLength = 18;

        public Collection<PrimitiveTypesModel> GetExpectedRows()
        {
            var rows = new Collection<PrimitiveTypesModel>
            {
                new PrimitiveTypesModel
                {
                    id = 0,
                    boolTest = true,
                    decimalTest = decimal.MaxValue - 1,
                    doubleTest = 1.79769312486232E+308, // Just short of min / max values for each type. If actual min / max is used, overflow exception is thrown because rounding on floats.
                    floatTest = 3.402822E+38f, 
                    intTest = int.MaxValue,
                    longTest = long.MaxValue,
                    shortTest = short.MaxValue,
                    stringTest = "Test 1",
                    uintTest = uint.MaxValue,
                    ulongTest = ulong.MaxValue,
                    ushortTest = ushort.MaxValue
                },
                new PrimitiveTypesModel
                {
                    id = 1,
                    boolTest = false,
                    decimalTest = decimal.MinValue + 1,
                    doubleTest = -1.79769312486232E+308, //Math.Round(double.MinValue + 1E+300, 10),
                    floatTest = -3.402822E+38f, // (float)Math.Round(float.MinValue + (float)1E+32, 10),
                    intTest = int.MinValue,
                    longTest = long.MinValue,
                    shortTest = short.MinValue,
                    stringTest = "Test 2",
                    uintTest = uint.MinValue,
                    ulongTest = ulong.MinValue,
                    ushortTest = ushort.MinValue
                },
                new PrimitiveTypesModel
                {
                    id = 2,
                    boolTest = false,
                    decimalTest = (decimal) 42.42424242,
                    doubleTest = 42.42424242,
                    floatTest = (float) 42.42424,
                    intTest = 42,
                    longTest = (long) 42.42424242,
                    shortTest = (short) 42.42424242,
                    stringTest = "l33t $42",
                    uintTest = 42,
                    ulongTest = 42,
                    ushortTest = 42
                },
                new PrimitiveTypesModel
                {
                    id = 3,
                    boolTest = true,
                    decimalTest = 0,
                    doubleTest = 0,
                    floatTest = 0,
                    intTest = 0,
                    longTest = 0,
                    shortTest = 0,
                    stringTest = string.Empty,
                    uintTest = 0,
                    ulongTest = 0,
                    ushortTest = 0
                }
            };

            for (var i = 4; i <= 1000; i++)
                rows.Add(new PrimitiveTypesModel
                {
                    id = i,
                    boolTest = i % 2 == 0,
                    longTest = (long) (i * 25.25),
                    decimalTest = (decimal) (i * 36.36),
                    doubleTest = i * 50.5,
                    floatTest = i * -25.5f,
                    intTest = i * 25,
                    ulongTest = (ulong) (i * 500),
                    stringTest = $"Test String {i}",
                    shortTest = (short) (i * -2.5),
                    ushortTest = (ushort) (i * 4),
                    uintTest = (uint) (i * 5)
                });

            return rows;
        }

        public IFlatFileLayoutDescriptor<PrimitiveTypesModel> GetLayout()
        {
            return new LayoutDescriptor<PrimitiveTypesModel>()
                .AppendField(x => x.id, IdFieldLength)
                .AppendField(x => x.boolTest, BoolFieldLength)
                .AppendField(x => x.longTest, NumberFieldLength)
                .AppendField(x => x.decimalTest, NumberFieldLength)
                .AppendField(x => x.doubleTest, NumberFieldLength)
                .AppendField(x => x.floatTest, NumberFieldLength)
                .AppendField(x => x.intTest, NumberFieldLength)
                .AppendField(x => x.ulongTest, NumberFieldLength)
                .AppendField(x => x.stringTest, StringFieldLength)
                .AppendField(x => x.shortTest, NumberFieldLength)
                .AppendField(x => x.ushortTest, NumberFieldLength)
                .AppendField(x => x.uintTest, NumberFieldLength);
        }
    }
}