using System.Windows.Markup;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Implements <see cref="byte" /> support for .NET Framework XAML Services.
/// </summary>
[MarkupExtensionReturnType(typeof(byte))]
public sealed class ByteExtension : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="byte" /> value for this extension.
	/// </summary>
	public byte Value { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ByteExtension" /> class.
	/// </summary>
	public ByteExtension()
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="ByteExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="byte" /> value.
	/// </summary>
	/// <param name="value">A <see cref="byte" /> value that is assigned to <see cref="Value" />.</param>
	public ByteExtension(byte value) : this()
	{
		Value = value;
	}

	/// <summary>
	/// Returns a <see cref="byte" /> value that is supplied in the constructor of <see cref="ByteExtension" />.
	/// </summary>
	/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
	/// <returns>
	/// A <see cref="byte" /> value that is supplied in the constructor of <see cref="ByteExtension" />.
	/// </returns>
	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return Value;
	}
}