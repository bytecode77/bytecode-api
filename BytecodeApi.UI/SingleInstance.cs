using System;
using System.Threading;
using System.Windows;

namespace BytecodeApi.UI
{
	/// <summary>
	/// Class for managing single instance UI applications. A second instance can detect an already running instance and notify the first instance.
	/// </summary>
	public class SingleInstance : IDisposable
	{
		private readonly Mutex Mutex;
		private readonly HwndBroadcast Broadcast;
		/// <summary>
		/// Occurs when <see cref="SendActivationMessage" /> is called by another running instance.
		/// </summary>
		public event EventHandler Activated;

		/// <summary>
		/// Initializes a new instance of the <see cref="SingleInstance" /> class and registers a <see cref="System.Threading.Mutex" /> and a WindowMessage using the specified identifier.
		/// </summary>
		/// <param name="identifier">A <see cref="string" /> representing the identifier for the <see cref="System.Threading.Mutex" /> and the WindowMessage.</param>
		public SingleInstance(string identifier)
		{
			Check.ArgumentNull(identifier, nameof(identifier));
			Check.ArgumentEx.StringNotEmpty(identifier, nameof(identifier));

			Mutex = new Mutex(false, "BAPI_SINGLE_INSTANCE_" + identifier);
			Broadcast = new HwndBroadcast("SINGLE_INSTANCE_" + identifier);
			Broadcast.Notified += Broadcast_Notified;
		}
		/// <summary>
		/// Releases all resources used by the current instance of the <see cref="SingleInstance" /> class.
		/// </summary>
		public void Dispose()
		{
			Mutex.Dispose();
			Broadcast.Dispose();
		}

		/// <summary>
		/// Registers a <see cref="Window" /> object that identifies as the main application window.
		/// </summary>
		/// <param name="window">The <see cref="Window" /> object identifying as the main application window.</param>
		public void RegisterWindow(Window window)
		{
			Check.ArgumentNull(window, nameof(window));

			Broadcast.RegisterWindow(window);
		}
		/// <summary>
		/// Registers a window handle (HWND) that identifies as the main application window.
		/// </summary>
		/// <param name="handle">A <see cref="IntPtr" /> representing window handle (HWND).</param>
		public void RegisterWindow(IntPtr handle)
		{
			Check.ArgumentEx.Handle(handle, nameof(handle));

			Broadcast.RegisterWindow(handle);
		}
		/// <summary>
		/// Checks whether an instance is already running by querying the <see cref="System.Threading.Mutex" />.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if an instance is already running;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool CheckInstanceRunning()
		{
			try
			{
				return !Mutex.WaitOne(0, false);
			}
			catch (AbandonedMutexException)
			{
				return false;
			}
		}
		/// <summary>
		/// Sends a notification to other running instances. The <see cref="Activated" /> event will be triggered in all instances, except the current.
		/// </summary>
		public void SendActivationMessage()
		{
			Broadcast.Notify();
		}
		private void Broadcast_Notified(object sender, EventArgs e)
		{
			OnActivated(EventArgs.Empty);
		}

		/// <summary>
		/// Raises the <see cref="Activated" /> event.
		/// </summary>
		/// <param name="e">The event data for the <see cref="Activated" /> event.</param>
		protected virtual void OnActivated(EventArgs e)
		{
			Activated?.Invoke(this, e);
		}
	}
}