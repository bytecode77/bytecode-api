using System.Windows.Threading;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="DispatcherTimer" /> objects.
/// </summary>
public static class DispatcherTimerExtensions
{
	extension(DispatcherTimer)
	{
		/// <summary>
		/// Creates and starts a new <see cref="DispatcherTimer" /> with the specified <see cref="Action" /> and interval.
		/// </summary>
		/// <param name="action">The <see cref="Action" /> to be invoked by the new <see cref="DispatcherTimer" />.</param>
		/// <param name="interval">The interval in which <paramref name="action" /> is invoked.</param>
		/// <returns>
		/// The <see cref="DispatcherTimer" /> this method creates.
		/// </returns>
		public static DispatcherTimer StartNew(Action action, TimeSpan interval)
		{
			return StartNew(action, interval, DispatcherPriority.Background);
		}
		/// <summary>
		/// Creates and starts a new <see cref="DispatcherTimer" /> with the specified <see cref="Action" /> and interval.
		/// </summary>
		/// <param name="action">The <see cref="Action" /> to be invoked by the new <see cref="DispatcherTimer" />.</param>
		/// <param name="interval">The interval in which <paramref name="action" /> is invoked.</param>
		/// <param name="priority">The priority at which to invoke the timer.</param>
		/// <returns>
		/// The <see cref="DispatcherTimer" /> this method creates.
		/// </returns>
		public static DispatcherTimer StartNew(Action action, TimeSpan interval, DispatcherPriority priority)
		{
			return StartNew(action, interval, priority, false);
		}
		/// <summary>
		/// Creates and starts a new <see cref="DispatcherTimer" /> with the specified <see cref="Action" /> and interval.
		/// </summary>
		/// <param name="action">The <see cref="Action" /> to be invoked by the new <see cref="DispatcherTimer" />.</param>
		/// <param name="interval">The interval in which <paramref name="action" /> is invoked.</param>
		/// <param name="priority">The priority at which to invoke the timer.</param>
		/// <param name="executeNow"><see langword="true" /> to execute <paramref name="action" /> immediately; <see langword="false" /> to execute it on the first interval.</param>
		/// <returns>
		/// The <see cref="DispatcherTimer" /> this method creates.
		/// </returns>
		public static DispatcherTimer StartNew(Action action, TimeSpan interval, DispatcherPriority priority, bool executeNow)
		{
			return StartNew(action, interval, priority, executeNow, false);
		}
		/// <summary>
		/// Creates and starts a new <see cref="DispatcherTimer" /> with the specified <see cref="Action" /> and interval.
		/// </summary>
		/// <param name="action">The <see cref="Action" /> to be invoked by the new <see cref="DispatcherTimer" />.</param>
		/// <param name="interval">The interval in which <paramref name="action" /> is invoked.</param>
		/// <param name="priority">The priority at which to invoke the timer.</param>
		/// <param name="executeNow"><see langword="true" /> to execute <paramref name="action" /> immediately; <see langword="false" /> to execute it on the first interval.</param>
		/// <param name="ignoreExceptions"><see langword="true" /> to ignore all exceptions.</param>
		/// <returns>
		/// The <see cref="DispatcherTimer" /> this method creates.
		/// </returns>
		public static DispatcherTimer StartNew(Action action, TimeSpan interval, DispatcherPriority priority, bool executeNow, bool ignoreExceptions)
		{
			Check.ArgumentNull(action);
			Check.ArgumentOutOfRangeEx.GreaterEqual0(interval);

			DispatcherTimer timer = new(priority) { Interval = interval };
			timer.Tick += delegate { ExecuteAction(); };
			timer.Start();

			if (executeNow)
			{
				ExecuteAction();
			}

			return timer;

			void ExecuteAction()
			{
				try
				{
					action();
				}
				catch when (ignoreExceptions)
				{
				}
			}
		}
	}
}