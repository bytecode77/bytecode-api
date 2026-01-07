namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods that debounces delegate objects.
/// </summary>
public static class DelegateDebounceExtensions
{
	extension(Action action)
	{
		/// <summary>
		/// Debounces the specified <see cref="Action" /> by a given delay and returns a new <see cref="Action" /> that wraps this <see cref="Action" />.
		/// </summary>
		/// <param name="delay">The delay, in milliseconds, to wait to call this <see cref="Action" />.</param>
		/// <returns>
		/// A new <see cref="Action" /> that wraps this <see cref="Action" />.
		/// </returns>
		public Action Debounce(int delay)
		{
			return action.Debounce(TimeSpan.FromMilliseconds(delay));
		}
		/// <summary>
		/// Debounces the specified <see cref="Action" /> by a given delay and returns a new <see cref="Action" /> that wraps this <see cref="Action" />.
		/// </summary>
		/// <param name="delay">The delay to wait to call this <see cref="Action" />.</param>
		/// <returns>
		/// A new <see cref="Action" /> that wraps this <see cref="Action" />.
		/// </returns>
		public Action Debounce(TimeSpan delay)
		{
			Check.ArgumentNull(action);

			CancellationTokenSource? cancel = null;

			return () =>
			{
				cancel?.Cancel();
				cancel = new();

				Task
					.Delay(delay, cancel.Token)
					.ContinueWith(task =>
					{
						try
						{
							if (task.IsCompletedSuccessfully)
							{
								action();
							}
						}
						catch (TaskCanceledException)
						{
						}
					}, TaskScheduler.Default);
			};
		}
	}

	extension<T>(Action<T> action)
	{
		/// <summary>
		/// Debounces the specified <see cref="Action{T}" /> by a given delay and returns a new <see cref="Action{T}" /> that wraps this <see cref="Action{T}" />.
		/// </summary>
		/// <param name="delay">The delay, in milliseconds, to wait to call this <see cref="Action{T}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T}" /> that wraps this <see cref="Action{T}" />.
		/// </returns>
		public Action<T> Debounce(int delay)
		{
			return action.Debounce(TimeSpan.FromMilliseconds(delay));
		}
		/// <summary>
		/// Debounces the specified <see cref="Action{T}" /> by a given delay and returns a new <see cref="Action{T}" /> that wraps this <see cref="Action{T}" />.
		/// </summary>
		/// <param name="delay">The delay to wait to call this <see cref="Action{T}" />.</param>
		/// <returns>
		/// A new <see cref="Action{T}" /> that wraps this <see cref="Action{T}" />.
		/// </returns>
		public Action<T> Debounce(TimeSpan delay)
		{
			Check.ArgumentNull(action);

			CancellationTokenSource? cancel = null;

			return arg =>
			{
				cancel?.Cancel();
				cancel = new();

				Task
					.Delay(delay, cancel.Token)
					.ContinueWith(task =>
					{
						try
						{
							if (task.IsCompletedSuccessfully)
							{
								action(arg);
							}
						}
						catch (TaskCanceledException)
						{
						}
					}, TaskScheduler.Default);
			};
		}
	}
}