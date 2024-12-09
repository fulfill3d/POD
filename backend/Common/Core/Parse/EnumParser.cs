using System.ComponentModel;
using System.Reflection;

namespace POD.Common.Core.Parse
{
    public class EnumParser
    {
        public static TEnum ParseEnum<TEnum>(string value) where TEnum : struct
        {
            foreach (TEnum item in System.Enum.GetValues(typeof(TEnum)))
            {
                if (GetEnumDescription(item).Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }

            throw new ArgumentException($"Invalid status value: {value}");
        }

        public static string GetEnumDescription<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString().ToLower(); // Default to the enum name if no description is found
            }
        }
    }
}