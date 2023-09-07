using System.Windows.Markup;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Implements <see cref="uint" /> support for .NET Framework XAML Services.
/// </summary>
[MarkupExtensionReturnType(typeof(uint))]
public sealed class UInt32Extension : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="uint" /> value for this extension.
	/// </summary>
	public uint Value { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="UInt32Extension" /> class.
	/// </summary>
	public UInt32Extension()
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="UInt32Extension" /> class, initializing <see cref="Value" /> based on the provided <see cref="uint" /> value.
	/// </summary>
	/// <param name="value">A <see cref="uint" /> value that is assigned to <see cref="Value" />.</param>
	public UInt32Extension(uint value) : this()
	{
		Value = value;
	}

	/// <summary>
	/// Returns a <see cref="uint" /> value that is supplied in the constructor of <see cref="UInt32Extension" />.
	/// </summary>
	/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
	/// <returns>
	/// A <see cref="uint" /> value that is supplied in the constructor of <see cref="UInt32Extension" />.
	/// </returns>
	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return Value;
	}
}