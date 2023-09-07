using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;
using BytecodeApi.Threading;
using System.IO.Pipes;
using System.Text;
using System.Windows;

namespace BytecodeApi.Wpf.Interop;

/// <summary>
/// Class for managing single instance UI applications. A second instance can detect an already running instance and notify the first instance.
/// </summary>
public class SingleInstance : IDisposable
{
	private readonly Mutex Mutex;
	private readonly HwndBroadcast Broadcast;
	private readonly string PipeName;
	private readonly NamedPipeServerStream Pipe;
	private readonly int PipeIdentifier;
	private bool Disposed;
	/// <summary>
	/// Occurs when <see cref="SendActivationMessage" /> is called by another running instance.
	/// </summary>
	public event EventHandler? Activated;
	/// <summary>
	/// Occurs when <see cref="SendMessage" /> is called by another running instance.
	/// </summary>
	public event EventHandler<string>? MessageReceived;

	/// <summary>
	/// Initializes a new instance of the <see cref="SingleInstance" /> class and registers a <see cref="System.Threading.Mutex" /> and a WindowMessage using the specified identifier.
	/// </summary>
	/// <param name="identifier">A <see cref="string" /> representing the identifier for the <see cref="System.Threading.Mutex" /> and the WindowMessage.</param>
	public SingleInstance(string identifier)
	{
		Check.ArgumentNull(identifier);
		Check.ArgumentEx.StringNotEmpty(identifier);

		Mutex = new(false, $"BAPI_SINGLE_INSTANCE_{identifier}");
		Broadcast = new($"BAPI_SINGLE_INSTANCE_BROADCAST_{identifier}");
		Broadcast.Notified += Broadcast_Notified;

		PipeName = $"BAPI_SINGLE_INSTANCE_PIPE_{identifier}";
		Pipe = new(PipeName, PipeDirection.In, NamedPipeServerStream.MaxAllowedServerInstances);
		PipeIdentifier = MathEx.RandomNumberGenerator.GetInt32();
		ThreadFactory.StartThread(PipeThreadFunc);
	}
	/// <summary>
	/// Releases all resources used by the current instance of the <see cref="SingleInstance" /> class.
	/// </summary>
	public void Dispose()
	{
		if (!Disposed)
		{
			Mutex.Dispose();
			Broadcast.Notified -= Broadcast_Notified;
			Broadcast.Dispose();
			Pipe.Dispose();

			Disposed = true;
		}
	}

	/// <summary>
	/// Registers a <see cref="Window" /> object that identifies as the main application window.
	/// </summary>
	/// <param name="window">The <see cref="Window" /> object identifying as the main application window.</param>
	public void RegisterWindow(Window window)
	{
		Check.ObjectDisposed<SingleInstance>(Disposed);
		Check.ArgumentNull(window);

		Broadcast.RegisterWindow(window);
	}
	/// <summary>
	/// Registers a window handle (HWND) that identifies as the main application window.
	/// </summary>
	/// <param name="handle">A <see cref="nint" /> representing window handle (HWND).</param>
	public void RegisterWindow(nint handle)
	{
		Check.ObjectDisposed<SingleInstance>(Disposed);
		Check.ArgumentEx.Handle(handle);

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
		Check.ObjectDisposed<SingleInstance>(Disposed);

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
		Check.ObjectDisposed<SingleInstance>(Disposed);

		Broadcast.Notify();
	}
	/// <summary>
	/// Sends a message to other running instances. The <see cref="MessageReceived" /> event will be triggered in all instances, except the current.
	/// </summary>
	public void SendMessage(string message)
	{
		Check.ObjectDisposed<SingleInstance>(Disposed);
		Check.ArgumentNull(message);

		using NamedPipeClientStream client = new(".", PipeName, PipeDirection.Out);

		try
		{
			client.Connect(1000);
		}
		catch (TimeoutException)
		{
			return;
		}

		using StreamWriter stream = new(client);
		stream.WriteLine($"{PipeIdentifier}:{message}");
	}
	private void Broadcast_Notified(object? sender, EventArgs e)
	{
		OnActivated(EventArgs.Empty);
	}
	private void PipeThreadFunc()
	{
		while (true)
		{
			Pipe.WaitForConnection();

			string message;
			try
			{
				using StreamReader streamReader = new(Pipe, Encoding.UTF8, true, -1, true);
				message = streamReader.ReadToEnd();
			}
			catch (ObjectDisposedException)
			{
				// This instance of SingleInstance was disposed
				return;
			}

			Pipe.Disconnect();

			if (message.Contains(':') && message.SubstringUntil(':').ToInt32OrDefault() is int identifier)
			{
				message = message.SubstringFrom(':').Trim();

				if (identifier == PipeIdentifier)
				{
					SendMessage(message);
				}
				else
				{
					OnMessageReceived(message);
				}
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