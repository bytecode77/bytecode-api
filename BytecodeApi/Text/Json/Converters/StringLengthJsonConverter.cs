using System.Text.Json;
using System.Text.Json.Serialization;

namespace BytecodeApi.Text.Json.Converters;

/// <summary>
/// Trims JSON values that exceed a given length.
/// </summary>
public sealed class StringLengthJsonConverter : JsonConverter<string>
{
	private readonly int MaxLength;

	/// <summary>
	/// Initializes a new instance of the <see cref="StringLengthJsonConverter" /> class.
	/// </summary>
	/// <param name="maxLength">The maximum length of JSON values. If the <see cref="string" /> is longer, it is trimmed.</param>
	public StringLengthJsonConverter(int maxLength)
	{
		MaxLength = maxLength;
	}

	/// <summary>
	/// Reads the JSON value and trims the <see cref="string" />, if it exceeds a given length.
	/// </summary>
	/// <param name="reader">The <see cref="Utf8JsonReader" /> to read from.</param>
	/// <param name="typeToConvert">This parameter is ignored.</param>
	/// <param name="options">This parameter is ignored.</param>
	/// <returns>
	/// The trimmed JSON value.
	/// </returns>
	public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		string? str = reader.GetString();
		return str?.Length > MaxLength ? str[..MaxLength] : str;
	}
	/// <summary>
	/// Writes the value as JSON and trims the <see cref="string" />, if it exceeds a given length.
	/// </summary>
	/// <param name="writer">The <see cref="Utf8JsonWriter" /> to write to.</param>
	/// <param name="value">The <see cref="string" /> value to be trimmed.</param>
	/// <param name="options">This parameter is ignored.</param>
	public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value?.Length > MaxLength ? value[..MaxLength] : value);
	}
}