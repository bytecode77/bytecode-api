using System.Diagnostics;

namespace BytecodeApi.IO.Debugging
{
	/// <summary>
	/// Specifies a trace event.
	/// </summary>
	public sealed class TraceEventInfo
	{
		/// <summary>
		/// Gets the <see cref="TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.
		/// </summary>
		public TraceEventCache EventCache { get; private set; }
		/// <summary>
		/// Gets the name used to identify the output, typically the name of the application that generated the trace event.
		/// </summary>
		public string Source { get; private set; }
		/// <summary>
		/// Gets the <see cref="TraceEventType" /> value specifying the type of event that has caused the trace.
		/// </summary>
		public TraceEventType EventType { get; private set; }
		/// <summary>
		/// Gets the numeric identifier for the event.
		/// </summary>
		public int Id { get; private set; }
		/// <summary>
		/// Gets the event message.
		/// </summary>
		public string Message { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TraceEventInfo" /> class.
		/// </summary>
		public TraceEventInfo()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TraceEventInfo" /> class with the specified trace event parameters.
		/// </summary>
		/// <param name="eventCache">A <see cref="TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		public TraceEventInfo(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
		{
			EventCache = eventCache;
			Source = source;
			EventType = eventType;
			Id = id;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TraceEventInfo" /> class with the specified trace event parameters.
		/// </summary>
		/// <param name="eventCache">A <see cref="TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">The event message.</param>
		public TraceEventInfo(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message) : this(eventCache, source, eventType, id)
		{
			Message = message;
		}
	}
}