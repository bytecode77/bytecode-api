using System;

namespace BytecodeApi.IO.Cli
{
	/// <summary>
	/// Specifies a validation source for a <see cref="CliException" />, if validated using the <see cref="ParsedOptionSet.Assert" /> object.
	/// </summary>
	public enum CliValidationSource
	{
		/// <summary>
		/// The exception is a general error that did not occur during validation using the <see cref="ParsedOptionSet.Assert" /> object.
		/// </summary>
		None,
		/// <summary>
		/// The exception occurred in the <see cref="ParsedOptionSet.AssertHelper.OptionRequired(string)" /> method.
		/// </summary>
		OptionRequired,
		/// <summary>
		/// The exception occurred in the <see cref="ParsedOptionSet.AssertHelper.OptionNotDuplicate(string)" /> method.
		/// </summary>
		OptionNotDuplicate,
		/// <summary>
		/// The exception occurred in the <see cref="ParsedOptionSet.AssertHelper.ValueCount(string, int)" /> method.
		/// </summary>
		ValueCount,
		/// <summary>
		/// The exception occurred in the <see cref="ParsedOptionSet.AssertHelper.MinimumValueCount(string, int)" /> method.
		/// </summary>
		MinimumValueCount,
		/// <summary>
		/// The exception occurred in the <see cref="ParsedOptionSet.AssertHelper.MaximumValueCount(string, int)" /> method.
		/// </summary>
		MaximumValueCount,
		/// <summary>
		/// The exception occurred in the <see cref="ParsedOptionSet.AssertHelper.Custom(string, Predicate{string[]})" /> method.
		/// </summary>
		Custom,
		/// <summary>
		/// The exception occurred in the <see cref="ParsedOptionSet.AssertHelper.Int32(string)" /> method.
		/// </summary>
		Int32,
		/// <summary>
		/// The exception occurred in the <see cref="ParsedOptionSet.AssertHelper.FileExists(string)" /> method.
		/// </summary>
		FileExists,
		/// <summary>
		/// The exception occurred in the <see cref="ParsedOptionSet.AssertHelper.DirectoryExists(string)" /> method.
		/// </summary>
		DirectoryExists,
		/// <summary>
		/// The exception occurred in the <see cref="ParsedOptionSet.AssertHelper.FileExtension(string, string[])" /> method.
		/// </summary>
		FileExtension
	}
}