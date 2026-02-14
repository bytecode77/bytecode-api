using BytecodeApi.Extensions;
using System.Collections;

namespace BytecodeApi.IniParser;

/// <summary>
/// Represents a collection of INI file properties.
/// </summary>
public sealed class IniPropertyCollection : ICollection<IniProperty>
{
	private readonly List<IniProperty> Properties;
	/// <summary>
	/// Gets the <see cref="IniProperty" /> at the specified index.
	/// </summary>
	/// <param name="index">The index at which to retrieve the <see cref="IniProperty" />.</param>
	public IniProperty this[int index]
	{
		get
		{
			Check.IndexOutOfRange(index, Count);
			return Properties[index];
		}
	}
	/// <summary>
	/// Gets the <see cref="IniProperty" /> with the specified case sensitive name.
	/// </summary>
	/// <param name="name">A <see cref="string" /> specifying the name of the property.</param>
	public IniProperty this[string name]
	{
		get
		{
			Check.ArgumentNull(name);
			return Properties.FirstOrDefault(p => p.Name == name) ?? throw Throw.KeyNotFound($"A property with the name '{name}' was not found.");
		}
	}
	/// <summary>
	/// Gets the number of properties in this <see cref="IniPropertyCollection" />.
	/// </summary>
	public int Count => Properties.Count;
	/// <summary>
	/// Gets a value indicating whether the <see cref="IniPropertyCollection" /> is read-only.
	/// </summary>
	public bool IsReadOnly => false;

	/// <summary>
	/// Initializes a new instance of the <see cref="IniPropertyCollection" /> class.
	/// </summary>
	public IniPropertyCollection()
	{
		Properties = [];
	}

	/// <summary>
	/// Processes all duplicate properties within this collection according to the specified behavior.
	/// </summary>
	/// <param name="behavior">An <see cref="IniDuplicatePropertyNameBehavior" /> object specifying how duplicates are processed.</param>
	public void ProcessDuplicates(IniDuplicatePropertyNameBehavior behavior)
	{
		ProcessDuplicates(behavior, false);
	}
	/// <summary>
	/// Processes all duplicate properties within this collection according to the specified behavior.
	/// </summary>
	/// <param name="behavior">An <see cref="IniDuplicatePropertyNameBehavior" /> object specifying how duplicates are processed.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during name comparison.</param>
	public void ProcessDuplicates(IniDuplicatePropertyNameBehavior behavior, bool ignoreCase)
	{
		List<IniProperty> removedProperties = [];

		switch (behavior)
		{
			case IniDuplicatePropertyNameBehavior.Abort:
				if (Properties.DistinctBy(property => ignoreCase ? property.Name.ToLower() : property.Name).Count() != Count)
				{
					throw Throw.Format("Duplicate property found.");
				}
				break;
			case IniDuplicatePropertyNameBehavior.Ignore:
				for (int i = 1; i < Count; i++)
				{
					if (Properties.Take(i).Any(property => property.Name.Equals(Properties[i].Name, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)))
					{
						removedProperties.Add(Properties[i]);
					}
				}
				break;
			case IniDuplicatePropertyNameBehavior.Overwrite:
				for (int i = 1; i < Count; i++)
				{
					if (Properties.Take(i).FirstOrDefault(property => property.Name.Equals(Properties[i].Name, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)) is IniProperty firstProperty)
					{
						firstProperty.Value = Properties[i].Value;
						removedProperties.Add(Properties[i]);
					}
				}
				break;
			case IniDuplicatePropertyNameBehavior.Duplicate:
				break;
			default:
				throw Throw.InvalidEnumArgument(nameof(behavior), behavior);
		}

		Properties.RemoveRange(removedProperties);
	}

	/// <summary>
	/// Adds an <see cref="IniProperty" /> to the end of the <see cref="IniPropertyCollection" />.
	/// </summary>
	/// <param name="item">The <see cref="IniProperty" /> to be added to the end of the <see cref="IniPropertyCollection" />.</param>
	public void Add(IniProperty item)
	{
		Check.ArgumentNull(item);

		Properties.Add(item);
	}
	/// <summary>
	/// Adds an <see cref="IniProperty" /> to the end of the <see cref="IniPropertyCollection" />.
	/// </summary>
	/// <param name="name">The name of the INI property.</param>
	/// <param name="value">The value of the INI property.</param>
	/// <returns>
	/// The newly added <see cref="IniProperty" />.
	/// </returns>
	public IniProperty Add(string name, string value)
	{
		Check.ArgumentNull(name);
		Check.ArgumentNull(value);

		IniProperty property = new(name, value);
		Add(property);
		return property;
	}
	/// <summary>
	/// Removes the first occurrence of a specific <see cref="IniProperty" /> from the <see cref="IniPropertyCollection" />.
	/// </summary>
	/// <param name="item">The <see cref="IniProperty" /> to remove from the <see cref="IniPropertyCollection" />.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="item" /> is successfully removed;
	/// otherwise, <see langword="false" />.
	/// This method also returns <see langword="false" />, if <paramref name="item" /> was not found in the <see cref="IniPropertyCollection" />.
	/// </returns>
	public bool Remove(IniProperty item)
	{
		return Properties.Remove(item);
	}
	/// <summary>
	/// Removes all elements from the <see cref="IniPropertyCollection" />.
	/// </summary>
	public void Clear()
	{
		Properties.Clear();
	}
	/// <summary>
	/// Determines whether an element is in the <see cref="IniPropertyCollection" />.
	/// </summary>
	/// <param name="item">The <see cref="IniProperty" /> to locate in the <see cref="IniPropertyCollection" />.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="item" /> is found in the <see cref="IniPropertyCollection" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Contains(IniProperty item)
	{
		return Properties.Contains(item);
	}
	void ICollection<IniProperty>.CopyTo(IniProperty[] array, int arrayIndex)
	{
		Check.ArgumentNull(array);
		Check.IndexOutOfRange(arrayIndex, array.Length - Count + 1);

		Properties.CopyTo(array, arrayIndex);
	}
	/// <summary>
	/// Returns an enumerator that iterates through the <see cref="IniPropertyCollection" />.
	/// </summary>
	/// <returns>
	/// An enumerator that can be used to iterate through the <see cref="IniPropertyCollection" />.
	/// </returns>
	public IEnumerator<IniProperty> GetEnumerator()
	{
		return Properties.GetEnumerator();
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return Properties.GetEnumerator();
	}
}