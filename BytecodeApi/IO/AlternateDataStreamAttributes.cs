namespace BytecodeApi.IO;

/// <summary>
/// Specifies the attributes of an alternate data stream.
/// </summary>
[Flags]
public enum AlternateDataStreamAttributes
{
	/// <summary>
	/// The alternate data stream has no attributes.
	/// </summary>
	None = 0,
	/// <summary>
	/// The alternate data stream contains data that is modified when read.
	/// </summary>
	ModifiedWhenRead = 1,
	/// <summary>
	/// The alternate data stream contains security data.
	/// </summary>
	ContainsSecurity = 2,
	/// <summary>
	/// The alternate data stream contains properties.
	/// </summary>
	ContainsProperties = 4,
	/// <summary>
	/// The alternate data stream is sparse.
	/// </summary>
	Sparse = 8
}