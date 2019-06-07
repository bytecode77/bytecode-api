using System;

namespace BytecodeApi.IO.Cli
{
	/// <summary>
	/// The exception that is thrown when commandline parsing failed or was asserted using the <see cref="ParsedOptionSet.Assert" /> object.
	/// </summary>
	public sealed class CliException : Exception
	{
		/// <summary>
		/// Gets a <see cref="CliValidationSource" /> value that indicates what validation took place using the <see cref="ParsedOptionSet.Assert" /> object. For general exceptions, <see cref="CliValidationSource.None" /> is returned.
		/// </summary>
		public CliValidationSource ValidationSource { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CliException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public CliException(string message) : this(CliValidationSource.None, message)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CliException" /> class.
		/// </summary>
		/// <param name="validationSource">If validated using the <see cref="ParsedOptionSet.Assert" /> object, indicates what validation took place.</param>
		/// <param name="message">The message that describes the error.</param>
		public CliException(CliValidationSource validationSource, string message) : base(message)
		{
			ValidationSource = validationSource;
		}
	}
}