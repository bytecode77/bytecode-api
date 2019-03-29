using System;

namespace BytecodeApi.IO.Wmi
{
	/// <summary>
	/// Represents a WMI property, composed of a name and a value.
	/// </summary>
	public struct WmiProperty : IEquatable<WmiProperty>
	{
		/// <summary>
		/// Gets the name of the <see cref="WmiProperty" />.
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Gets the value of the <see cref="WmiProperty" />.
		/// </summary>
		public object Value { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="WmiProperty" /> struct with the specified name and value.
		/// </summary>
		/// <param name="name">The name of the <see cref="WmiProperty" />.</param>
		/// <param name="value">The value of the <see cref="WmiProperty" />.</param>
		internal WmiProperty(string name, object value)
		{
			Name = name;
			Value = value;
		}

		/// <summary>
		/// Returns the strongly typed representation of <see cref="Value" />. If <see cref="Value" /> is of another type, an exception is thrown.
		/// </summary>
		/// <typeparam name="T">The value type to use for conversion.</typeparam>
		/// <returns>
		/// The strongly typed representation of <see cref="Value" />. If <see cref="Value" /> is of another type, an exception is thrown.
		/// </returns>
		public T GetValue<T>()
		{
			return (T)Value;
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + Name + ", " + Value + "]";
		}
		/// <summary>
		/// Determines whether the specified <see cref="object" /> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
		/// <returns>
		/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public override bool Equals(object obj)
		{
			return obj is WmiProperty property && Equals(property);
		}
		/// <summary>
		/// Determines whether this instance is equal to another <see cref="WmiProperty" />.
		/// </summary>
		/// <param name="other">The <see cref="WmiProperty" /> to compare to this instance.</param>
		/// <returns>
		/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(WmiProperty other)
		{
			return Name == other.Name && Value == other.Value;
		}
		/// <summary>
		/// Returns a hash code for this <see cref="WmiProperty" />.
		/// </summary>
		/// <returns>
		/// The hash code for this <see cref="WmiProperty" /> instance.
		/// </returns>
		public override int GetHashCode()
		{
			return (Name?.GetHashCode() ?? 0) ^ (Value?.GetHashCode() ?? 0);
		}

		/// <summary>
		/// Compares two <see cref="WmiProperty" /> instances for equality.
		/// </summary>
		/// <param name="a">The first <see cref="WmiProperty" /> to compare.</param>
		/// <param name="b">The second <see cref="WmiProperty" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="WmiProperty" /> are equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator ==(WmiProperty a, WmiProperty b)
		{
			return Equals(a, b);
		}
		/// <summary>
		/// Compares two <see cref="WmiProperty" /> instances for inequality.
		/// </summary>
		/// <param name="a">The first <see cref="WmiProperty" /> to compare.</param>
		/// <param name="b">The second <see cref="WmiProperty" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="WmiProperty" /> are not equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator !=(WmiProperty a, WmiProperty b)
		{
			return !(a == b);
		}
	}
}