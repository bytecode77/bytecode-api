using System.Text.Json.Serialization;

namespace BytecodeApi.Text.Json.Converters;

/// <summary>
/// Specifies the maximum string length when serializing and deserializing. Values that exceed the given length are trimmed.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class JsonStringLengthAttribute : JsonConverterAttribute
{
	/// <summary>
	/// Gets the maximum length of <see cref="string" /> values.
	/// </summary>
	public int MaxLength { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="JsonStringLengthAttribute" /> class.
	/// </summary>
	/// <param name="maxLength">The maximum length of <see cref="string" /> values. If the <see cref="string" /> is longer, it is trimmed.</param>
	public JsonStringLengthAttribute(int maxLength)
	{
		MaxLength = maxLength;
	}

	/// <summary>
	/// Creates a new <see cref="StringLengthJsonConverter" /> instance.
	/// </summary>
	/// <param name="typeToConvert">This parameter is ignored.</param>
	/// <returns>
	/// A new <see cref="StringLengthJsonConverter" /> instance.
	/// </returns>
	public override JsonConverter CreateConverter(Type typeToConvert)
	{
		return new StringLengthJsonConverter(MaxLength);
	}
}