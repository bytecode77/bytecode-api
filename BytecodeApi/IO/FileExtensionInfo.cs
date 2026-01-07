using BytecodeApi.Extensions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace BytecodeApi.IO;

/// <summary>
/// Class that retrieves information about a file extension using the AssocQueryString API.
/// </summary>
[SupportedOSPlatform("windows")]
public class FileExtensionInfo
{
	/// <summary>
	/// Gets the file extension as supplied in the constructor of <see cref="FileExtensionInfo" /> without the leading dot.
	/// </summary>
	public string? Extension { get; }
	/// <summary>
	/// Gets the ASSOCSTR_COMMAND property of the specified extension.
	/// </summary>
	public string? Command { get; }
	/// <summary>
	/// Gets the ASSOCSTR_EXECUTABLE property of the specified extension.
	/// </summary>
	public string? Executable { get; }
	/// <summary>
	/// Gets the ASSOCSTR_FRIENDLYDOCNAME property of the specified extension.
	/// </summary>
	public string? FriendlyDocName { get; }
	/// <summary>
	/// Gets the ASSOCSTR_FRIENDLYAPPNAME property of the specified extension.
	/// </summary>
	public string? FriendlyAppName { get; }
	/// <summary>
	/// Gets the ASSOCSTR_NOOPEN property of the specified extension.
	/// </summary>
	public string? NoOpen { get; }
	/// <summary>
	/// Gets the ASSOCSTR_SHELLNEWVALUE property of the specified extension.
	/// </summary>
	public string? ShellNewValue { get; }
	/// <summary>
	/// Gets the ASSOCSTR_DDECOMMAND property of the specified extension.
	/// </summary>
	public string? DDECommand { get; }
	/// <summary>
	/// Gets the ASSOCSTR_DDEIFEXEC property of the specified extension.
	/// </summary>
	public string? DDEIfExec { get; }
	/// <summary>
	/// Gets the ASSOCSTR_DDEAPPLICATION property of the specified extension.
	/// </summary>
	public string? DDEApplication { get; }
	/// <summary>
	/// Gets the ASSOCSTR_DDETOPIC property of the specified extension.
	/// </summary>
	public string? DDETopic { get; }
	/// <summary>
	/// Gets the ASSOCSTR_INFOTIP property of the specified extension.
	/// </summary>
	public string? InfoTip { get; }
	/// <summary>
	/// Gets the ASSOCSTR_QUICKTIP property of the specified extension.
	/// </summary>
	public string? QuickTip { get; }
	/// <summary>
	/// Gets the ASSOCSTR_TILEINFO property of the specified extension.
	/// </summary>
	public string? TileInfo { get; }
	/// <summary>
	/// Gets the ASSOCSTR_CONTENTTYPE property of the specified extension.
	/// </summary>
	public string? ContentType { get; }
	/// <summary>
	/// Gets the ASSOCSTR_DEFAULTICON property of the specified extension.
	/// </summary>
	public string? DefaultIcon { get; }
	/// <summary>
	/// Gets the ASSOCSTR_SHELLEXTENSION property of the specified extension.
	/// </summary>
	public string? ShellExtension { get; }
	/// <summary>
	/// Gets the ASSOCSTR_DROPTARGET property of the specified extension.
	/// </summary>
	public string? DropTarget { get; }
	/// <summary>
	/// Gets the ASSOCSTR_DELEGATEEXECUTE property of the specified extension.
	/// </summary>
	public string? DelegateExecute { get; }
	/// <summary>
	/// Gets the ASSOCSTR_SUPPORTED_URI_PROTOCOLS property of the specified extension.
	/// </summary>
	public string? SupportedUriProtocols { get; }
	/// <summary>
	/// Gets the ASSOCSTR_PROGID property of the specified extension.
	/// </summary>
	public string? ProgId { get; }
	/// <summary>
	/// Gets the ASSOCSTR_APPID property of the specified extension.
	/// </summary>
	public string? AppId { get; }
	/// <summary>
	/// Gets the ASSOCSTR_APPPUBLISHER property of the specified extension.
	/// </summary>
	public string? AppPublisher { get; }
	/// <summary>
	/// Gets the ASSOCSTR_APPICONREFERENCE property of the specified extension.
	/// </summary>
	public string? AppIconReference { get; }
	/// <summary>
	/// Gets the ASSOCSTR_MAX property of the specified extension.
	/// </summary>
	public string? Max { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="FileExtensionInfo" /> class and retrieves information about the specified file extension.
	/// </summary>
	/// <param name="extension">A file extension, with or without the leading dot.</param>
	public FileExtensionInfo(string extension)
	{
		extension ??= "";
		string assocExtension = extension.EnsureStartsWith('.');

		Extension = extension.TrimStart('.');
		Command = GetValue(Native.AssocStr.Command);
		Executable = GetValue(Native.AssocStr.Executable);
		FriendlyDocName = GetValue(Native.AssocStr.FriendlyDocName);
		FriendlyAppName = GetValue(Native.AssocStr.FriendlyAppName);
		NoOpen = GetValue(Native.AssocStr.NoOpen);
		ShellNewValue = GetValue(Native.AssocStr.ShellNewValue);
		DDECommand = GetValue(Native.AssocStr.DDECommand);
		DDEIfExec = GetValue(Native.AssocStr.DDEIfExec);
		DDEApplication = GetValue(Native.AssocStr.DDEApplication);
		DDETopic = GetValue(Native.AssocStr.DDETopic);
		InfoTip = GetValue(Native.AssocStr.InfoTip);
		QuickTip = GetValue(Native.AssocStr.QuickTip);
		TileInfo = GetValue(Native.AssocStr.TileInfo);
		ContentType = GetValue(Native.AssocStr.ContentType);
		DefaultIcon = GetValue(Native.AssocStr.DefaultIcon);
		ShellExtension = GetValue(Native.AssocStr.ShellExtension);
		DropTarget = GetValue(Native.AssocStr.DropTarget);
		DelegateExecute = GetValue(Native.AssocStr.DelegateExecute);
		SupportedUriProtocols = GetValue(Native.AssocStr.SupportedUriProtocols);
		ProgId = GetValue(Native.AssocStr.ProgId);
		AppId = GetValue(Native.AssocStr.AppId);
		AppPublisher = GetValue(Native.AssocStr.AppPublisher);
		AppIconReference = GetValue(Native.AssocStr.AppIconReference);
		Max = GetValue(Native.AssocStr.Max);

		string? GetValue(Native.AssocStr assocStr)
		{
			uint resultLength = 0;
			if (Native.AssocQueryString(0, assocStr, assocExtension, null, null, ref resultLength) == 1 && resultLength != 0)
			{
				StringBuilder result = new((int)resultLength);
				if (Native.AssocQueryString(0x40, assocStr, assocExtension, null, result, ref resultLength) == 0)
				{
					return result.ToString();
				}
			}

			return null;
		}
	}

	/// <summary>
	/// Starts the default program associated with this file extension (<see cref="Executable" />) and returns the created <see cref="Process" /> object and <see langword="null" />, if process creation failed.
	/// </summary>
	/// <returns>
	/// The <see cref="Process" /> this method creates and <see langword="null" />, if process creation failed.
	/// </returns>
	public Process? StartProgram()
	{
		return StartProgram(null, false);
	}
	/// <summary>
	/// Starts the default program associated with this file extension (<see cref="Executable" />) with the specified commandline and returns the created <see cref="Process" /> object and <see langword="null" />, if process creation failed. Typically, the commandline is used for the file to be opened by the application.
	/// </summary>
	/// <param name="commandLine">A <see cref="string" /> specifying the commandline to be passed to the executable.</param>
	/// <returns>
	/// The <see cref="Process" /> this method creates and <see langword="null" />, if process creation failed.
	/// </returns>
	public Process? StartProgram(string? commandLine)
	{
		return StartProgram(commandLine, false);
	}
	/// <summary>
	/// Starts the default program associated with this file extension (<see cref="Executable" />) and returns the created <see cref="Process" /> object and <see langword="null" />, if process creation failed.
	/// </summary>
	/// <param name="runas"><see langword="true" /> to execute this file with the "runas" verb.</param>
	/// <returns>
	/// The <see cref="Process" /> this method creates and <see langword="null" />, if process creation failed.
	/// </returns>
	public Process? StartProgram(bool runas)
	{
		return StartProgram(null, runas);
	}
	/// <summary>
	/// Starts the default program associated with this file extension (<see cref="Executable" />) with the specified commandline and returns the created <see cref="Process" /> object and <see langword="null" />, if process creation failed. Typically, the commandline is used for the file to be opened by the application.
	/// </summary>
	/// <param name="commandLine">A <see cref="string" /> specifying the commandline to be passed to the executable.</param>
	/// <param name="runas"><see langword="true" /> to execute this file with the "runas" verb.</param>
	/// <returns>
	/// The <see cref="Process" /> this method creates and <see langword="null" />, if process creation failed.
	/// </returns>
	public Process? StartProgram(string? commandLine, bool runas)
	{
		try
		{
			return Process.Start(new ProcessStartInfo(Executable!, commandLine ?? "")
			{
				UseShellExecute = true,
				Verb = runas ? "runas" : ""
			});
		}
		catch
		{
			return null;
		}
	}
	//TODO:FEATURE: GetIcon
}

[SupportedOSPlatform("windows")]
file static class Native
{
	[DllImport("shlwapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
	public static extern uint AssocQueryString(int flags, AssocStr str, string assoc, string? extra, [Out] StringBuilder? result, [In][Out] ref uint resultLength);

	public enum AssocStr
	{
		Command = 1,
		Executable = 2,
		FriendlyDocName = 3,
		FriendlyAppName = 4,
		NoOpen = 5,
		ShellNewValue = 6,
		DDECommand = 7,
		DDEIfExec = 8,
		DDEApplication = 9,
		DDETopic = 10,
		InfoTip = 11,
		QuickTip = 12,
		TileInfo = 13,
		ContentType = 14,
		DefaultIcon = 15,
		ShellExtension = 16,
		DropTarget = 17,
		DelegateExecute = 18,
		SupportedUriProtocols = 19,
		ProgId = 20,
		AppId = 21,
		AppPublisher = 22,
		AppIconReference = 23,
		Max = 24,
	}
}