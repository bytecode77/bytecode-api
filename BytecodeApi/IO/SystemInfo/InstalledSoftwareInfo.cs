using BytecodeApi.Text;
using System;
using System.Diagnostics;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Represents a program that is installed on this computer.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class InstalledSoftwareInfo
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay => CSharp.DebuggerDisplay<InstalledSoftwareInfo>("Name = {0}, Publisher = {1}, Version = {2}", new QuotedString(Name), new QuotedString(Publisher), new QuotedString(Version));
		/// <summary>
		/// Gets the name of the software, or <see langword="null" />, if it cannot be retrieved.
		/// </summary>
		public string Name { get; internal set; }
		/// <summary>
		/// Gets the publisher of the software, or <see langword="null" />, if it cannot be retrieved.
		/// </summary>
		public string Publisher { get; internal set; }
		/// <summary>
		/// Gets the version of the software, or <see langword="null" />, if it cannot be retrieved.
		/// </summary>
		public string Version { get; internal set; }
		/// <summary>
		/// Gets the install date of the software, or <see langword="null" />, if it cannot be retrieved.
		/// </summary>
		public DateTime? InstallDate { get; internal set; }
		/// <summary>
		/// Gets the install path of the software, or <see langword="null" />, if it cannot be retrieved.
		/// </summary>
		public string InstallPath { get; internal set; }

		internal InstalledSoftwareInfo()
		{
		}

		/// <summary>
		/// Returns the name of this <see cref="InstalledSoftwareInfo" />.
		/// </summary>
		/// <returns>
		/// The name of this <see cref="InstalledSoftwareInfo" />.
		/// </returns>
		public override string ToString()
		{
			return Name;
		}
	}
}