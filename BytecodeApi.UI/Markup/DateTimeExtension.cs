using System;
using System.Windows.Markup;

namespace BytecodeApi.UI.Markup
{
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
		/// <param name="value">A <see cref="DateTime" /> value that is assigned to <see cref="Value" />.</param>
		public DateTimeExtension(DateTime value) : this()
		{
			Value = value;
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
}