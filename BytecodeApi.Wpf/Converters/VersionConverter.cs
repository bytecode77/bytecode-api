namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts <see cref="Version" /> values. The <see cref="Convert(Version)" /> method returns a <see cref="string" /> based on the specified <see cref="FieldCount" /> parameter.
/// </summary>
public sealed class VersionConverter : ConverterBase<Version>
{
	/// <summary>
	/// Specifies the number of components to return from the given <see cref="Version" />. The value ranges from 0 to 4.
	/// </summary>
	public int FieldCount { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="VersionConverter" /> class.
	/// </summary>
	public VersionConverter() : this(4)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="VersionConverter" /> class with the specified conversion field count.
	/// </summary>
	/// <param name="fieldCount">Specifies the number of components to return from the given <see cref="Version" />. The value ranges from 0 to 4.</param>
	public VersionConverter(int fieldCount)
	{
		FieldCount = fieldCount;
	}

	/// <summary>
	/// Converts the <see cref="Version" /> value based the specified <see cref="FieldCount" /> parameter.
	/// </summary>
	/// <param name="value">The <see cref="Version" /> value to convert.</param>
	/// <returns>
	/// A <see cref="string" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(Version? value)
	{
		return value?.ToString(FieldCount);
	}
}