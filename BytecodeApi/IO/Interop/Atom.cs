using System.Text;

namespace BytecodeApi.IO.Interop
{
	/// <summary>
	/// Represents an entry in the local atom table.
	/// </summary>
	public struct Atom : IAtom
	{
		/// <summary>
		/// Represents the atom as a 16-bit integer value.
		/// </summary>
		public ushort Value { get; private set; }
		/// <summary>
		/// Gets a <see cref="string" /> with the name of the atom.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Atom" /> structure with a the specified atom value.
		/// </summary>
		/// <param name="value">A <see cref="ushort" /> representing the atom value.</param>
		public Atom(ushort value)
		{
			Value = value;

			StringBuilder stringBuilder = new StringBuilder(256);
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
			return new Atom(Native.FindAtom(name));
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
			return new Atom(Native.AddAtom(name));
		}

		/// <summary>
		/// Decrements the reference count of this atom. If the atom's reference count is reduced to zero, it is removed from the atom table.
		/// </summary>
		public void Delete()
		{
			Native.DeleteAtom(Value);
		}
	}
}