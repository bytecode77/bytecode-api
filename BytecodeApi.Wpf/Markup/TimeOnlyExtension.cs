using BytecodeApi.Extensions;
using System.Globalization;
using System.Windows.Markup;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Implements <see cref="TimeOnly" /> support for .NET Framework XAML Services.
/// </summary>
[MarkupExtensionReturnType(typeof(TimeOnly))]
public sealed class TimeOnlyExtension : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="TimeOnly" /> value for this extension.
	/// </summary>
	public TimeOnly Value { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeOnlyExtension" /> class.
	/// </summary>
	public TimeOnlyExtension()
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="TimeOnlyExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="TimeOnly" /> value.
	/// </summary>
	/// <param name="value">A <see cref="string" /> value that is converted to <see cref="TimeOnly" /> and then assigned to <see cref="Value" />.</param>
	public TimeOnlyExtension(string? value) : this()
	{
		if (TimeOnly.TryParse(value, CultureInfo.InvariantCulture, out TimeOnly timeOnly))
		{
			Value = timeOnly;
		}
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="TimeOnlyExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="TimeOnly" /> value.
	/// </summary>
	/// <param name="value">A <see cref="string" /> value that is converted to <see cref="TimeOnly" /> and then assigned to <see cref="Value" />.</param>
	/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert <paramref name="value" />.</param>
	public TimeOnlyExtension(string? value, string format) : this()
	{
		Check.ArgumentNull(format);

		if (value?.ToTimeOnly(format) is TimeOnly timeOnly)
		{
			Value = timeOnly;
		}
	}

	/// <summary>
	/// Returns a <see cref="TimeOnly" /> value that is supplied in the constructor of <see cref="TimeOnlyExtension" />.
	/// </summary>
	/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
	/// <returns>
	/// A <see cref="TimeOnly" /> value that is supplied in the constructor of <see cref="TimeOnlyExtension" />.
	/// </returns>
	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return Value;
	}
}