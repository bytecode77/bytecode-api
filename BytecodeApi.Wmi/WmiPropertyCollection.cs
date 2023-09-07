using BytecodeApi.Extensions;
using System.Collections;

namespace BytecodeApi.Wmi;

/// <summary>
/// Represents a collection of WMI properties.
/// </summary>
public sealed class WmiPropertyCollection : IReadOnlyCollection<WmiProperty>
{
	private readonly List<WmiProperty> Properties;
	/// <summary>
	/// Gets the <see cref="WmiProperty" /> at the specified index.
	/// </summary>
	/// <param name="index">The index at which to retrieve the <see cref="WmiProperty" />.</param>
	public WmiProperty this[int index]
	{
		get
		{
			Check.IndexOutOfRange(index, Count);
			return Properties[index];
		}
	}
	/// <summary>
	/// Gets the <see cref="WmiProperty" /> with the specified name and throws an exception, if it was not found.
	/// </summary>
	/// <param name="name">A <see cref="string" /> specifying the WMI property name.</param>
	public WmiProperty this[string name]
	{
		get
		{
			Check.ArgumentNull(name);
			return Properties.FirstOrNull(p => p.Name == name) ?? throw Throw.KeyNotFound($"A property with the name '{name}' was not found.");
		}
	}
	/// <summary>
	/// Gets the number of properties in this <see cref="WmiPropertyCollection" />.
	/// </summary>
	public int Count => Properties.Count;

	/// <summary>
	/// Initializes a new instance of the <see cref="WmiPropertyCollection" /> class.
	/// </summary>
	public WmiPropertyCollection()
	{
		Properties = new();
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="WmiPropertyCollection" /> class with the specified collection of properties.
	/// </summary>
	/// <param name="properties">A collection of <see cref="WmiProperty" /> structures to add to this <see cref="WmiPropertyCollection" />.</param>
	public WmiPropertyCollection(IEnumerable<WmiProperty> properties) : this()
	{
		Check.ArgumentNull(properties);

		Properties.AddRange(properties);
	}

	/// <summary>
	/// Returns an enumerator that iterates through the <see cref="WmiPropertyCollection" />.
	/// </summary>
	/// <returns>
	/// An enumerator that can be used to iterate through the <see cref="WmiPropertyCollection" />.
	/// </returns>
	public IEnumerator<WmiProperty> GetEnumerator()
	{
		return Properties.GetEnumerator();
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return Properties.GetEnumerator();
	}
}