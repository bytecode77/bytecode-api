using System.Text.Json.Serialization;

namespace BytecodeApi.Text.Json.Converters;

/// <summary>
/// Specifies conversion of JSON values to or from <see cref="DateTime" /> values.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class JsonDateTimeAttribute : JsonConverterAttribute
{
	/// <summary>
	/// The format that is used to convert <see cref="DateTime" /> values.
	/// </summary>
	public string Format { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="JsonDateTimeAttribute" /> class.
	/// </summary>
	/// <param name="format">The format that is used to convert <see cref="DateTime" /> values.</param>
	public JsonDateTimeAttribute(string format)
	{
		Check.ArgumentNull(format);

		Format = format;
	}

	/// <summary>
	/// Creates a new <see cref="JsonDateTimeAttribute" /> instance.
	/// </summary>
	/// <param name="typeToConvert">This parameter is ignored.</param>
	/// <returns>
	/// A new <see cref="JsonDateTimeAttribute" /> instance.
	/// </returns>
	public override JsonConverter CreateConverter(Type typeToConvert)
	{
		return new DateTimeJsonConverter(Format);
	}
}