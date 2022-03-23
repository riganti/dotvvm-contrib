using System;
using Newtonsoft.Json;

namespace DotVVM.Contrib.CookieBar
{
    public class CookieBarRuleConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var rule = (CookieBarRule)value;

            writer.WriteStartObject();
            
            writer.WritePropertyName(nameof(CookieBarRule.Key));
            writer.WriteValue(rule.Key);
            writer.WritePropertyName(nameof(CookieBarRule.Title));
            writer.WriteValue(rule.Title);
            writer.WritePropertyName(nameof(CookieBarRule.Description));
            writer.WriteValue(rule.Description);

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CookieBarRule);
        }
    }
}