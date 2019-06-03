using System;
using System.Diagnostics;

namespace BytecodeApi.IO.Debugging
{
	/// <summary>
	/// Specifies a trace transfer.
	/// </summary>
	public sealed class TraceTransferInfo
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
		/// Gets the numeric identifier for the event.
		/// </summary>
		public int Id { get; private set; }
		/// <summary>
		/// Gets the event message.
		/// </summary>
		public string Message { get; private set; }
		/// <summary>
		/// Gets the <see cref="Guid" /> object identifying a related activity.
		/// </summary>
		public Guid RelatedActivityId { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TraceTransferInfo" /> class.
		/// </summary>
		public TraceTransferInfo()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TraceTransferInfo" /> class with the specified trace transfer parameters.
		/// </summary>
		/// <param name="eventCache">A <see cref="TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">The event message.</param>
		/// <param name="relatedActivityId">A <see cref="Guid" /> object identifying a related activity.</param>
		public TraceTransferInfo(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			EventCache = eventCache;
			Source = source;
			Id = id;
			Message = message;
			RelatedActivityId = relatedActivityId;
		}
	}
}