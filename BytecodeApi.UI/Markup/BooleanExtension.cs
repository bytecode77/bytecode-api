using System;
using System.Windows.Markup;

namespace BytecodeApi.UI.Markup
{
	/// <summary>
	/// Implements <see cref="bool" /> support for .NET Framework XAML Services.
	/// </summary>
	public sealed class BooleanExtension : MarkupExtension
	{
		/// <summary>
		/// Gets or sets the <see cref="bool" /> value for this extension.
		/// </summary>
		public bool Value { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BooleanExtension" /> class.
		/// </summary>
		public BooleanExtension()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="BooleanExtension" /> class, initializing <see cref="Value" /> based on the provided <see cref="bool" /> value.
		/// </summary>
		/// <param name="value">A <see cref="bool" /> value that is assigned to <see cref="Value" />.</param>
		public BooleanExtension(bool value) : this()
		{
			Value = value;
		}

		/// <summary>
		/// Returns a <see cref="bool" /> value that is supplied in the constructor of <see cref="BooleanExtension" />.
		/// </summary>
		/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
		/// <returns>
		/// A <see cref="bool" /> value that is supplied in the constructor of <see cref="BooleanExtension" />.
		/// </returns>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Value;
		}
	}
}