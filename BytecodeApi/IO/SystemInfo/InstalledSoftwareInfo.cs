using BytecodeApi.Extensions;
using Microsoft.Win32;
using System;
using System.Linq;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Provides information about installed software on this computer.
	/// </summary>
	public sealed class InstalledSoftwareInfo
	{
		/// <summary>
		/// Gets the name of the software, or <see langword="null" />, if it cannot be retrieved.
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Gets the publisher of the software, or <see langword="null" />, if it cannot be retrieved.
		/// </summary>
		public string Publisher { get; private set; }
		/// <summary>
		/// Gets the version of the software, or <see langword="null" />, if it cannot be retrieved.
		/// </summary>
		public string Version { get; private set; }
		/// <summary>
		/// Gets the install date of the software, or <see langword="null" />, if it cannot be retrieved.
		/// </summary>
		public DateTime? InstallDate { get; private set; }
		/// <summary>
		/// Gets the install path of the software, or <see langword="null" />, if it cannot be retrieved.
		/// </summary>
		public string InstallPath { get; private set; }

		private InstalledSoftwareInfo()
		{
		}
		/// <summary>
		/// Gets a list of installed software on this computer.
		/// </summary>
		/// <returns>
		/// A new <see cref="InstalledSoftwareInfo" />[] with a list of installed software on this computer.
		/// </returns>
		public static InstalledSoftwareInfo[] GetInstalledSoftware()
		{
			return Create
				.Enumerable(4, i => ((i & 1) == 0 ? Registry.LocalMachine : Registry.CurrentUser).OpenSubKey(@"Software\" + ((i & 2) == 0 ? null : @"Wow6432Node\") + @"Microsoft\Windows\CurrentVersion\Uninstall"))
				.Where(key => key != null)
				.ToList()
				.SelectMany(key =>
				{
					using (key)
					{
						return key
							.GetSubKeyNames()
							.Select(subKey => key.OpenSubKey(subKey))
							.Where(subKey => subKey.GetStringValue("DisplayName") != null)
							.Where(subKey => subKey.GetStringValue("IsMinorUpgrade") == null)
							.Where(subKey => subKey.GetInt32Value("SystemComponent", 0) == 0)
							.ToList()
							.Select(subKey =>
							{
								using (subKey)
								{
									return new InstalledSoftwareInfo
									{
										Name = (subKey.GetStringValue("DisplayName"))?.Trim().ToNullIfEmpty(),
										Publisher = (subKey.GetStringValue("Publisher"))?.Trim().ToNullIfEmpty(),
										Version = (subKey.GetStringValue("DisplayVersion"))?.Trim().ToNullIfEmpty(),
										InstallDate = subKey.GetStringValue("InstallDate").ToDateTime("yyyyMMdd"),
										InstallPath = (subKey.GetStringValue("InstallLocation"))?.TrimEnd('\\').ToNullIfEmpty()
									};
								}
							});
					}
				})
				.ToArray();
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + Name + ", Version: " + Version + "]";
		}
	}
}