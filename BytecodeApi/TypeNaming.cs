using System;

namespace BytecodeApi
{
	/// <summary>
	/// Specifies the naming convention of a <see cref="Type" />.
	/// </summary>
	public enum TypeNaming
	{
		/// <summary>
		/// The namespace is omitted and the C# type name is used for built-in types.
		/// <para>Examples: int, DateTime</para>
		/// </summary>
		CSharp,
		/// <summary>
		/// The namespace is omitted and the .NET Framework type name is used.
		/// <para>Examples: Int32, DateTime</para>
		/// </summary>
		TypeName,
		/// <summary>
		/// The full type name including namespace is used.
		/// <para>Examples: System.Int32, System.DateTime</para>
		/// </summary>
		FullName
	}
}