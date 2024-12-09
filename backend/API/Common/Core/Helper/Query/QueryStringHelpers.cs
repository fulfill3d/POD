using System.Collections.Specialized;

namespace POD.API.Common.Core.Helper.Query
{
    public static class QueryStringHelpers
    {
        public static int TryGetIntValueOrDefault(this NameValueCollection nameValues, string key, int defaultValue = 1)
        {
            var stringValues = nameValues.GetValues(key);

            if (stringValues == null || !stringValues.Any()) return defaultValue;

            return int.TryParse(stringValues[0], out var result) ? result : defaultValue;
        }

        public static List<int> TryGetIntList(this NameValueCollection nameValues, string key)
        {
            var result = new List<int>();
            var stringValues = nameValues.GetValues(key);

            if (stringValues == null) return result;

            foreach (var item in stringValues)
            {
                if(int.TryParse(item, out var intValue)) result.Add(intValue);
            }
            return result;
        }

        public static IEnumerable<string> TryGetStringList(this NameValueCollection nameValues, string key)
        {
            var stringValues = nameValues.GetValues(key);

            if (stringValues == null) return new List<string>();

            return stringValues;
        }

        public static string? TryGetStringValueOrNull(this NameValueCollection nameValues, string key)
        {
            var stringValues = nameValues.GetValues(key);

            if (stringValues == null || !stringValues.Any()) return null;

            return stringValues[0];
        }

        public static bool TryGetBoolValueOrDefault(this NameValueCollection nameValues, string key)
        {
            var stringValues = nameValues.GetValues(key);

            if (stringValues == null || !stringValues.Any()) return false;

            return bool.TryParse(stringValues[0], out var result) && result;
        }

        public static T TryGetEnumValueOrDefault<T>(this NameValueCollection nameValues, string key) where T : struct
        {
            T result = default;
            var stringValues = nameValues.GetValues(key);
            if (stringValues == null || !stringValues.Any()) return result;
            Enum.TryParse<T>(stringValues[0], true, out result);
            return result;
        }

        public static DateTime? TryGetDateTimeValueOrNull(this NameValueCollection nameValues, string key)
        {
            var stringValues = nameValues.GetValues(key);

            if (stringValues == null || !stringValues.Any()) return null;

            return DateTime.TryParse(stringValues[0], out var result) ? result : null;
        }
    }
}