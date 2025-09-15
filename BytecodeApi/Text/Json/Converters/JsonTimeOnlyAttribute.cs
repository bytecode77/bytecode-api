using System.Text.Json.Serialization;

namespace BytecodeApi.Text.Json.Converters;

/// <summary>
/// Specifies conversion of JSON values to or from <see cref="TimeOnly" /> values.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class JsonTimeOnlyAttribute : JsonConverterAttribute
{
	/// <summary>
	/// The format that is used to convert <see cref="TimeOnly" /> values.
	/// </summary>
	public string Format { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="JsonTimeOnlyAttribute" /> class.
	/// </summary>
	/// <param name="format">The format that is used to convert <see cref="TimeOnly" /> values.</param>
	public JsonTimeOnlyAttribute(string format)
	{
		Check.ArgumentNull(format);

		Format = format;
	}

	/// <summary>
	/// Creates a new <see cref="JsonTimeOnlyAttribute" /> instance.
	/// </summary>
	/// <param name="typeToConvert">This parameter is ignored.</param>
	/// <returns>
	/// A new <see cref="JsonTimeOnlyAttribute" /> instance.
	/// </returns>
	public override JsonConverter CreateConverter(Type typeToConvert)
	{
		return new TimeOnlyJsonConverter(Format);
	}
}