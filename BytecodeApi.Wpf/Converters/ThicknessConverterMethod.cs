using System.Windows;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Specifies the method that is used to convert <see cref="Thickness" />? values.
/// </summary>
public enum ThicknessConverterMethod
{
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Left" /> is set to the parameter value.
	/// </summary>
	SetLeft,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Top" /> is set to the parameter value.
	/// </summary>
	SetTop,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Right" /> is set to the parameter value.
	/// </summary>
	SetRight,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Bottom" /> is set to the parameter value.
	/// </summary>
	SetBottom,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Left" /> and <see cref="Thickness.Right" /> is set to the parameter value.
	/// </summary>
	SetHorizontal,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Top" /> and <see cref="Thickness.Bottom" /> is set to the parameter value.
	/// </summary>
	SetVertical,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Left" /> ist incremented by the parameter value.
	/// </summary>
	AddLeft,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Top" /> ist incremented by the parameter value.
	/// </summary>
	AddTop,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Right" /> ist incremented by the parameter value.
	/// </summary>
	AddRight,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Bottom" /> ist incremented by the parameter value.
	/// </summary>
	AddBottom,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Left" /> and <see cref="Thickness.Right" /> ist incremented by the parameter value.
	/// </summary>
	AddHorizontal,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Top" /> and <see cref="Thickness.Bottom" /> ist incremented by the parameter value.
	/// </summary>
	AddVertical,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where every property except <see cref="Thickness.Left" /> is set to the parameter value.
	/// </summary>
	KeepLeft,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where every property except <see cref="Thickness.Top" /> is set to the parameter value.
	/// </summary>
	KeepTop,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where every property except <see cref="Thickness.Right" /> is set to the parameter value.
	/// </summary>
	KeepRight,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where every property except <see cref="Thickness.Bottom" /> is set to the parameter value.
	/// </summary>
	KeepBottom,
	/// <summary>
	/// Returns an inverted <see cref="Thickness" /> value.
	/// </summary>
	Inverse,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Left" /> is inverted.
	/// </summary>
	InverseLeft,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Top" /> is inverted.
	/// </summary>
	InverseTop,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Right" /> is inverted.
	/// </summary>
	InverseRight,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Bottom" /> is inverted.
	/// </summary>
	InverseBottom,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Left" /> and <see cref="Thickness.Right" /> is inverted.
	/// </summary>
	InverseHorizontal,
	/// <summary>
	/// Returns a <see cref="Thickness" /> value, where <see cref="Thickness.Top" /> and <see cref="Thickness.Bottom" /> is inverted.
	/// </summary>
	InverseVertical
}