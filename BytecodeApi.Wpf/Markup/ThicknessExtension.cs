using System.Windows;
using System.Windows.Markup;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Implements <see cref="Thickness" /> support for .NET Framework XAML Services.
/// </summary>
[MarkupExtensionReturnType(typeof(Thickness))]
public sealed class ThicknessExtension : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="Thickness" /> value for this extension.
	/// </summary>
	public Thickness Value { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ThicknessExtension" /> class.
	/// </summary>
	public ThicknessExtension()
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="ThicknessExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="double" /> value.
	/// </summary>
	/// <param name="uniformLength">The uniform margin of the <see cref="Thickness" /> value.</param>
	public ThicknessExtension(double uniformLength) : this()
	{
		Value = new(uniformLength);
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="ThicknessExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="Thickness" /> value.
	/// </summary>
	/// <param name="left">The left margin of the <see cref="Thickness" /> value.</param>
	/// <param name="top">The top margin of the <see cref="Thickness" /> value.</param>
	/// <param name="right">The right margin of the <see cref="Thickness" /> value.</param>
	/// <param name="bottom">The bottom margin of the <see cref="Thickness" /> value.</param>
	public ThicknessExtension(double left, double top, double right, double bottom) : this()
	{
		Value = new(left, top, right, bottom);
	}

	/// <summary>
	/// Returns a <see cref="Thickness" /> value that is supplied in the constructor of <see cref="ThicknessExtension" />.
	/// </summary>
	/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
	/// <returns>
	/// A <see cref="Thickness" /> value that is supplied in the constructor of <see cref="ThicknessExtension" />.
	/// </returns>
	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return Value;
	}
}