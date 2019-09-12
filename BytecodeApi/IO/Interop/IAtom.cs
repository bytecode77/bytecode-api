namespace BytecodeApi.IO.Interop
{
	/// <summary>
	/// Defines base properties for atom table entries.
	/// </summary>
	public interface IAtom
	{
		/// <summary>
		/// Represents the atom as a 16-bit integer value.
		/// </summary>
		ushort Value { get; }
		/// <summary>
		/// Gets a <see cref="string" /> with the name of the atom.
		/// </summary>
		string Name { get; }
	}
}