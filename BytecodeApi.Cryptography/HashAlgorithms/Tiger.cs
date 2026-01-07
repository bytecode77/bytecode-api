namespace BytecodeApi.Cryptography.HashAlgorithms;

/// <summary>
/// Computes the <see cref="Tiger" /> hash for the input data.
/// </summary>
public sealed class Tiger : TigerBase
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Tiger" /> class.
	/// </summary>
	public Tiger() : base(1)
	{
	}
	/// <summary>
	/// Creates an instance of the default implementation of <see cref="Tiger" />.
	/// </summary>
	/// <returns>
	/// A new instance of <see cref="Tiger" />.
	/// </returns>
	public static new Tiger Create()
	{
		return new();
	}
}