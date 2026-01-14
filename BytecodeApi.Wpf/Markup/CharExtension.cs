using System.Windows.Markup;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Implements <see cref="char" /> support for .NET Framework XAML Services.
/// </summary>
[MarkupExtensionReturnType(typeof(char))]
public sealed class CharExtension : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="char" /> value for this extension.
	/// </summary>
	public char Value { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="CharExtension" /> class.
	/// </summary>
	public CharExtension()
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="CharExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="char" /> value.
	/// </summary>
	/// <param name="value">A <see cref="char" /> value that is assigned to <see cref="Value" />.</param>
	public CharExtension(char value) : this()
	{
		Value = value;
	}

	/// <summary>
	/// Returns a <see cref="char" /> value that is supplied in the constructor of <see cref="CharExtension" />.
	/// </summary>
	/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
	/// <returns>
	/// A <see cref="char" /> value that is supplied in the constructor of <see cref="CharExtension" />.
	/// </returns>
	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return Value;
	}
}