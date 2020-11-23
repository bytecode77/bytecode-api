using System;
using System.Windows.Markup;

namespace BytecodeApi.UI.Markup
{
	/// <summary>
	/// Implements <see cref="int" /> support for .NET Framework XAML Services.
	/// </summary>
	[MarkupExtensionReturnType(typeof(int))]
	public sealed class Int32Extension : MarkupExtension
	{
		/// <summary>
		/// Gets or sets the <see cref="int" /> value for this extension.
		/// </summary>
		public int Value { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Int32Extension" /> class.
		/// </summary>
		public Int32Extension()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Int32Extension" /> class, initializing <see cref="Value" /> based on the provided <see cref="int" /> value.
		/// </summary>
		/// <param name="value">A <see cref="int" /> value that is assigned to <see cref="Value" />.</param>
		public Int32Extension(int value) : this()
		{
			Value = value;
		}

		/// <summary>
		/// Returns a <see cref="int" /> value that is supplied in the constructor of <see cref="Int32Extension" />.
		/// </summary>
		/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
		/// <returns>
		/// A <see cref="int" /> value that is supplied in the constructor of <see cref="Int32Extension" />.
		/// </returns>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Value;
		}
	}
}