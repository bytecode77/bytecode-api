using System.Runtime.InteropServices;
using System.Text;

namespace BytecodeApi.Win32.AtomTable;

/// <summary>
/// Represents an entry in the local atom table.
/// </summary>
public readonly struct Atom : IAtom, IEquatable<Atom>
{
	/// <summary>
	/// Represents the atom as a 16-bit integer value.
	/// </summary>
	public ushort Value { get; private init; }
	/// <summary>
	/// Gets a <see cref="string" /> with the name of the atom.
	/// </summary>
	public string Name { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="Atom" /> structure with the specified atom value.
	/// </summary>
	/// <param name="value">A <see cref="ushort" /> representing the atom value.</param>
	public Atom(ushort value)
	{
		Value = value;

		StringBuilder stringBuilder = new(256);
		Native.GetAtomName(Value, stringBuilder, 256);
		Name = stringBuilder.ToString();
	}
	/// <summary>
	/// Finds an atom by its name. If not found, a new <see cref="Atom" /> value is returned with a value of 0.
	/// </summary>
	/// <param name="name">A <see cref="string" /> with the case insensitive name of the atom to be searched.</param>
	/// <returns>
	/// The <see cref="Atom" /> this method creates.
	/// </returns>
	public static Atom Find(string name)
	{
		Check.ArgumentNull(name);

		return new(Native.FindAtom(name));
	}
	/// <summary>
	/// Creates a new atom using the specified name.
	/// </summary>
	/// <param name="name">A <see cref="string" /> with the name for the new atom.</param>
	/// <returns>
	/// The <see cref="Atom" /> this method creates.
	/// </returns>
	public static Atom Add(string name)
	{
		Check.ArgumentNull(name);

		return new(Native.AddAtom(name));
	}

	/// <summary>
	/// Decrements the reference count of this atom. If the atom's reference count is reduced to zero, it is removed from the atom table.
	/// </summary>
	public void Delete()
	{
		Native.DeleteAtom(Value);
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
		return obj is Atom atom && Equals(atom);
	}
	/// <summary>
	/// Determines whether this instance is equal to another <see cref="Atom" />.
	/// </summary>
	/// <param name="other">The <see cref="Atom" /> to compare to this instance.</param>
	/// <returns>
	/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals(Atom other)
	{
		return Value == other.Value && Name == other.Name;
	}
	/// <summary>
	/// Returns a hash code for this <see cref="Atom" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="Atom" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return CSharp.GetHashCode(Value, Name);
	}

	/// <summary>
	/// Compares two <see cref="Atom" /> values for equality.
	/// </summary>
	/// <param name="a">The first <see cref="Atom" /> to compare.</param>
	/// <param name="b">The second <see cref="Atom" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="Atom" /> values are equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator ==(Atom a, Atom b)
	{
		return Equals(a, b);
	}
	/// <summary>
	/// Compares two <see cref="Atom" /> values for inequality.
	/// </summary>
	/// <param name="a">The first <see cref="Atom" /> to compare.</param>
	/// <param name="b">The second <see cref="Atom" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="Atom" /> values are not equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator !=(Atom a, Atom b)
	{
		return !Equals(a, b);
	}
}

file static class Native
{
	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	public static extern uint GetAtomName(ushort atom, StringBuilder buffer, int size);
	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	public static extern ushort FindAtom(string name);
	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	public static extern ushort AddAtom(string name);
	[DllImport("kernel32.dll", ExactSpelling = true)]
	public static extern ushort DeleteAtom(ushort atom);
}