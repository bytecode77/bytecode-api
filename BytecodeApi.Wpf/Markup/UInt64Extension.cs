using System.Windows.Markup;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Implements <see cref="ulong" /> support for .NET Framework XAML Services.
/// </summary>
[MarkupExtensionReturnType(typeof(ulong))]
public sealed class UInt64Extension : MarkupExtension
{
	/// <summary>
	/// Gets or sets the <see cref="ulong" /> value for this extension.
	/// </summary>
	public ulong Value { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="UInt64Extension" /> class.
	/// </summary>
	public UInt64Extension()
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="UInt64Extension" /> class, initializing <see cref="Value" /> based on the provided <see cref="ulong" /> value.
	/// </summary>
	/// <param name="value">A <see cref="ulong" /> value that is assigned to <see cref="Value" />.</param>
	public UInt64Extension(ulong value) : this()
	{
		Value = value;
	}

	/// <summary>
	/// Returns a <see cref="ulong" /> value that is supplied in the constructor of <see cref="UInt64Extension" />.
	/// </summary>
	/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
	/// <returns>
	/// A <see cref="ulong" /> value that is supplied in the constructor of <see cref="UInt64Extension" />.
	/// </returns>
	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return Value;
	}
}