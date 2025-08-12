using BytecodeApi.Extensions;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace BytecodeApi.IO;

/// <summary>
/// Helper class for parsing of commandlines.
/// </summary>
public static class CommandLine
{
	/// <summary>
	/// Parses commandline arguments from a <see cref="string" /> and returns the equivalent <see cref="string" />[] with split commandline arguments.
	/// </summary>
	/// <param name="commandLine">A <see cref="string" /> specifying a commandline.</param>
	/// <returns>
	/// The equivalent <see cref="string" />[] with split commandline arguments from the given commandline.
	/// </returns>
	[SupportedOSPlatform("windows")]
	public static string[] GetArguments(string commandLine)
	{
		Check.ArgumentNull(commandLine);

		if (commandLine.IsNullOrWhiteSpace())
		{
			return [];
		}
		else
		{
			nint argumentsPtr = Native.CommandLineToArgvW(commandLine, out int count);
			if (argumentsPtr == 0)
			{
				throw Throw.Win32();
			}

			try
			{
				return Create.Array(count, i => Marshal.PtrToStringUni(Marshal.ReadIntPtr(argumentsPtr, i * nint.Size)) ?? "");
			}
			finally
			{
				Native.LocalFree(argumentsPtr);
			}
		}
	}
	/// <summary>
	/// Combines multiple commandline arguments into a single <see cref="string" />. Arguments are quoted when needed.
	/// </summary>
	/// <param name="arguments">A <see cref="string" />[] with commandline arguments to concatenate.</param>
	/// <returns>
	/// An equivalent <see cref="string" /> representing the combined commandline arguments.
	/// </returns>
	public static string FromArguments(params string[] arguments)
	{
		StringBuilder commandLine = new();

		foreach (string arg in arguments)
		{
			if (commandLine.Length > 0)
			{
				commandLine.Append(' ');
			}

			if (arg.Contains(' ') || arg.Contains('"'))
			{
				commandLine
					.Append('"')
					.Append(arg.Replace("\"", "\"\""))
					.Append('"');
			}
			else
			{
				commandLine.Append(arg);
			}
		}

		return commandLine.ToString();
	}
}

[SupportedOSPlatform("windows")]
file static class Native
{
	[DllImport("shell32.dll", SetLastError = true)]
	public static extern nint CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string commandLine, out int argumentCount);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern nint LocalFree(nint obj);
}