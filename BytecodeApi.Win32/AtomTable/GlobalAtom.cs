using System.Runtime.InteropServices;
using System.Text;

namespace BytecodeApi.Win32.AtomTable;

/// <summary>
/// Represents an entry in the global atom table.
/// </summary>
public readonly struct GlobalAtom : IAtom, IEquatable<GlobalAtom>
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
	/// Initializes a new instance of the <see cref="GlobalAtom" /> structure with the specified atom value.
	/// </summary>
	/// <param name="value">A <see cref="ushort" /> representing the atom value.</param>
	public GlobalAtom(ushort value)
	{
		Value = value;

		StringBuilder stringBuilder = new(256);
		Native.GlobalGetAtomName(Value, stringBuilder, 256);
		Name = stringBuilder.ToString();
	}
	/// <summary>
	/// Finds a global atom by its name. If not found, a new <see cref="GlobalAtom" /> value is returned with a value of 0.
	/// </summary>
	/// <param name="name">A <see cref="string" /> with the case insensitive name of the atom to be searched.</param>
	/// <returns>
	/// The <see cref="GlobalAtom" /> this method creates.
	/// </returns>
	public static GlobalAtom Find(string name)
	{
		Check.ArgumentNull(name);

		return new(Native.GlobalFindAtom(name));
	}
	/// <summary>
	/// Creates a new global atom using the specified name.
	/// </summary>
	/// <param name="name">A <see cref="string" /> with the name for the new atom.</param>
	/// <returns>
	/// The <see cref="GlobalAtom" /> this method creates.
	/// </returns>
	public static GlobalAtom Add(string name)
	{
		Check.ArgumentNull(name);

		return new(Native.GlobalAddAtom(name));
	}

	/// <summary>
	/// Decrements the reference count of this atom. If the atom's reference count is reduced to zero, it is removed from the global atom table.
	/// </summary>
	public void Delete()
	{
		Native.GlobalDeleteAtom(Value);
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
		return obj is GlobalAtom atom && Equals(atom);
	}
	/// <summary>
	/// Determines whether this instance is equal to another <see cref="GlobalAtom" />.
	/// </summary>
	/// <param name="other">The <see cref="GlobalAtom" /> to compare to this instance.</param>
	/// <returns>
	/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals(GlobalAtom other)
	{
		return Value == other.Value && Name == other.Name;
	}
	/// <summary>
	/// Returns a hash code for this <see cref="GlobalAtom" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="GlobalAtom" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return CSharp.GetHashCode(Value, Name);
	}

	/// <summary>
	/// Compares two <see cref="GlobalAtom" /> values for equality.
	/// </summary>
	/// <param name="a">The first <see cref="GlobalAtom" /> to compare.</param>
	/// <param name="b">The second <see cref="GlobalAtom" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="GlobalAtom" /> values are equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator ==(GlobalAtom a, GlobalAtom b)
	{
		return Equals(a, b);
	}
	/// <summary>
	/// Compares two <see cref="GlobalAtom" /> values for inequality.
	/// </summary>
	/// <param name="a">The first <see cref="GlobalAtom" /> to compare.</param>
	/// <param name="b">The second <see cref="GlobalAtom" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="GlobalAtom" /> values are not equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator !=(GlobalAtom a, GlobalAtom b)
	{
		return !Equals(a, b);
	}
}

file static class Native
{
	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	public static extern uint GlobalGetAtomName(ushort atom, StringBuilder buffer, int size);
	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	public static extern ushort GlobalFindAtom(string name);
	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	public static extern ushort GlobalAddAtom(string name);
	[DllImport("kernel32.dll", ExactSpelling = true)]
	public static extern ushort GlobalDeleteAtom(ushort atom);
}