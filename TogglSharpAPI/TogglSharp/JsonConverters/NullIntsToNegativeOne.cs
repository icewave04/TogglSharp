using System;
using System.Globalization;
using Newtonsoft.Json;

namespace TogglSharpAPI.JsonConverters
{
    public class NullIntsToNegativeOne : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.Equals(null))
            {
                writer.WriteValue(-1);
            }
            else
            {
                writer.WriteValue(value);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return -1;
            }

            string valueToString = reader.Value.ToString();
            var culture = CultureInfo.GetCultureInfo("en-US");
            int.TryParse(valueToString, NumberStyles.Integer, culture, out int outInt);
            return outInt;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int);
        }
    }
}