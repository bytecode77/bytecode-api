namespace BytecodeApi.Cryptography.HashAlgorithms;

/// <summary>
/// Computes the <see cref="Tiger2" /> hash for the input data.
/// </summary>
public sealed class Tiger2 : TigerBase
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Tiger2" /> class.
	/// </summary>
	public Tiger2() : base(0x80)
	{
	}
	/// <summary>
	/// Creates an instance of the default implementation of <see cref="Tiger2" />.
	/// </summary>
	/// <returns>
	/// A new instance of <see cref="Tiger2" />.
	/// </returns>
	public static new Tiger2 Create()
	{
		return new();
	}
}