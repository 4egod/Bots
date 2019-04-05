using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Twitter.Bot.Converters
{
    public class DateTimeConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = (DateTime)value;
            var iso = date.ToTwitterTimestamp().ToString();

            writer.WriteValue(iso);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Convert.ToInt64(reader.Value).FromTwitterTimestamp();
        }
    }
}
