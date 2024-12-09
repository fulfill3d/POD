using System.ComponentModel;

namespace POD.Common.Core.Enum
{
    public enum Unit
    {
        [Description("°C")]
        Celcius = 1,

        [Description("°F")]
        Fahrenheit = 2,

        [Description("°K")]
        Kelvin = 3,

        [Description("mm")]
        Millimeter = 4,

        [Description("cm")]
        Centimeter = 5,

        [Description("m")]
        Meter = 6,

        [Description("in")]
        Inch = 7,

        [Description("ft")]
        Foot = 8,

        [Description("g")]
        Gram = 9,

        [Description("kg")]
        Kilogram = 10,

        [Description("lb")]
        Pound = 11,

        [Description("oz")]
        Ounce = 12,

        [Description("mm3")]
        CubicMillimeter = 13,

        [Description("cm3")]
        CubicCentimeter = 14,

        [Description("m3")]
        CubicMeter = 15,

        [Description("gal")]
        Gallon = 16,

        [Description("mm2")]
        SquareMillimeter = 17,

        [Description("cm2")]
        SquareCentimeter = 18,

        [Description("m2")]
        SquareMeter = 19,

        [Description("in2")]
        SquareInch = 20,

        [Description("ft2")]
        SquareFoot = 21
        
    }
}