namespace BytecodeApi.IO;

/// <summary>
/// Specifies the result of the execution of a file.
/// </summary>
public sealed class CliResult
{
	/// <summary>
	/// Gets the exit code of the process.
	/// </summary>
	public int ExitCode { get; }
	/// <summary>
	/// Gets the console output of the process.
	/// </summary>
	public string Output { get; }
	/// <summary>
	/// Gets the timestamp when the process started.
	/// </summary>
	public DateTime StartTime { get; }
	/// <summary>
	/// Gets the timestamp when the process exited.
	/// </summary>
	public DateTime EndTime { get; }
	/// <summary>
	/// Gets the amount of time that the process needed to complete.
	/// </summary>
	public TimeSpan RunTime => EndTime - StartTime;

	internal CliResult(int exitCode, string output, DateTime startTime, DateTime endTime)
	{
		ExitCode = exitCode;
		Output = output;
		StartTime = startTime;
		EndTime = endTime;
	}
}