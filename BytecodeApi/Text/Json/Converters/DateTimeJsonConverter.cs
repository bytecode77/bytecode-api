﻿using BytecodeApi.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BytecodeApi.Text.Json.Converters;

/// <summary>
/// Converts JSON values to or from <see cref="DateTime" /> values.
/// </summary>
public sealed class DateTimeJsonConverter : JsonConverter<DateTime>
{
	/// <summary>
	/// Reads and converts the JSON value to a <see cref="DateTime" /> value.
	/// </summary>
	/// <param name="reader">The <see cref="Utf8JsonReader" /> to read from.</param>
	/// <param name="typeToConvert">This parameter is ignored.</param>
	/// <param name="options">This parameter is ignored.</param>
	/// <returns>
	/// An equivalent <see cref="DateTime" /> value, parsed from the JSON value.
	/// </returns>
	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return DateTime.Parse(reader.GetString()!);
	}
	/// <summary>
	/// Writes the <see cref="DateTime" /> value as JSON with the format "yyyy-MM-ddTHH:mm:ss".
	/// </summary>
	/// <param name="writer">The <see cref="Utf8JsonWriter" /> to write to.</param>
	/// <param name="value">The <see cref="DateTime" /> value to be converted.</param>
	/// <param name="options">This parameter is ignored.</param>
	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToStringInvariant("yyyy-MM-ddTHH:mm:ss"));
	}
}