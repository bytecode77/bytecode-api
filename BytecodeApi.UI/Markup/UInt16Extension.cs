using System;
using System.Windows.Markup;

namespace BytecodeApi.UI.Markup
{
	/// <summary>
	/// Implements <see cref="ushort" /> support for .NET Framework XAML Services.
	/// </summary>
	[MarkupExtensionReturnType(typeof(ushort))]
	public sealed class UInt16Extension : MarkupExtension
	{
		/// <summary>
		/// Gets or sets the <see cref="ushort" /> value for this extension.
		/// </summary>
		public ushort Value { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="UInt16Extension" /> class.
		/// </summary>
		public UInt16Extension()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UInt16Extension" /> class, initializing <see cref="Value" /> based on the provided <see cref="ushort" /> value.
		/// </summary>
		/// <param name="value">A <see cref="ushort" /> value that is assigned to <see cref="Value" />.</param>
		public UInt16Extension(ushort value) : this()
		{
			Value = value;
		}

		/// <summary>
		/// Returns a <see cref="ushort" /> value that is supplied in the constructor of <see cref="UInt16Extension" />.
		/// </summary>
		/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
		/// <returns>
		/// A <see cref="ushort" /> value that is supplied in the constructor of <see cref="UInt16Extension" />.
		/// </returns>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Value;
		}
	}
}