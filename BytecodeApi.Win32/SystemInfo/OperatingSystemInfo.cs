using BytecodeApi.Extensions;
using BytecodeApi.IO;
using BytecodeApi.Wmi;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace BytecodeApi.Win32.SystemInfo;

/// <summary>
/// Provides information about the installed operating system.
/// </summary>
public static class OperatingSystemInfo
{
	private static DateTime? _InstallDate;
	/// <summary>
	/// Gets the name of the operating system.
	/// <para>Examples: "Windows 7 Professional", "Windows 10 Pro"</para>
	/// </summary>
	public static string Name
	{
		get
		{
			if (field == null)
			{
				using RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
				using RegistryKey key = baseKey.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion") ?? throw Throw.Win32();
				field = key.GetStringValue("ProductName") ?? throw Throw.Win32();
			}

			return field;
		}
	}
	/// <summary>
	/// Gets the installation date of the operating system, or <see langword="null" />, if it could not be determined.
	/// </summary>
	public static DateTime InstallDate
	{
		get
		{
			if (_InstallDate == null)
			{
				using RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
				using RegistryKey key = baseKey.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion") ?? throw Throw.Win32();
				int installDate = key.GetInt32Value("InstallDate", 0);
				_InstallDate = installDate > 0 ? DateTime.FromUnixTimeStamp(installDate, DateTimeKind.Utc) : throw Throw.Win32();
			}

			return _InstallDate.Value;
		}
	}
	/// <summary>
	/// Gets an array containing a list of installed antivirus software.
	/// </summary>
	public static string[] InstalledAntiVirusSoftware => field ??= WmiContext.Root
		.GetNamespace("SecurityCenter2")
		.GetClass("AntiVirusProduct")
		.Select("displayName")
		.ToArray()
		.Select(obj => obj.Properties["displayName"].GetValue<string>()?.Trim().ToNullIfEmpty())
		.ExceptNull()
		.ToArray();
	/// <summary>
	/// Gets the default browser of the current user, or <see langword="null" />, if it could not be determined.
	/// </summary>
	public static KnownBrowser? DefaultBrowser
	{
		get
		{
			using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice"))
			{
				if (key?.GetStringValue("Progid") is string program)
				{
					if (program.Equals("IE.HTTP", StringComparison.OrdinalIgnoreCase))
					{
						return KnownBrowser.InternetExplorer;
					}
					else if (program == "AppXq0fevzme2pys62n3e0fbqa7peapykr8v")
					{
						return KnownBrowser.Edge;
					}
					else if (program.Equals("ChromeHTML", StringComparison.OrdinalIgnoreCase))
					{
						return KnownBrowser.Chrome;
					}
					else if (program.StartsWith("FirefoxURL", StringComparison.OrdinalIgnoreCase))
					{
						return KnownBrowser.Firefox;
					}
					else if (program.StartsWithAny(["Opera", "OperaStable"], StringComparison.OrdinalIgnoreCase))
					{
						return KnownBrowser.Opera;
					}
					else if (program.StartsWithAny(["Safari", "SafariHTML"], StringComparison.OrdinalIgnoreCase))
					{
						return KnownBrowser.Safari;
					}
				}
			}

			using (RegistryKey? key = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command"))
			{
				string? browser = key
					?.GetStringValue(null)
					?.Replace("\"", null)
					.SubstringUntil(".exe", false, StringComparison.OrdinalIgnoreCase)
					.SubstringFromLast('\\')
					.Trim()
					.ToLower();

				return browser switch
				{
					null => null,
					"iexplore" => KnownBrowser.InternetExplorer,
					"chrome" => KnownBrowser.Chrome,
					"firefox" => KnownBrowser.Firefox,
					"launcher" => KnownBrowser.Opera,
					_ => null
				};
			}
		}
	}
	/// <summary>
	/// Gets a list of installed .NET versions, ranging from .NET Framework 2.0 through .NET 7.
	/// </summary>
	public static Version[] FrameworkVersions
	{
		get
		{
			if (field == null)
			{
				List<Version> versions = [];
				GetFramework4(versions);
				GetFramework5(versions);

				field = versions.Distinct().ToArray();
			}

			return field;

			static void GetFramework4(List<Version> versions)
			{
				using RegistryKey frameworkKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP") ?? throw Throw.Win32();
				foreach (string versionKeyName in new[] { "v2.0.50727", "v3.0", "v3.5", "v4" })
				{
					using RegistryKey? versionKey = frameworkKey.OpenSubKey(versionKeyName);
					if (versionKey != null)
					{
						if (versionKey.GetStringValue("Version") is string version)
						{
							versions.Add(new(version));
						}
						else
						{
							foreach (string subKeyName in versionKey.GetSubKeyNames())
							{
								using RegistryKey subKey = versionKey.OpenSubKey(subKeyName) ?? throw Throw.Win32();
								if (subKey.GetStringValue("Version") is string version2)
								{
									versions.Add(new(version2));
								}
							}
						}
					}
				}
			}
			static void GetFramework5(List<Version> versions)
			{
				DirectoryInfo frameworkDirectory = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"dotnet\shared\Microsoft.NETCore.App"));
				if (frameworkDirectory.Exists)
				{
					foreach (DirectoryInfo subDirectory in frameworkDirectory.GetDirectories())
					{
						if (Version.TryParse(subDirectory.Name, out Version? version))
						{
							versions.Add(version);
						}
					}
				}
			}
		}
	}
	/// <summary>
	/// Gets the installed Java version, or <see langword="null" />, if Java is not installed.
	/// </summary>
	public static string? JavaVersion
	{
		get
		{
			if (field == null)
			{
				CliResult? result;

				try
				{
					result = CliCommand
						.FileName("java")
						.Arguments("-version")
						.Hidden()
						.Execute();
				}
				catch
				{
					// File "java" not found.
					result = null;
				}

				if (result == null)
				{
					field = "";
				}
				else
				{
					Match match = Regex.Match(result.Output, "^\\s*java version \"(.+)\"");
					field = match.Success ? match.Groups[1].Value : "";
				}
			}

			return field.ToNullIfEmpty();
		}
	}
}