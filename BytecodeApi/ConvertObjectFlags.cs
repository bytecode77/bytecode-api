using System;

namespace BytecodeApi
{
	/// <summary>
	/// Specifies flags for the conversion of objects, used by the <see cref="CSharp.ConvertObject{TDest}(object, TDest, ConvertObjectFlags)" /> method.
	/// </summary>
	[Flags]
	public enum ConvertObjectFlags
	{
		/// <summary>
		/// Specifies that no conversion flags are defined.
		/// </summary>
		None = 0,
		/// <summary>
		/// Specifies that character casing of property and field names is not considered.
		/// </summary>
		IgnoreCase = 1,
		/// <summary>
		/// Specifies that properties are not copied from the source <see cref="object" />.
		/// </summary>
		IgnoreProperties = 2,
		/// <summary>
		/// Specifies that fields are not copied from the source <see cref="object" />.
		/// </summary>
		IgnoreFields = 4,
		/// <summary>
		/// Specifies that properties from the source <see cref="object" /> are copied to fields of the destination <see cref="object" /> in addition to its properties, as long as the name matches.
		/// </summary>
		PropertiesToFields = 8,
		/// <summary>
		/// Specifies that fields from the source <see cref="object" /> are copied to properties of the destination <see cref="object" /> in addition to its fields, as long as the name matches.
		/// </summary>
		FieldsToProperties = 16,
		/// <summary>
		/// Specifies that non-public properties or fields are included.
		/// </summary>
		NonPublic = 32,
		/// <summary>
		/// Specifies that static properties or fields are included.
		/// </summary>
		Static = 64
	}
}