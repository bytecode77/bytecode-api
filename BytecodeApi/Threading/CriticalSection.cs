namespace BytecodeApi.Threading;

/// <summary>
/// Provides a wrapper for an <see cref="Action" /> to suppress multiple invocations.
/// </summary>
public class CriticalSection
{
	private int RunningCount;
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating whether an <see cref="Action" /> is currently running.
	/// </summary>
	public bool IsRunning => RunningCount > 0;

	/// <summary>
	/// Invokes <paramref name="action" />, if it is not already running, and prevents other invocations until <paramref name="action" /> finished.
	/// </summary>
	/// <param name="action">The <see cref="Action" /> to be invoked.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="action" /> was invoked.
	/// <see langword="false" />, if <paramref name="action" /> is already running and was not invoked.
	/// </returns>
	public bool Invoke(Action action)
	{
		Check.ArgumentNull(action);

		if (Interlocked.CompareExchange(ref RunningCount, 1, 0) > 0)
		{
			return false;
		}

		try
		{
			action();
		}
		finally
		{
			Interlocked.Decrement(ref RunningCount);
		}

		return true;
	}
}