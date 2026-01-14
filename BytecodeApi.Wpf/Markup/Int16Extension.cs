using System.Windows.Markup;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Implements <see cref="short" /> support for .NET Framework XAML Services.
/// </summary>
[MarkupExtensionReturnType(typeof(short))]
public sealed class Int16Extension : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="short" /> value for this extension.
	/// </summary>
	public short Value { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="Int16Extension" /> class.
	/// </summary>
	public Int16Extension()
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="Int16Extension" /> class, initializing <see cref="Value" /> based on the provided <see cref="short" /> value.
	/// </summary>
	/// <param name="value">A <see cref="short" /> value that is assigned to <see cref="Value" />.</param>
	public Int16Extension(short value) : this()
	{
		Value = value;
	}

	/// <summary>
	/// Returns a <see cref="short" /> value that is supplied in the constructor of <see cref="Int16Extension" />.
	/// </summary>
	/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
	/// <returns>
	/// A <see cref="short" /> value that is supplied in the constructor of <see cref="Int16Extension" />.
	/// </returns>
	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return Value;
	}
}