using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotVVM.Contrib.CookieBar
{
    public class CookieBarRuleConverter : JsonConverter<CookieBarRule>
    {
        public override CookieBarRule Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
			if (reader.TokenType != JsonTokenType.StartObject)
			{
				throw new JsonException("Expected start of object.");
			}
			var rule = new CookieBarRule();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var propertyName = reader.GetString();
					reader.Read();
					switch (propertyName)
					{
						case nameof(CookieBarRule.Key):
							rule.Key = reader.GetString();
							break;
						case nameof(CookieBarRule.Title):
							rule.Title = reader.GetString();
							break;
						case nameof(CookieBarRule.Description):
							rule.Description = reader.GetString();
							break;
						default:
							throw new JsonException($"Unexpected property: {propertyName}");
					}
				}
			}
			return rule;
		}

        public override void Write(Utf8JsonWriter writer, CookieBarRule value, JsonSerializerOptions options)
        {
			writer.WriteStartObject();
			writer.WriteString(nameof(CookieBarRule.Key), value.Key);
			writer.WriteString(nameof(CookieBarRule.Title), value.Title);
			writer.WriteString(nameof(CookieBarRule.Description), value.Description);
			writer.WriteEndObject();
		}
    }
}