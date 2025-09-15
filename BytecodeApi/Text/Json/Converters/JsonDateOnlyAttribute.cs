using System.Text.Json.Serialization;

namespace BytecodeApi.Text.Json.Converters;

/// <summary>
/// Specifies conversion of JSON values to or from <see cref="DateOnly" /> values.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class JsonDateOnlyAttribute : JsonConverterAttribute
{
	/// <summary>
	/// The format that is used to convert <see cref="DateOnly" /> values.
	/// </summary>
	public string Format { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="JsonDateOnlyAttribute" /> class.
	/// </summary>
	/// <param name="format">The format that is used to convert <see cref="DateOnly" /> values.</param>
	public JsonDateOnlyAttribute(string format)
	{
		Check.ArgumentNull(format);

		Format = format;
	}

	/// <summary>
	/// Creates a new <see cref="JsonDateOnlyAttribute" /> instance.
	/// </summary>
	/// <param name="typeToConvert">This parameter is ignored.</param>
	/// <returns>
	/// A new <see cref="JsonDateOnlyAttribute" /> instance.
	/// </returns>
	public override JsonConverter CreateConverter(Type typeToConvert)
	{
		return new DateOnlyJsonConverter(Format);
	}
}