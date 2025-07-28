using System.Text.Json;
using System.Text.Json.Serialization;

namespace BytecodeApi.Text.Json.Converters;

/// <summary>
/// Converts JSON numeric values to or from <see cref="Enum" /> values.
/// </summary>
public sealed class NumberEnumJsonConverter<T> : JsonConverter<T> where T : struct, Enum
{
	/// <summary>
	/// Reads and converts the JSON value to an <see cref="Enum" /> value.
	/// </summary>
	/// <param name="reader">The <see cref="Utf8JsonReader" /> to read from.</param>
	/// <param name="typeToConvert">This parameter is ignored.</param>
	/// <param name="options">This parameter is ignored.</param>
	/// <returns>
	/// An equivalent <see cref="Enum" /> value, parsed from the JSON value.
	/// </returns>
	public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return (T)Enum.ToObject(typeof(T), reader.GetInt32());
	}
	/// <summary>
	/// Writes the <see cref="Enum" /> value as JSON.
	/// </summary>
	/// <param name="writer">The <see cref="Utf8JsonWriter" /> to write to.</param>
	/// <param name="value">The <see cref="DateTime" /> value to be converted.</param>
	/// <param name="options">This parameter is ignored.</param>
	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(Convert.ToInt32(value));
	}
}