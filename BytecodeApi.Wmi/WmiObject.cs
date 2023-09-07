using System.Diagnostics;

namespace BytecodeApi.Wmi;

/// <summary>
/// Represents a WMI object instance.
/// </summary>
[DebuggerDisplay($"{nameof(WmiObject)}: Class = {{Class.Name}}, Properties: {{Properties.Count}}")]
public sealed class WmiObject
{
	/// <summary>
	/// Gets the <see cref="WmiClass" /> that this <see cref="WmiObject" /> was created from.
	/// </summary>
	public WmiClass Class { get; private init; }
	/// <summary>
	/// Gets the collection of properties of this <see cref="WmiObject" /> instance.
	/// </summary>
	public WmiPropertyCollection Properties { get; private init; }

	internal WmiObject(WmiClass @class, IEnumerable<WmiProperty> properties)
	{
		Class = @class;
		Properties = new(properties);
	}
}