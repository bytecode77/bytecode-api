using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;
using BytecodeApi.Threading;
using System;
using System.IO;
using System.IO.Pipes;
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
		private readonly string PipeName;
		private readonly NamedPipeServerStream Pipe;
		private readonly Thread PipeThread;
		private readonly int PipeIdentifier;
		/// <summary>
		/// Occurs when <see cref="SendActivationMessage" /> is called by another running instance.
		/// </summary>
		public event EventHandler Activated;
		/// <summary>
		/// Occurs when <see cref="SendMessage" /> is called by another running instance.
		/// </summary>
		public event EventHandler<string> MessageReceived;

		/// <summary>
		/// Initializes a new instance of the <see cref="SingleInstance" /> class and registers a <see cref="System.Threading.Mutex" /> and a WindowMessage using the specified identifier.
		/// </summary>
		/// <param name="identifier">A <see cref="string" /> representing the identifier for the <see cref="System.Threading.Mutex" /> and the WindowMessage.</param>
		public SingleInstance(string identifier)
		{
			Check.ArgumentNull(identifier, nameof(identifier));
			Check.ArgumentEx.StringNotEmpty(identifier, nameof(identifier));

			Mutex = new Mutex(false, "BAPI_SINGLE_INSTANCE_" + identifier);
			Broadcast = new HwndBroadcast("BAPI_SINGLE_INSTANCE_BROADCAST_" + identifier);
			Broadcast.Notified += Broadcast_Notified;

			PipeName = "BAPI_SINGLE_INSTANCE_PIPE_" + identifier;
			Pipe = new NamedPipeServerStream(PipeName, PipeDirection.In, NamedPipeServerStream.MaxAllowedServerInstances);
			PipeThread = ThreadFactory.StartThread(PipeThreadFunc);

			lock (MathEx._RandomNumberGenerator)
			{
				PipeIdentifier = MathEx._RandomNumberGenerator.GetInt32();
			}
		}
		/// <summary>
		/// Releases all resources used by the current instance of the <see cref="SingleInstance" /> class.
		/// </summary>
		public void Dispose()
		{
			Mutex.Dispose();
			Broadcast.Notified -= Broadcast_Notified;
			Broadcast.Dispose();
			PipeThread.Abort();
			Pipe.Dispose();
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
		/// <summary>
		/// Sends a message to other running instances. The <see cref="MessageReceived" /> event will be triggered in all instances, except the current.
		/// </summary>
		public void SendMessage(string message)
		{
			Check.ArgumentNull(message, nameof(message));

			using (NamedPipeClientStream client = new NamedPipeClientStream(".", PipeName, PipeDirection.Out))
			{
				client.Connect();

				using (StreamWriter stream = new StreamWriter(client))
				{
					stream.WriteLine(PipeIdentifier + ":" + message);
				}
			}
		}
		private void Broadcast_Notified(object sender, EventArgs e)
		{
			OnActivated(EventArgs.Empty);
		}
		private void PipeThreadFunc()
		{
			while (true)
			{
				Pipe.WaitForConnection();
				string message = new StreamReader(Pipe).ReadToEnd();
				Pipe.Disconnect();

				if (message.Contains(":") && message.SubstringUntil(":").ToInt32OrDefault() is int identifier)
				{
					message = message.SubstringFrom(":").Trim();

					if (identifier == PipeIdentifier) SendMessage(message);
					else OnMessageReceived(message);
				}
			}
		}

		/// <summary>
		/// Raises the <see cref="Activated" /> event.
		/// </summary>
		/// <param name="e">The event data for the <see cref="Activated" /> event.</param>
		protected virtual void OnActivated(EventArgs e)
		{
			Activated?.Invoke(this, e);
		}
		/// <summary>
		/// Raises the <see cref="MessageReceived" /> event.
		/// </summary>
		/// <param name="message">The message for the <see cref="MessageReceived" /> event.</param>
		protected virtual void OnMessageReceived(string message)
		{
			MessageReceived?.Invoke(this, message);
		}
	}
}