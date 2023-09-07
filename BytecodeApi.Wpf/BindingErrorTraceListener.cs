using System.Diagnostics;

namespace BytecodeApi.Wpf;

/// <summary>
/// Directs WPF binding error output to event handlers.
/// </summary>
public class BindingErrorTraceListener : TraceListener
{
	/// <summary>
	/// Occurs when a WPF binding error is traced.
	/// </summary>
	public event EventHandler<string>? EventReceived;

	/// <summary>
	/// Initializes a new instance of the <see cref="BindingErrorTraceListener" /> class.
	/// </summary>
	public BindingErrorTraceListener()
	{
		TraceSource traceSource = PresentationTraceSources.DataBindingSource;
		traceSource.Listeners.Add(this);
		traceSource.Switch.Level = SourceLevels.Information;
	}

	/// <summary>
	/// When overridden in a derived class, writes the specified message to the listener you create in the derived class.
	/// </summary>
	/// <param name="message">A message to write.</param>
	public override void Write(string? message)
	{
	}
	/// <summary>
	/// When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.
	/// </summary>
	/// <param name="message">A message to write.</param>
	public override void WriteLine(string? message)
	{
		OnEventReceived(message ?? "");
	}

	/// <summary>
	/// Raises the <see cref="EventReceived" /> event.
	/// </summary>
	/// <param name="message">The message for the <see cref="EventReceived" /> event.</param>
	protected virtual void OnEventReceived(string message)
	{
		EventReceived?.Invoke(this, message);
	}
}