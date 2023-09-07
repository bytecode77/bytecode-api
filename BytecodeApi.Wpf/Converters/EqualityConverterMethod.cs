namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Specifies the method that is used to compare objects or <see cref="IComparable" /> values.
/// </summary>
public enum EqualityConverterMethod
{
	/// <summary>
	/// Compares the value and parameter for equality using the <see cref="object.Equals(object, object)" /> method.
	/// </summary>
	Equal,
	/// <summary>
	/// Compares the value and parameter for inequality using the <see cref="object.Equals(object, object)" /> method.
	/// </summary>
	NotEqual,
	/// <summary>
	/// If the value and parameter are <see cref="IComparable" /> objects, returns <see langword="true" />, if value is less than parameter; otherwise, <see langword="false" />.
	/// </summary>
	Less,
	/// <summary>
	/// If the value and parameter are <see cref="IComparable" /> objects, returns <see langword="true" />, if value is less than or equal to parameter; otherwise, <see langword="false" />.
	/// </summary>
	LessEqual,
	/// <summary>
	/// If the value and parameter are <see cref="IComparable" /> objects, returns <see langword="true" />, if value is greater than parameter; otherwise, <see langword="false" />.
	/// </summary>
	Greater,
	/// <summary>
	/// If the value and parameter are <see cref="IComparable" /> objects, returns <see langword="true" />, if value is greater than or equal to parameter; otherwise, <see langword="false" />.
	/// </summary>
	GreaterEqual
}