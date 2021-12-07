using System;
using System.Runtime.InteropServices;

namespace BytecodeApi.IO.Cli
{
	/// <summary>
	/// Helper class for commandline parsion.
	/// </summary>
	public static class CommandLineParser
	{
		/// <summary>
		/// Parses commandline arguments from a <see cref="string" /> and returns the equivalent <see cref="string" />[] with split commandline arguments.
		/// </summary>
		/// <param name="commandLine">A <see cref="string" /> specifying a commandline.</param>
		/// <returns>
		/// The equivalent <see cref="string" />[] with split commandline arguments from the given commandline.
		/// </returns>
		public static string[] GetArguments(string commandLine)
		{
			Check.ArgumentNull(commandLine, nameof(commandLine));

			IntPtr argumentsPtr = Native.CommandLineToArgvW(commandLine, out int count);
			if (argumentsPtr == IntPtr.Zero) throw Throw.Win32();

			try
			{
				return Create.Array(count, i => Marshal.PtrToStringUni(Marshal.ReadIntPtr(argumentsPtr, i * IntPtr.Size)));
			}
			finally
			{
				Native.LocalFree(argumentsPtr);
			}
		}
	}
}