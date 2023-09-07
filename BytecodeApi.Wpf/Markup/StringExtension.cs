using System.Windows.Markup;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Implements <see cref="string" /> support for .NET Framework XAML Services.
/// </summary>
[MarkupExtensionReturnType(typeof(string))]
public sealed class StringExtension : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="string" /> value for this extension.
	/// </summary>
	public string? Value { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="StringExtension" /> class.
	/// </summary>
	public StringExtension()
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="StringExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="string" /> value.
	/// </summary>
	/// <param name="value">A <see cref="string" /> value that is assigned to <see cref="Value" />.</param>
	public StringExtension(string? value) : this()
	{
		Value = value;
	}

	/// <summary>
	/// Returns a <see cref="string" /> value that is supplied in the constructor of <see cref="StringExtension" />.
	/// </summary>
	/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
	/// <returns>
	/// A <see cref="string" /> value that is supplied in the constructor of <see cref="StringExtension" />.
	/// </returns>
	public override object? ProvideValue(IServiceProvider serviceProvider)
	{
		return Value;
	}
}