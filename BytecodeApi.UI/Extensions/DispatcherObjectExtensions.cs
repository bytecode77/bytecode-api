using System;
using System.Windows.Threading;

namespace BytecodeApi.UI.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="DispatcherObject" /> objects.
	/// </summary>
	public static class DispatcherObjectExtensions
	{
		/// <summary>
		/// Invokes an empty <see cref="Action" /> on the <see cref="Dispatcher" /> of this <see cref="DispatcherObject" />, using the <see cref="DispatcherPriority.Render" /> priority, thereby refreshing the UI.
		/// </summary>
		/// <param name="dispatcherObject">The <see cref="DispatcherObject" /> to be refreshed.</param>
		public static void Refresh(this DispatcherObject dispatcherObject)
		{
			dispatcherObject.Refresh(DispatcherPriority.Render);
		}
		/// <summary>
		/// Invokes an empty <see cref="Action" /> on the <see cref="Dispatcher" /> of this <see cref="DispatcherObject" />, using the specified priority, thereby refreshing the UI.
		/// </summary>
		/// <param name="dispatcherObject">The <see cref="DispatcherObject" /> to be refreshed.</param>
		/// <param name="priority">The <see cref="DispatcherPriority" /> to be used during <see cref="Dispatcher" /> invocation.</param>
		public static void Refresh(this DispatcherObject dispatcherObject, DispatcherPriority priority)
		{
			Check.ArgumentNull(dispatcherObject, nameof(dispatcherObject));

			dispatcherObject.Dispatcher.Invoke(delegate { }, priority);
		}
	}
}