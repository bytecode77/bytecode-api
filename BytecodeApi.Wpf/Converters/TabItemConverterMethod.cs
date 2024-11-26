using System.Windows.Controls;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Specifies the method that is used to convert <see cref="TabItem" />? values.
/// </summary>
public enum TabItemConverterMethod
{
	/// <summary>
	/// Returns the index of the <see cref="TabItem" /> value.
	/// </summary>
	Index,
	/// <summary>
	/// Returns <see langword="true" />, if the <see cref="TabItem" /> value is the first tab item in the parent <see cref="TabControl" />.
	/// </summary>
	IsFirst,
	/// <summary>
	/// Returns <see langword="true" />, if the <see cref="TabItem" /> value is the last tab item in the parent <see cref="TabControl" />.
	/// </summary>
	IsLast,
	/// <summary>
	/// Returns <see cref="TabItem" />.Header property of the <see cref="TabItem" /> left to the <see cref="TabItem" /> value.
	/// </summary>
	PreviousHeader,
	/// <summary>
	/// Returns <see cref="TabItem" />.Header property of the <see cref="TabItem" /> right to the <see cref="TabItem" /> value.
	/// </summary>
	NextHeader
}