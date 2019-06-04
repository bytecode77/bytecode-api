using System.Diagnostics;

namespace BytecodeApi.IO.Debugging
{
	/// <summary>
	/// Specifies trace data.
	/// </summary>
	public sealed class TraceDataInfo
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
		/// Gets the array of emitted data objects.
		/// </summary>
		public object[] Data { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TraceDataInfo" /> class.
		/// </summary>
		public TraceDataInfo()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TraceDataInfo" /> class with the specified trace data parameters.
		/// </summary>
		/// <param name="eventCache">A <see cref="TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">The array of emitted data objects.</param>
		public TraceDataInfo(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			EventCache = eventCache;
			Source = source;
			EventType = eventType;
			Id = id;
			Data = data;
		}
	}
}