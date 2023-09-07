using System.Windows.Markup;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Implements <see cref="sbyte" /> support for .NET Framework XAML Services.
/// </summary>
[MarkupExtensionReturnType(typeof(sbyte))]
public sealed class SByteExtension : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="sbyte" /> value for this extension.
	/// </summary>
	public sbyte Value { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="SByteExtension" /> class.
	/// </summary>
	public SByteExtension()
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="SByteExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="sbyte" /> value.
	/// </summary>
	/// <param name="value">A <see cref="sbyte" /> value that is assigned to <see cref="Value" />.</param>
	public SByteExtension(sbyte value) : this()
	{
		Value = value;
	}

	/// <summary>
	/// Returns a <see cref="sbyte" /> value that is supplied in the constructor of <see cref="SByteExtension" />.
	/// </summary>
	/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
	/// <returns>
	/// A <see cref="sbyte" /> value that is supplied in the constructor of <see cref="SByteExtension" />.
	/// </returns>
	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return Value;
	}
}