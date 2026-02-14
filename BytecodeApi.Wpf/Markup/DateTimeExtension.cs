using BytecodeApi.Extensions;
using System.Globalization;
using System.Windows.Markup;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Implements <see cref="DateTime" /> support for .NET Framework XAML Services.
/// </summary>
[MarkupExtensionReturnType(typeof(DateTime))]
public sealed class DateTimeExtension : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="DateTime" /> value for this extension.
	/// </summary>
	public DateTime Value { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="DateTimeExtension" /> class.
	/// </summary>
	public DateTimeExtension()
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="DateTimeExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="DateTime" /> value.
	/// </summary>
	/// <param name="value">A <see cref="string" /> value that is converted to <see cref="DateTime" /> and then assigned to <see cref="Value" />.</param>
	public DateTimeExtension(string? value) : this()
	{
		if (DateTime.TryParse(value, CultureInfo.InvariantCulture, out DateTime dateTime))
		{
			Value = dateTime;
		}
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="DateTimeExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="DateTime" /> value.
	/// </summary>
	/// <param name="value">A <see cref="string" /> value that is converted to <see cref="DateTime" /> and then assigned to <see cref="Value" />.</param>
	/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert <paramref name="value" />.</param>
	public DateTimeExtension(string? value, string format) : this()
	{
		Check.ArgumentNull(format);

		if (value?.ToDateTime(format) is DateTime dateTime)
		{
			Value = dateTime;
		}
	}

	/// <summary>
	/// Returns a <see cref="DateTime" /> value that is supplied in the constructor of <see cref="DateTimeExtension" />.
	/// </summary>
	/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
	/// <returns>
	/// A <see cref="DateTime" /> value that is supplied in the constructor of <see cref="DateTimeExtension" />.
	/// </returns>
	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return Value;
	}
}