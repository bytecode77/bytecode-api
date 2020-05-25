using System;
using System.Windows.Markup;

namespace BytecodeApi.UI.Markup
{
	/// <summary>
	/// Implements <see cref="TimeSpan" /> support for .NET Framework XAML Services.
	/// </summary>
	[MarkupExtensionReturnType(typeof(TimeSpan))]
	public sealed class TimeSpanExtension : MarkupExtension
	{
		/// <summary>
		/// Gets or sets the <see cref="TimeSpan" /> value for this extension.
		/// </summary>
		public TimeSpan Value { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TimeSpanExtension" /> class.
		/// </summary>
		public TimeSpanExtension()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TimeSpanExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="int" /> value.
		/// </summary>
		/// <param name="value">A <see cref="int" /> value represeting the milliseconds fraction of a <see cref="TimeSpan" /> that is assigned to <see cref="Value" />.</param>
		public TimeSpanExtension(int value) : this()
		{
			Value = TimeSpan.FromMilliseconds(value);
		}

		/// <summary>
		/// Returns a <see cref="TimeSpan" /> value that is supplied in the constructor of <see cref="TimeSpanExtension" />.
		/// </summary>
		/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
		/// <returns>
		/// A <see cref="TimeSpan" /> value that is supplied in the constructor of <see cref="TimeSpanExtension" />.
		/// </returns>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Value;
		}
	}
}