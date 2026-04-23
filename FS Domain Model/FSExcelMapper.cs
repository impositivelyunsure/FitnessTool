using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;


namespace FitnessTool
{
    public class FSExcelMapper
    {

        private static readonly IReadOnlyDictionary<string, string> AfcdOverrides =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["Classification"] = nameof(AfcdFoodEntryRaw.classificationID),
            ["Moisture (water) (g)"] = nameof(AfcdFoodEntryRaw.moistureG)
        };

        private static readonly IReadOnlyDictionary<string, string> AusnutOverrides =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // Add entries here only if you later find headers that do not map cleanly.
            };


        public static AfcdFoodEntryRaw MapAfcdRow(IDictionary<string, object?> row) =>
        MapByConvention<AfcdFoodEntryRaw>(row, AfcdOverrides);

        public static AusnutFoodEntryRaw MapAusnutRow(IDictionary<string, object?> row) =>
            MapByConvention<AusnutFoodEntryRaw>(row, AusnutOverrides);

        public static List<string> GetUnmappedHeadersForAfcd(IEnumerable<string> headers) =>
            GetUnmappedHeaders<AfcdFoodEntryRaw>(headers, AfcdOverrides);

        public static List<string> GetUnmappedHeadersForAusnut(IEnumerable<string> headers) =>
            GetUnmappedHeaders<AusnutFoodEntryRaw>(headers, AusnutOverrides);

        public static T MapByConvention<T>(
            IDictionary<string, object?> row,
            IReadOnlyDictionary<string, string> overrides)
            where T : new()
        {
            var entity = new T();

            var writableProperties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite)
                .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            foreach (var (rawHeader, rawValue) in row)
            {
                var header = NormalizeHeader(rawHeader);

                var propertyName = overrides.TryGetValue(header, out var overrideName)
                    ? overrideName
                    : HeaderToPropertyName(header);

                if (!writableProperties.TryGetValue(propertyName, out var property))
                    continue;

                var convertedValue = ConvertValue(rawValue, property.PropertyType);
                property.SetValue(entity, convertedValue);
            }

            return entity;
        }

        public static List<string> GetUnmappedHeaders<T>(
            IEnumerable<string> headers,
            IReadOnlyDictionary<string, string> overrides)
        {
            var propertyNames = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite)
                .Select(p => p.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var unmapped = new List<string>();

            foreach (var rawHeader in headers)
            {
                var header = NormalizeHeader(rawHeader);

                var propertyName = overrides.TryGetValue(header, out var overrideName)
                    ? overrideName
                    : HeaderToPropertyName(header);

                if (!propertyNames.Contains(propertyName))
                    unmapped.Add(header);
            }

            return unmapped;
        }

        private static object? ConvertValue(object? rawValue, Type targetType)
        {
            var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
            var text = rawValue?.ToString()?.Trim();

            if (string.IsNullOrWhiteSpace(text))
            {
                if (targetType == typeof(string))
                    return string.Empty;

                if (Nullable.GetUnderlyingType(targetType) is not null)
                    return null;

                if (underlyingType == typeof(int))
                    return 0;

                if (underlyingType == typeof(decimal))
                    return 0m;

                return Activator.CreateInstance(underlyingType);
            }

            if (underlyingType == typeof(string))
                return text;

            if (underlyingType == typeof(int))
            {
                if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var i) ||
                    int.TryParse(text, NumberStyles.Integer, CultureInfo.CurrentCulture, out i))
                {
                    return i;
                }

                throw new FormatException($"Could not parse integer value '{text}'.");
            }

            if (underlyingType == typeof(decimal))
            {
                if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ||
                    decimal.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out d))
                {
                    return d;
                }

                if (Nullable.GetUnderlyingType(targetType) is not null)
                    return null;

                throw new FormatException($"Could not parse decimal value '{text}'.");
            }

            throw new NotSupportedException(
                $"Type '{targetType.Name}' is not supported by the mapper.");
        }

        private static string NormalizeHeader(string header)
        {
            if (string.IsNullOrWhiteSpace(header))
                return string.Empty;

            var normalized = header
                .Replace("\r", " ")
                .Replace("\n", " ")
                .Replace("\t", " ")
                .Replace("\"", "")
                .Trim();

            normalized = Regex.Replace(normalized, @"\s+", " ");

            return normalized;
        }

        private static string HeaderToPropertyName(string header)
        {
            var working = NormalizeHeader(header);

            working = working
                .Replace("%T", " PercentT ", StringComparison.OrdinalIgnoreCase)
                .Replace("%", " Percent ", StringComparison.OrdinalIgnoreCase)
                .Replace(" ug ", " mcg ", StringComparison.OrdinalIgnoreCase)
                .Replace("(ug)", "(mcg)", StringComparison.OrdinalIgnoreCase);

            var tokens = Regex.Matches(working, @"[A-Za-z0-9]+")
                .Select(m => m.Value)
                .ToList();

            if (tokens.Count == 0)
                return string.Empty;

            var propertyName = string.Concat(tokens.Select(FormatToken));

            if (char.IsDigit(propertyName[0]))
                propertyName = "N" + propertyName;

            return propertyName;
        }

        private static string FormatToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return string.Empty;

            if (token.Equals("PercentT", StringComparison.OrdinalIgnoreCase))
                return "PercentT";

            if (token.Equals("ID", StringComparison.OrdinalIgnoreCase))
                return "Id";

            if (token.Equals("mcg", StringComparison.OrdinalIgnoreCase))
                return "Mcg";

            if (token.Equals("mg", StringComparison.OrdinalIgnoreCase))
                return "Mg";

            if (token.Equals("g", StringComparison.OrdinalIgnoreCase))
                return "G";

            if (token.Equals("kj", StringComparison.OrdinalIgnoreCase))
                return "Kj";

            if (char.IsDigit(token[0]))
                return token.ToLowerInvariant();

            if (token.Length == 1)
                return token.ToUpperInvariant();

            return char.ToUpperInvariant(token[0]) + token[1..].ToLowerInvariant();
        }
    }
}
