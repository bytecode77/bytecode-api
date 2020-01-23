using System;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Specifies the method that is used to convert the <see cref="Type" /> of <see cref="object" /> values.
	/// </summary>
	public enum TypeConverterMethod
	{
		/// <summary>
		/// Returns <see langword="true" />, if the <see cref="Type" /> of value equals parameter; otherwise, <see langword="false" />.
		/// </summary>
		TypeEqual,
		/// <summary>
		/// Returns <see langword="true" />, if parameter <see cref="Type" /> is assignable from the <see cref="Type" /> of the value; otherwise, <see langword="false" />.
		/// </summary>
		IsAssignableFrom
	}
}