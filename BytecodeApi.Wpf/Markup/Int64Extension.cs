using System.Windows.Markup;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Implements <see cref="long" /> support for .NET Framework XAML Services.
/// </summary>
[MarkupExtensionReturnType(typeof(long))]
public sealed class Int64Extension : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="long" /> value for this extension.
	/// </summary>
	public long Value { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="Int64Extension" /> class.
	/// </summary>
	public Int64Extension()
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="Int64Extension" /> class, initializing <see cref="Value" /> based on the provided <see cref="long" /> value.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value that is assigned to <see cref="Value" />.</param>
	public Int64Extension(long value) : this()
	{
		Value = value;
	}

	/// <summary>
	/// Returns a <see cref="long" /> value that is supplied in the constructor of <see cref="Int64Extension" />.
	/// </summary>
	/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
	/// <returns>
	/// A <see cref="long" /> value that is supplied in the constructor of <see cref="Int64Extension" />.
	/// </returns>
	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return Value;
	}
}