using System;
using System.Windows.Markup;

namespace BytecodeApi.UI.Markup
{
	/// <summary>
	/// Implements <see cref="double" /> support for .NET Framework XAML Services.
	/// </summary>
	public sealed class DoubleExtension : MarkupExtension
	{
		/// <summary>
		/// Gets or sets the <see cref="double" /> value for this extension.
		/// </summary>
		public double Value { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DoubleExtension" /> class.
		/// </summary>
		public DoubleExtension()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="DoubleExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="double" /> value.
		/// </summary>
		/// <param name="value">A <see cref="double" /> value that is assigned to <see cref="Value" />.</param>
		public DoubleExtension(double value) : this()
		{
			Value = value;
		}

		/// <summary>
		/// Returns a <see cref="double" /> value that is supplied in the constructor of <see cref="DoubleExtension" />.
		/// </summary>
		/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
		/// <returns>
		/// A <see cref="double" /> value that is supplied in the constructor of <see cref="DoubleExtension" />.
		/// </returns>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Value;
		}
	}
}