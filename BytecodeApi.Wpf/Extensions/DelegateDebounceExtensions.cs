using System.Windows.Threading;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods that debounces delegate objects.
/// </summary>
public static class DelegateDebounceExtensions
{
	extension(Action action)
	{
		/// <summary>
		/// Debounces the specified <see cref="Action" /> by a given delay and returns a new <see cref="Action" /> that wraps <paramref name="action" />.
		/// </summary>
		/// <param name="delay">The delay, in milliseconds, to wait to call <paramref name="action" />.</param>
		/// <param name="dispatcher">The <see cref="Dispatcher" /> to be used when invoking <paramref name="action" />, or <see langword="null" /> to call <paramref name="action" /> directly.</param>
		/// <returns>
		/// A new <see cref="Action" /> that wraps <paramref name="action" />.
		/// </returns>
		public Action Debounce(int delay, Dispatcher? dispatcher)
		{
			return action.Debounce(TimeSpan.FromMilliseconds(delay), dispatcher);
		}
		/// <summary>
		/// Debounces the specified <see cref="Action" /> by a given delay and returns a new <see cref="Action" /> that wraps <paramref name="action" />.
		/// </summary>
		/// <param name="delay">The delay to wait to call <paramref name="action" />.</param>
		/// <param name="dispatcher">The <see cref="Dispatcher" /> to be used when invoking <paramref name="action" />, or <see langword="null" /> to call <paramref name="action" /> directly.</param>
		/// <returns>
		/// A new <see cref="Action" /> that wraps <paramref name="action" />.
		/// </returns>
		public Action Debounce(TimeSpan delay, Dispatcher? dispatcher)
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
								if (dispatcher == null)
								{
									action();
								}
								else
								{
									dispatcher.Invoke(action);
								}
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
		/// Debounces the specified <see cref="Action{T}" /> by a given delay and returns a new <see cref="Action{T}" /> that wraps <paramref name="action" />.
		/// </summary>
		/// <param name="delay">The delay, in milliseconds, to wait to call <paramref name="action" />.</param>
		/// <param name="dispatcher">The <see cref="Dispatcher" /> to be used when invoking <paramref name="action" />, or <see langword="null" /> to call <paramref name="action" /> directly.</param>
		/// <returns>
		/// A new <see cref="Action{T}" /> that wraps <paramref name="action" />.
		/// </returns>
		public Action<T> Debounce(int delay, Dispatcher? dispatcher)
		{
			return action.Debounce(TimeSpan.FromMilliseconds(delay), dispatcher);
		}
		/// <summary>
		/// Debounces the specified <see cref="Action{T}" /> by a given delay and returns a new <see cref="Action{T}" /> that wraps <paramref name="action" />.
		/// </summary>
		/// <param name="delay">The delay to wait to call <paramref name="action" />.</param>
		/// <param name="dispatcher">The <see cref="Dispatcher" /> to be used when invoking <paramref name="action" />, or <see langword="null" /> to call <paramref name="action" /> directly.</param>
		/// <returns>
		/// A new <see cref="Action{T}" /> that wraps <paramref name="action" />.
		/// </returns>
		public Action<T> Debounce(TimeSpan delay, Dispatcher? dispatcher)
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
								if (dispatcher == null)
								{
									action(arg);
								}
								else
								{
									dispatcher.Invoke(() => action(arg));
								}
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