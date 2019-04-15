using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BytecodeApi.IO.Wmi
{
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
				Check.KeyNotFoundException(HasProperty(name), "The property '" + name + "' was not found.");
				return Properties.First(property => property.Name == name);
			}
		}
		/// <summary>
		/// Gets the <see cref="WmiProperty" /> with the specified name and returns a new <see cref="WmiProperty" /> with a default value, if it was not found.
		/// </summary>
		/// <param name="name">A <see cref="string" /> specifying the WMI property name.</param>
		/// <param name="defaultValue">The value that is used if the property with the specified name was not found.</param>
		public WmiProperty this[string name, object defaultValue] => HasProperty(name) ? this[name] : new WmiProperty(name, defaultValue);
		/// <summary>
		/// Gets the number of properties in this <see cref="WmiPropertyCollection" />.
		/// </summary>
		public int Count => Properties.Count;

		/// <summary>
		/// Initializes a new instance of the <see cref="WmiPropertyCollection" /> class.
		/// </summary>
		public WmiPropertyCollection()
		{
			Properties = new List<WmiProperty>();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="WmiPropertyCollection" /> class with the specified collection of properties.
		/// </summary>
		/// <param name="properties">A collection of <see cref="WmiProperty" /> structures to add to this <see cref="WmiPropertyCollection" />.</param>
		public WmiPropertyCollection(IEnumerable<WmiProperty> properties) : this()
		{
			Check.ArgumentNull(properties, nameof(properties));

			Properties.AddRange(properties);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="WmiPropertyCollection" /> class with the specified collection of properties.
		/// </summary>
		/// <param name="properties">A collection of <see cref="WmiProperty" /> structures to add to this <see cref="WmiPropertyCollection" />.</param>
		public WmiPropertyCollection(params WmiProperty[] properties) : this((IEnumerable<WmiProperty>)properties)
		{
		}

		/// <summary>
		/// Determines whether a property with the specified name exists in this collection.
		/// </summary>
		/// <param name="name">The name of the property to check.</param>
		/// <returns>
		/// <see langword="true" />, if the property with the specified name exists;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool HasProperty(string name)
		{
			return Properties.Any(property => property.Name == name);
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
}