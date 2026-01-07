using System.Text.Json;
using System.Text.Json.Serialization;

namespace BytecodeApi.Text.Json.Converters;

/// <summary>
/// Converts JSON values to or from <see cref="Enum" /> values.
/// </summary>
/// <typeparam name="T">The type of the enum.</typeparam>
public sealed class EnumNameJsonConverter<T> : JsonConverter<T> where T : struct, Enum
{
	/// <summary>
	/// Reads and converts the JSON value to an <see cref="Enum" /> value, where the JSON value is equal to the name of the enum value.
	/// </summary>
	/// <param name="reader">The <see cref="Utf8JsonReader" /> to read from.</param>
	/// <param name="typeToConvert">This parameter is ignored.</param>
	/// <param name="options">This parameter is ignored.</param>
	/// <returns>
	/// An equivalent <see cref="Enum" /> value, parsed from the JSON value.
	/// </returns>
	public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return Enum.Parse<T>(reader.GetString()!);
	}
	/// <summary>
	/// Writes the name of the <see cref="Enum" /> value as JSON.
	/// </summary>
	/// <param name="writer">The <see cref="Utf8JsonWriter" /> to write to.</param>
	/// <param name="value">The <see cref="Enum" /> value to be converted.</param>
	/// <param name="options">This parameter is ignored.</param>
	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString());
	}
}