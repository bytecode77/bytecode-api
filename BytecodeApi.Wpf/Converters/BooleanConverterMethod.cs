using System.Windows;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Specifies the method that is used to convert <see cref="bool" />? values.
/// </summary>
public enum BooleanConverterMethod
{
	/// <summary>
	/// Returns the original <see cref="bool" />? value.
	/// </summary>
	Default,
	/// <summary>
	/// Returns <see langword="true" />, if the <see cref="bool" />? value is <see langword="false" /> or <see langword="null" />; otherwise, <see langword="false" />.
	/// </summary>
	Inverse,
	/// <summary>
	/// Returns <see cref="Visibility.Visible" />, if the <see cref="bool" />? value is <see langword="true" />; otherwise, <see cref="Visibility.Collapsed" />.
	/// </summary>
	Visibility,
	/// <summary>
	/// Returns <see cref="Visibility.Collapsed" />, if the <see cref="bool" />? value is <see langword="true" />; otherwise, <see cref="Visibility.Visible" />.
	/// </summary>
	VisibilityInverse,
	/// <summary>
	/// Returns <see cref="Visibility.Visible" />, if the <see cref="bool" />? value is <see langword="true" />; otherwise, <see cref="Visibility.Hidden" />.
	/// </summary>
	VisibilityHidden,
	/// <summary>
	/// Returns <see cref="Visibility.Hidden" />, if the <see cref="bool" />? value is <see langword="true" />; otherwise, <see cref="Visibility.Visible" />.
	/// </summary>
	VisibilityHiddenInverse,
	/// <summary>
	/// Returns <see cref="GridLength.Auto" />, if the <see cref="bool" />? value is <see langword="true" />; otherwise, <see langword="new" /> <see cref="GridLength" />(0).
	/// </summary>
	GridLengthZeroAuto,
	/// <summary>
	/// Returns <see langword="new" /> <see cref="GridLength" />(0), if the <see cref="bool" />? value is <see langword="true" />; otherwise, <see cref="GridLength.Auto" />.
	/// </summary>
	GridLengthZeroAutoInverse,
	/// <summary>
	/// Returns <see langword="new" /> <see cref="GridLength" />(1, <see cref="GridUnitType.Star" />), if the <see cref="bool" />? value is <see langword="true" />; otherwise, <see langword="new" /> <see cref="GridLength" />(0).
	/// </summary>
	GridLengthZeroStar,
	/// <summary>
	/// Returns <see langword="new" /> <see cref="GridLength" />(0), if the <see cref="bool" />? value is <see langword="true" />; otherwise, <see langword="new" /> <see cref="GridLength" />(1, <see cref="GridUnitType.Star" />).
	/// </summary>
	GridLengthZeroStarInverse,
	/// <summary>
	/// Returns <see langword="new" /> <see cref="GridLength" />(parameter), if the <see cref="bool" />? value is <see langword="true" />; otherwise, <see cref="GridLength.Auto" />.
	/// </summary>
	GridLengthValueAuto,
	/// <summary>
	/// Returns <see cref="GridLength.Auto" />, if the <see cref="bool" />? value is <see langword="true" />; otherwise, <see langword="new" /> <see cref="GridLength" />(parameter).
	/// </summary>
	GridLengthValueAutoInverse,
	/// <summary>
	/// Returns <see langword="new" /> <see cref="GridLength" />(parameter), if the <see cref="bool" />? value is <see langword="true" />; otherwise, <see langword="new" /> <see cref="GridLength" />(1, <see cref="GridUnitType.Star" />).
	/// </summary>
	GridLengthValueStar,
	/// <summary>
	/// Returns <see langword="new" /> <see cref="GridLength" />(1, <see cref="GridUnitType.Star" />), if the <see cref="bool" />? value is <see langword="true" />; otherwise, <see langword="new" /> <see cref="GridLength" />(parameter).
	/// </summary>
	GridLengthValueStarInverse,
	/// <summary>
	/// Returns <see langword="new" /> <see cref="GridLength" />(parameter), if the <see cref="bool" />? value is <see langword="true" />; otherwise, <see langword="new" /> <see cref="GridLength" />(0).
	/// </summary>
	GridLengthValueZero,
	/// <summary>
	/// Returns <see langword="new" /> <see cref="GridLength" />(0), if the <see cref="bool" />? value is <see langword="true" />; otherwise, <see langword="new" /> <see cref="GridLength" />(parameter).
	/// </summary>
	GridLengthValueZeroInverse
}