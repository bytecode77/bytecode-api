using System.Diagnostics;

namespace BytecodeApi;

/// <summary>
/// Class that can be used to guard loops by specifying a maximum number of iterations and/or a timeout.
/// If any of these is exceeded, an infinite loop is assumed and an exception is thrown.
/// </summary>
public sealed class IterationGuard
{
	private readonly Stopwatch Stopwatch;
	/// <summary>
	/// Gets the maximum number of iterations, or <see langword="null" />, if no maximum number of iterations is specified.
	/// </summary>
	public int? MaxIterations { get; }
	/// <summary>
	/// Gets the timeout for the operation, or <see langword="null" />, if no timeout is specified.
	/// </summary>
	public TimeSpan? Timeout { get; }
	/// <summary>
	/// Gets the current number of iterations that have been executed.
	/// This property is incremented by calling the <see cref="Next" /> method.
	/// </summary>
	public int Iterations { get; private set; }
	/// <summary>
	/// Gets the time elapsed since the creation of this <see cref="IterationGuard" /> instance.
	/// </summary>
	public TimeSpan Elapsed => Stopwatch.Elapsed;

	/// <summary>
	/// Initializes a new instance of the <see cref="IterationGuard" /> class.
	/// </summary>
	/// <param name="maxIterations">The maximum number of iterations, or <see langword="null" />, to specify no maximum number of iterations.</param>
	/// <param name="timeout">The timeout for the operation, or <see langword="null" />, to specify no timeout.</param>
	public IterationGuard(int? maxIterations = null, TimeSpan? timeout = null)
	{
		if (maxIterations == null && timeout == null) throw Throw.Argument(nameof(maxIterations), $"At least one of {nameof(maxIterations)} or {nameof(timeout)} must be specified.");
		if (maxIterations != null) Check.ArgumentOutOfRangeEx.Greater0(maxIterations.Value);
		if (timeout != null) Check.ArgumentOutOfRangeEx.Greater0(timeout.Value);

		MaxIterations = maxIterations;
		Timeout = timeout;
		Stopwatch = Stopwatch.StartNew();
	}

	/// <summary>
	/// Advances the iteration guard by one step and checks for iteration or timeout limits.
	/// <para>Call this method at the start of each iteration to enforce iteration and timeout constraints.</para>
	/// <para>If either limit is exceeded, an infinite loop is assumed and an exception is thrown.</para>
	/// </summary>
	public void Next()
	{
		if (Iterations++ > MaxIterations)
		{
			throw new IterationGuardException(IterationGuardErrorReason.MaxIterationsExceeded, Iterations, Elapsed, $"Maximum number of {MaxIterations} iterations exceeded.");
		}

		if (Elapsed > Timeout)
		{
			throw new IterationGuardException(IterationGuardErrorReason.TimeoutExceeded, Iterations, Elapsed, $"Timeout of {Timeout} exceeded.");
		}
	}
	/// <summary>
	/// Resets the state of this instance.
	/// </summary>
	public void Reset()
	{
		Iterations = 0;
		Stopwatch.Restart();
	}
}