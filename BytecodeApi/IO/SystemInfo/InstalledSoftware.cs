using BytecodeApi.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Provides information about installed software on this computer.
	/// </summary>
	public static class InstalledSoftware
	{
		/// <summary>
		/// Gets a list of installed software on this computer.
		/// </summary>
		/// <returns>
		/// A new <see cref="InstalledSoftwareInfo" />[] with a list of installed software on this computer.
		/// </returns>
		public static InstalledSoftwareInfo[] GetEntries()
		{
			List<InstalledSoftwareInfo> entries = new List<InstalledSoftwareInfo>();

			ReadEntries(Registry.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Uninstall");
			ReadEntries(Registry.LocalMachine, @"Software\Microsoft\Windows\CurrentVersion\Uninstall");

			if (Environment.Is64BitOperatingSystem)
			{
				ReadEntries(Registry.CurrentUser, @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
				ReadEntries(Registry.LocalMachine, @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
			}

			return entries.OrderBy(entry => entry.Name).ToArray();

			void ReadEntries(RegistryKey hive, string rootKey)
			{
				using (RegistryKey key = hive.OpenSubKey(rootKey))
				{
					if (key != null)
					{
						foreach (string subKeyName in key.GetSubKeyNames())
						{
							using (RegistryKey subKey = key.OpenSubKey(subKeyName))
							{
								if (subKey.GetStringValue("DisplayName") != null &&
									subKey.GetStringValue("IsMinorUpgrade") == null &&
									subKey.GetInt32Value("SystemComponent", 0) == 0)
								{
									entries.Add(new InstalledSoftwareInfo
									{
										Name = subKey.GetStringValue("DisplayName")?.Trim().ToNullIfEmpty(),
										Publisher = subKey.GetStringValue("Publisher")?.Trim().ToNullIfEmpty(),
										Version = subKey.GetStringValue("DisplayVersion")?.Trim().ToNullIfEmpty(),
										InstallDate = subKey.GetStringValue("InstallDate").ToDateTime("yyyyMMdd"),
										InstallPath = subKey.GetStringValue("InstallLocation")?.TrimEnd('\\').ToNullIfEmpty()
									});
								}
							}
						}
					}
				}
			}
		}
	}
}