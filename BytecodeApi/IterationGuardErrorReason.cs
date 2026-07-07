namespace BytecodeApi;

/// <summary>
/// Specifies the reason why an iteration guard operation failed.
/// </summary>
public enum IterationGuardErrorReason
{
	/// <summary>
	/// The maximum number of iterations was exceeded.
	/// </summary>
	MaxIterationsExceeded,
	/// <summary>
	/// The timeout was exceeded.
	/// </summary>
	TimeoutExceeded
}