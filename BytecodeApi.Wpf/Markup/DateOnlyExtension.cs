using BytecodeApi.Extensions;
using System.Globalization;
using System.Windows.Markup;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Implements <see cref="DateOnly" /> support for .NET Framework XAML Services.
/// </summary>
[MarkupExtensionReturnType(typeof(DateOnly))]
public sealed class DateOnlyExtension : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="DateOnly" /> value for this extension.
	/// </summary>
	public DateOnly Value { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="DateOnlyExtension" /> class.
	/// </summary>
	public DateOnlyExtension()
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="DateOnlyExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="DateOnly" /> value.
	/// </summary>
	/// <param name="value">A <see cref="string" /> value that is converted to <see cref="DateOnly" /> and then assigned to <see cref="Value" />.</param>
	public DateOnlyExtension(string? value) : this()
	{
		if (DateOnly.TryParse(value, CultureInfo.InvariantCulture, out DateOnly dateOnly))
		{
			Value = dateOnly;
		}
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="DateOnlyExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="DateOnly" /> value.
	/// </summary>
	/// <param name="value">A <see cref="string" /> value that is converted to <see cref="DateOnly" /> and then assigned to <see cref="Value" />.</param>
	/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert <paramref name="value" />.</param>
	public DateOnlyExtension(string? value, string format) : this()
	{
		Check.ArgumentNull(format);

		if (value?.ToDateOnly(format) is DateOnly dateOnly)
		{
			Value = dateOnly;
		}
	}

	/// <summary>
	/// Returns a <see cref="DateOnly" /> value that is supplied in the constructor of <see cref="DateOnlyExtension" />.
	/// </summary>
	/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
	/// <returns>
	/// A <see cref="DateOnly" /> value that is supplied in the constructor of <see cref="DateOnlyExtension" />.
	/// </returns>
	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return Value;
	}
}