using BytecodeApi.Extensions;
using System;
using System.Diagnostics;

namespace BytecodeApi.IO.Debugging
{
	/// <summary>
	/// Directs tracing or debugging output to event handlers.
	/// </summary>
	public class DelegateTraceListener : TraceListener
	{
		/// <summary>
		/// Occurs when an event is traced.
		/// </summary>
		public event EventHandler<TraceEventInfo> EventReceived;
		/// <summary>
		/// Occurs when data is traced.
		/// </summary>
		public event EventHandler<TraceDataInfo> DataReceived;
		/// <summary>
		/// Occurs when a transfer is traced.
		/// </summary>
		public event EventHandler<TraceTransferInfo> TransferReceived;

		/// <summary>
		/// Initializes a new instance of the <see cref="DelegateTraceListener" /> class.
		/// </summary>
		public DelegateTraceListener()
		{
		}

		/// <summary>
		/// Writes trace and event information to the listener specific output.
		/// </summary>
		/// <param name="eventCache">A <see cref="TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
		{
			OnEventReceived(new TraceEventInfo(eventCache, source, eventType, id));
		}
		/// <summary>
		/// Writes trace information, a message, and event information to the listener specific output.
		/// </summary>
		/// <param name="eventCache">A <see cref="TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A message to write.</param>
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			OnEventReceived(new TraceEventInfo(eventCache, source, eventType, id, message));
		}
		/// <summary>
		/// Writes trace information, a formatted array of objects and event information to the listener specific output.
		/// </summary>
		/// <param name="eventCache">A <see cref="TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A format <see cref="string" /> that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An <see cref="object" /> array containing zero or more objects to format.</param>
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			OnEventReceived(new TraceEventInfo(eventCache, source, eventType, id, args == null ? format : format.Format(args)));
		}
		/// <summary>
		/// Writes trace information, a data object and event information to the listener specific output.
		/// </summary>
		/// <param name="eventCache">A <see cref="TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">The trace data to emit.</param>
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			OnDataReceived(new TraceDataInfo(eventCache, source, eventType, id, data));
		}
		/// <summary>
		/// Writes trace information, an array of data objects and event information to the listener specific output.
		/// </summary>
		/// <param name="eventCache">A <see cref="TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">An array of objects to emit as data.</param>
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			OnDataReceived(new TraceDataInfo(eventCache, source, eventType, id, data));
		}
		/// <summary>
		/// Writes trace information, a message, a related activity identity and event information to the listener specific output.
		/// </summary>
		/// <param name="eventCache">A <see cref="TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A message to write.</param>
		/// <param name="relatedActivityId">A <see cref="Guid" /> object identifying a related activity.</param>
		public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			OnTransferReceived(new TraceTransferInfo(eventCache, source, id, message, relatedActivityId));
		}
		/// <summary>
		/// When overridden in a derived class, writes the specified message to the listener you create in the derived class.
		/// </summary>
		/// <param name="message">A message to write.</param>
		public override void Write(string message)
		{
		}
		/// <summary>
		/// When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.
		/// </summary>
		/// <param name="message">A message to write.</param>
		public override void WriteLine(string message)
		{
		}

		/// <summary>
		/// Raises the <see cref="EventReceived" /> event.
		/// </summary>
		/// <param name="traceEvent">The event data for the <see cref="EventReceived" /> event.</param>
		protected void OnEventReceived(TraceEventInfo traceEvent)
		{
			EventReceived?.Invoke(this, traceEvent);
		}
		/// <summary>
		/// Raises the <see cref="DataReceived" /> event.
		/// </summary>
		/// <param name="traceData">The event data for the <see cref="DataReceived" /> event.</param>
		protected void OnDataReceived(TraceDataInfo traceData)
		{
			DataReceived?.Invoke(this, traceData);
		}
		/// <summary>
		/// Raises the <see cref="TransferReceived" /> event.
		/// </summary>
		/// <param name="traceTransfer">The event data for the <see cref="TransferReceived" /> event.</param>
		protected void OnTransferReceived(TraceTransferInfo traceTransfer)
		{
			TransferReceived?.Invoke(this, traceTransfer);
		}
	}
}