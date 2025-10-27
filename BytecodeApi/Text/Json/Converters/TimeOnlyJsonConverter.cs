using BytecodeApi.Extensions;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BytecodeApi.Text.Json.Converters;

/// <summary>
/// Converts JSON values to or from <see cref="TimeOnly" /> values.
/// </summary>
public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
	private readonly string Format;
	private readonly bool ParseExact;

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeOnlyJsonConverter" /> class.
	/// </summary>
	/// <param name="format">The format that is used to convert <see cref="TimeOnly" /> values.</param>
	/// <param name="parseExact"><see langword="true" /> to require the exact format; <see langword="false" /> to allow any format during read operations.</param>
	public TimeOnlyJsonConverter(string format, bool parseExact)
	{
		Check.ArgumentNull(format);

		Format = format;
		ParseExact = parseExact;
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
		if (ParseExact)
		{
			return reader.GetString()!.ToTimeOnly(Format) ?? throw Throw.Format("String is not a valid TimeOnly value.");
		}
		else
		{
			return TimeOnly.TryParse(reader.GetString(), CultureInfo.InvariantCulture, out TimeOnly result) ? result : throw Throw.Format("String is not a valid TimeOnly value.");
		}
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