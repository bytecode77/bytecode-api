using System.Diagnostics;
using System.Linq;

namespace BytecodeApi.IO
{
	/// <summary>
	/// Provides <see langword="static" /> methods that extend the <see cref="Process" /> class.
	/// </summary>
	public static class ProcessEx
	{
		/// <summary>
		/// Creates a new <see cref="Process" /> component for each process resource with the SessionId of the current <see cref="Process" /> on the local computer.
		/// </summary>
		/// <returns>
		/// An <see cref="Process" />[] that represents all the process resources with the SessionId of the current <see cref="Process" /> running on the local computer.
		/// </returns>
		public static Process[] GetSessionProcesses()
		{
			return Process
				.GetProcesses()
				.Where(process => process.SessionId == ApplicationBase.Process.SessionId)
				.ToArray();
		}
		/// <summary>
		/// Creates an array of new <see cref="Process" /> components and associates them with all the process resources on the local computer that share the specified process name and the SessionId of the current <see cref="Process" />.
		/// </summary>
		/// <param name="processName">A <see cref="string" /> specifying the friendly name of the process.</param>
		/// <returns>
		/// A <see cref="Process" />[] that represents the process resources running the specified application or file with the SessionId of the current <see cref="Process" />.
		/// </returns>
		public static Process[] GetSessionProcessesByName(string processName)
		{
			return Process
				.GetProcessesByName(processName)
				.Where(process => process.SessionId == ApplicationBase.Process.SessionId)
				.ToArray();
		}
	}
}