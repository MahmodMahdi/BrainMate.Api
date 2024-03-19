using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BrainMate.Infrastructure.Helper
{
	public class DateConverter : JsonConverter<DateOnly>
	{
		private string formatDate = "dd/MM/yyyy";
		public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return DateOnly.ParseExact(reader.GetString()!, formatDate, CultureInfo.InvariantCulture);
		}

		public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString(formatDate));
		}
	}
}
