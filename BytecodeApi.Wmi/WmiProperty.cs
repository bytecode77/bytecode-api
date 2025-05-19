using System.Diagnostics;

namespace BytecodeApi.Wmi;

/// <summary>
/// Represents a WMI property, composed of a name and a value.
/// </summary>
[DebuggerDisplay($"{nameof(WmiProperty)}: Name = {{Name}}, Value = {{Value}}")]
public readonly struct WmiProperty : IEquatable<WmiProperty>
{
	/// <summary>
	/// Gets the name of this <see cref="WmiProperty" />.
	/// </summary>
	public string Name { get; private init; }
	/// <summary>
	/// Gets the value of this <see cref="WmiProperty" />.
	/// </summary>
	public object? Value { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="WmiProperty" /> structure with the specified name and value.
	/// </summary>
	/// <param name="name">The name of this <see cref="WmiProperty" />.</param>
	/// <param name="value">The value of this <see cref="WmiProperty" />.</param>
	internal WmiProperty(string name, object? value)
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
	public T? GetValue<T>()
	{
		return (T?)Value;
	}

	/// <summary>
	/// Returns the name of this <see cref="WmiProperty" />.
	/// </summary>
	/// <returns>
	/// The name of this <see cref="WmiProperty" />.
	/// </returns>
	public override string ToString()
	{
		return Name;
	}
	/// <summary>
	/// Determines whether the specified <see cref="object" /> is equal to this instance.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
	/// <returns>
	/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public override bool Equals([NotNullWhen(true)] object? obj)
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
		return Name == other.Name && Equals(Value, other.Value);
	}
	/// <summary>
	/// Returns a hash code for this <see cref="WmiProperty" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="WmiProperty" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return HashCode.Combine(Name, Value);
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