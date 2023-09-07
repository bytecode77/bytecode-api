using System.Windows.Markup;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Implements <see cref="float" /> support for .NET Framework XAML Services.
/// </summary>
[MarkupExtensionReturnType(typeof(float))]
public sealed class SingleExtension : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="float" /> value for this extension.
	/// </summary>
	public float Value { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="SingleExtension" /> class.
	/// </summary>
	public SingleExtension()
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="SingleExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="float" /> value.
	/// </summary>
	/// <param name="value">A <see cref="float" /> value that is assigned to <see cref="Value" />.</param>
	public SingleExtension(float value) : this()
	{
		Value = value;
	}

	/// <summary>
	/// Returns a <see cref="Value" /> value that is supplied in the constructor of <see cref="SingleExtension" />.
	/// </summary>
	/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
	/// <returns>
	/// A <see cref="Value" /> value that is supplied in the constructor of <see cref="SingleExtension" />.
	/// </returns>
	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return Value;
	}
}