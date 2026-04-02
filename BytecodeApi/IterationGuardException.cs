namespace BytecodeApi;

/// <summary>
/// The exception that is thrown when a loop guarded by an <see cref="IterationGuard" /> instance is assumed to have become an infinite loop by exceeding the specified maximum number of iterations or a timeout.
/// </summary>
public sealed class IterationGuardException : InvalidOperationException
{
	/// <summary>
	/// Gets the reason for the iteration guard error.
	/// </summary>
	public IterationGuardErrorReason Reason { get; }
	/// <summary>
	/// Gets the number of iterations that have been executed.
	/// </summary>
	public int Iterations { get; }
	/// <summary>
	/// Gets the time elapsed since the creation of this <see cref="IterationGuard" /> instance, before the exception was thrown.
	/// </summary>
	public TimeSpan Elapsed { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="IterationGuardException" /> class.
	/// </summary>
	/// <param name="reason">The reason for the iteration guard error.</param>
	/// <param name="iterations">The number of iterations that have been executed.</param>
	/// <param name="elapsed">The time elapsed since the creation of this <see cref="IterationGuard" /> instance, before the exception was thrown.</param>
	/// <param name="message">The message that describes the error.</param>
	public IterationGuardException(IterationGuardErrorReason reason, int iterations, TimeSpan elapsed, string message) : base(message)
	{
		Reason = reason;
		Iterations = iterations;
		Elapsed = elapsed;
	}
}