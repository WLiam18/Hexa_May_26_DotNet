using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HospitalAppointmentAPI.JsonConverters
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new JsonException("Cannot convert empty string to DateOnly.");
                }

                if (DateOnly.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    return date;
                }

                if (DateOnly.TryParseExact(value, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return date;
                }

                throw new JsonException($"Unable to convert '{value}' to DateOnly.");
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                using var document = JsonDocument.ParseValue(ref reader);
                var root = document.RootElement;

                if (!root.TryGetProperty("year", out var yearProperty) ||
                    !root.TryGetProperty("month", out var monthProperty) ||
                    !root.TryGetProperty("day", out var dayProperty))
                {
                    throw new JsonException("Expected object with year, month, and day properties when parsing DateOnly.");
                }

                var year = yearProperty.GetInt32();
                var month = monthProperty.GetInt32();
                var day = dayProperty.GetInt32();
                return new DateOnly(year, month, day);
            }

            throw new JsonException("Expected string or object token when parsing DateOnly.");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }
    }

    public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        private const string Format = "HH:mm:ss";

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new JsonException("Cannot convert empty string to TimeOnly.");
                }

                if (TimeOnly.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
                {
                    return time;
                }

                if (TimeOnly.TryParseExact(value, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
                {
                    return time;
                }

                throw new JsonException($"Unable to convert '{value}' to TimeOnly.");
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                using var document = JsonDocument.ParseValue(ref reader);
                var root = document.RootElement;

                if (!root.TryGetProperty("hour", out var hourProperty) ||
                    !root.TryGetProperty("minute", out var minuteProperty))
                {
                    throw new JsonException("Expected object with hour and minute properties when parsing TimeOnly.");
                }

                var hour = hourProperty.GetInt32();
                var minute = minuteProperty.GetInt32();
                var second = root.TryGetProperty("second", out var secondProperty) ? secondProperty.GetInt32() : 0;
                return new TimeOnly(hour, minute, second);
            }

            throw new JsonException("Expected string or object token when parsing TimeOnly.");
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }
    }
}
