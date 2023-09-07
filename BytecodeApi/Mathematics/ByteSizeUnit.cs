using System.ComponentModel;

namespace BytecodeApi.Mathematics;

/// <summary>
/// Gets the unit that represents a <see cref="ByteSize" /> value.
/// <para>Example: bytes, KB, MB, GB, TB, PB, EB</para>
/// </summary>
public enum ByteSizeUnit
{
	/// <summary>
	/// Specifies the bytes unit (bytes).
	/// </summary>
	[Description("bytes")]
	Byte,
	/// <summary>
	/// Specifies the kilobytes unit (KB).
	/// </summary>
	[Description("KB")]
	KiloByte,
	/// <summary>
	/// Specifies the megabytes unit (MB).
	/// </summary>
	[Description("MB")]
	MegaByte,
	/// <summary>
	/// Specifies the gigabytes unit (GB).
	/// </summary>
	[Description("GB")]
	GigaByte,
	/// <summary>
	/// Specifies the terabytes unit (TB).
	/// </summary>
	[Description("TB")]
	TeraByte,
	/// <summary>
	/// Specifies the petabytes unit (PB).
	/// </summary>
	[Description("PB")]
	PetaByte,
	/// <summary>
	/// Specifies the exabytes unit (EB).
	/// </summary>
	[Description("EB")]
	ExaByte
}