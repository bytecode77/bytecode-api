using BytecodeApi.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BytecodeApi.Text.Json.Converters;

/// <summary>
/// Converts JSON values to or from <see cref="TimeOnly" /> values.
/// </summary>
public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
	private readonly string Format;

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeOnlyJsonConverter" /> class.
	/// </summary>
	/// <param name="format">The format that is used to convert <see cref="TimeOnly" /> values.</param>
	public TimeOnlyJsonConverter(string format)
	{
		Check.ArgumentNull(format);

		Format = format;
	}

	/// <summary>
	/// Reads and converts the JSON value to a <see cref="TimeOnly" /> value.
	/// </summary>
	/// <param name="reader">The <see cref="Utf8JsonReader" /> to read from.</param>
	/// <param name="typeToConvert">This parameter is ignored.</param>
	/// <param name="options">This parameter is ignored.</param>
	/// <returns>
	/// An equivalent <see cref="TimeOnly" /> value, parsed from the JSON value.
	/// </returns>
	public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return TimeOnly.Parse(reader.GetString()!);
	}
	/// <summary>
	/// Writes the <see cref="TimeOnly" /> value as JSON.
	/// </summary>
	/// <param name="writer">The <see cref="Utf8JsonWriter" /> to write to.</param>
	/// <param name="value">The <see cref="TimeOnly" /> value to be converted.</param>
	/// <param name="options">This parameter is ignored.</param>
	public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToStringInvariant(Format));
	}
}