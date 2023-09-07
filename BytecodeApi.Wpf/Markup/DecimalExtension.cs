using System.Windows.Markup;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Implements <see cref="decimal" /> support for .NET Framework XAML Services.
/// </summary>
[MarkupExtensionReturnType(typeof(decimal))]
public sealed class DecimalExtension : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="decimal" /> value for this extension.
	/// </summary>
	public decimal Value { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="DecimalExtension" /> class.
	/// </summary>
	public DecimalExtension()
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="DecimalExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="decimal" /> value.
	/// </summary>
	/// <param name="value">A <see cref="decimal" /> value that is assigned to <see cref="Value" />.</param>
	public DecimalExtension(decimal value) : this()
	{
		Value = value;
	}

	/// <summary>
	/// Returns a <see cref="decimal" /> value that is supplied in the constructor of <see cref="DecimalExtension" />.
	/// </summary>
	/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
	/// <returns>
	/// A <see cref="decimal" /> value that is supplied in the constructor of <see cref="DecimalExtension" />.
	/// </returns>
	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return Value;
	}
}