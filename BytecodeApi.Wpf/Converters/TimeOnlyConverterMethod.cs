namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Specifies the method that is used to convert <see cref="TimeOnly" />? values.
/// </summary>
public enum TimeOnlyConverterMethod
{
	/// <summary>
	/// Returns the hour component of the <see cref="TimeOnly" />? value as a <see cref="string" />.
	/// </summary>
	Hour,
	/// <summary>
	/// Returns the minute component of the <see cref="TimeOnly" />? value as a <see cref="string" />.
	/// </summary>
	Minute,
	/// <summary>
	/// Returns the second component of the <see cref="TimeOnly" />? value as a <see cref="string" />.
	/// </summary>
	Second,
	/// <summary>
	/// Returns the equivalent <see cref="string" /> representation of the <see cref="TimeOnly" />? value using a specified format parameter and the invariant culture.
	/// </summary>
	Format
}