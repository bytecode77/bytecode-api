namespace BytecodeApi.Data;

/// <summary>
/// Represents an attribute that specifies a timeout for an operation.
/// </summary>
public sealed class TimeoutAttribute : Attribute
{
	/// <summary>
	/// Gets the timeout, in milliseconds.
	/// </summary>
	public int Milliseconds { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="TimeoutAttribute" /> class.
	/// </summary>
	/// <param name="milliseconds">The timeout, in milliseconds.</param>
	public TimeoutAttribute(int milliseconds)
	{
		Milliseconds = milliseconds;
	}
}