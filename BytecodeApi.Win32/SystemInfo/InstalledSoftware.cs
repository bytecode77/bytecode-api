using BytecodeApi.Extensions;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace BytecodeApi.Win32.SystemInfo;

/// <summary>
/// Provides a snapshot of all installed software products on this computer.
/// </summary>
public sealed class InstalledSoftware
{
	/// <summary>
	/// Gets a list of installed software products.
	/// </summary>
	public ReadOnlyCollection<InstalledSoftwareInfo> Software { get; private init; }

	private InstalledSoftware(IEnumerable<InstalledSoftwareInfo> software)
	{
		Software = software.ToReadOnlyCollection();
	}
	/// <summary>
	/// Creates a new <see cref="InstalledSoftware" /> instance and loads information about installed software.
	/// </summary>
	/// <returns>
	/// The <see cref="InstalledSoftware" /> this method creates.
	/// </returns>
	public static InstalledSoftware Load()
	{
		List<InstalledSoftwareInfo> entries = new();

		ReadEntries(Registry.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Uninstall");
		ReadEntries(Registry.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");

		if (Environment.Is64BitOperatingSystem)
		{
			ReadEntries(Registry.CurrentUser, @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
			ReadEntries(Registry.LocalMachine, @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
		}

		return new(entries.OrderBy(entry => entry.Name));

		void ReadEntries(RegistryKey hive, string rootKey)
		{
			using RegistryKey? key = hive.OpenSubKey(rootKey);
			if (key != null)
			{
				foreach (string subKeyName in key.GetSubKeyNames())
				{
					using RegistryKey? subKey = key.OpenSubKey(subKeyName);
					if (subKey?.GetStringValue("DisplayName") != null &&
						subKey?.GetStringValue("IsMinorUpgrade") == null &&
						subKey?.GetInt32Value("SystemComponent", 0) == 0)
					{
						entries.Add(new()
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