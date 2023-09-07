using System.Diagnostics;
using System.Text;

namespace BytecodeApi.IO;

/// <summary>
/// Class to start processes and retrieve exit code and console output.
/// </summary>
public sealed class CliCommand
{
	private string _FileName = null!;
	private string? _Arguments;
	private string? _WorkingDirectory;
	private bool _UseShellExecute;
	private bool _Elevated;
	private bool _Hidden;

	private CliCommand()
	{
	}

	/// <summary>
	/// Specifies the file to be executed.
	/// </summary>
	/// <param name="fileName">The full path to the file to be executed.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public static CliCommand FileName(string fileName)
	{
		Check.ArgumentNull(fileName);

		return new()
		{
			_FileName = fileName
		};
	}
	/// <summary>
	/// Specifies arguments to pass to the executed process.
	/// </summary>
	/// <param name="arguments">Commandline arguments to pass to the executed process.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public CliCommand Arguments(string arguments)
	{
		Check.ArgumentNull(arguments);

		_Arguments = arguments;
		return this;
	}
	/// <summary>
	/// Specifies arguments to pass to the executed process.
	/// </summary>
	/// <param name="arguments">Commandline arguments to pass to the executed process.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public CliCommand Arguments(params string[] arguments)
	{
		Check.ArgumentNull(arguments);
		Check.ArgumentEx.ArrayValuesNotNull(arguments);

		_Arguments = CommandLine.FromArguments(arguments);
		return this;
	}
	/// <summary>
	/// Specifies the working directory of the executed process.
	/// </summary>
	/// <param name="workingDirectory">The working directory of the executed process.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public CliCommand WorkingDirectory(string workingDirectory)
	{
		Check.ArgumentNull(workingDirectory);

		_WorkingDirectory = workingDirectory;
		return this;
	}
	/// <summary>
	/// Specifies to use ShellExecute during process creation. Console output will not be read.
	/// </summary>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public CliCommand UseShellExecute()
	{
		_UseShellExecute = true;
		return this;
	}
	/// <summary>
	/// Specifies that the process will be started with elevated privileges. This option is only valid with UseShellExecute.
	/// </summary>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public CliCommand Elevated()
	{
		_Elevated = true;
		return this;
	}
	/// <summary>
	/// Specifies that the window of the process is hidden.
	/// </summary>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public CliCommand Hidden()
	{
		_Hidden = true;
		return this;
	}

	/// <summary>
	/// Executes this <see cref="CliCommand" /> and returns a <see cref="CliResult" /> object with the result.
	/// </summary>
	/// <returns>
	/// A <see cref="CliResult" /> object with the result.
	/// </returns>
	public CliResult Execute()
	{
		return Execute(Timeout.Infinite)!;
	}
	/// <summary>
	/// Executes this <see cref="CliCommand" /> and returns a <see cref="CliResult" /> object with the result, or <see langword="null" />, if the operation timed out.
	/// </summary>
	/// <param name="timeout">The amount of time, in milliseconds, to wait for the process to exit.</param>
	/// <returns>
	/// A <see cref="CliResult" /> object with the result, or <see langword="null" />, if the operation timed out.
	/// </returns>
	public CliResult? Execute(int timeout)
	{
		Check.Argument(!_Elevated || _UseShellExecute, null, "Elevated can only be used with UseShellExecute.");

		StringBuilder output = new();
		DateTime startTime = DateTime.Now;

		using Process process = new()
		{
			StartInfo =
			{
				FileName = _FileName,
				Arguments = _Arguments,
				WorkingDirectory = _WorkingDirectory,
				UseShellExecute = _UseShellExecute,
				Verb = _Elevated ? "runas" : "",
				RedirectStandardOutput = !_UseShellExecute,
				RedirectStandardError = !_UseShellExecute,
				CreateNoWindow = _Hidden,
				WindowStyle = _Hidden ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal
			},
			EnableRaisingEvents = true
		};

		process.OutputDataReceived += DataReceived;
		process.ErrorDataReceived += DataReceived;

		process.Start();
		if (!_UseShellExecute)
		{
			process.BeginOutputReadLine();
			process.BeginErrorReadLine();
		}

		if (process.WaitForExit(timeout))
		{
			return new(process.ExitCode, output.ToString(), startTime, DateTime.Now);
		}
		else
		{
			return null;
		}

		void DataReceived(object sender, DataReceivedEventArgs e)
		{
			output.AppendLine(e.Data);
		}
	}
	//TODO:FEATURE: ExecuteAsync
}