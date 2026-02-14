using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace BytecodeApi.IO;

//TODO:FEATURE: Create new or update existing stream
/// <summary>
/// Represents an alternate data stream entry of a file or directory.
/// </summary>
[DebuggerDisplay($"{nameof(AlternateDataStream)}: Name = {{Name}}, Size = {{Size}}")]
[SupportedOSPlatform("windows")]
public sealed class AlternateDataStream
{
	/// <summary>
	/// Gets the full path to the file, including the alternate data stream name.
	/// <para>Example: C:\path\to\file.txt:NameOfADS</para>
	/// </summary>
	public string FullPath { get; }
	/// <summary>
	/// Gets the name of the alternate data stream without the leading colon.
	/// </summary>
	public string Name { get; }
	/// <summary>
	/// Gets the size of the alternate data stream.
	/// </summary>
	public long Size { get; }
	/// <summary>
	/// Gets the type of the alternate data stream.
	/// </summary>
	public AlternateDataStreamType Type { get; }
	/// <summary>
	/// Gets the alternate data stream attributes.
	/// </summary>
	public AlternateDataStreamAttributes Attributes { get; }

	internal AlternateDataStream(string fullPath, string name, long size, AlternateDataStreamType type, AlternateDataStreamAttributes attributes)
	{
		FullPath = fullPath;
		Name = name;
		Size = size;
		Type = type;
		Attributes = attributes;
	}

	/// <summary>
	/// Opens the alternate data stream and reads all lines into a <see cref="string" />.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> containing all lines of the alternate data stream.
	/// </returns>
	public string ReadAllText()
	{
		return ReadAllText(Encoding.UTF8);
	}
	/// <summary>
	/// Opens the alternate data stream and reads all lines with the specified encoding into a <see cref="string" />.
	/// </summary>
	/// <param name="encoding">The encoding applied to the contents of the alternate data stream.</param>
	/// <returns>
	/// A <see cref="string" /> containing all lines of the alternate data stream.
	/// </returns>
	public string ReadAllText(Encoding encoding)
	{
		return encoding.GetString(ReadAllBytes());
	}
	/// <summary>
	/// Opens the alternate data stream and reads the contents into a <see cref="byte" />[].
	/// </summary>
	/// <returns>
	/// A new <see cref="byte" />[] containing the contents of the alternate data stream.
	/// </returns>
	public byte[] ReadAllBytes()
	{
		nint file = 0;

		try
		{
			file = Native.CreateFileW(FullPath, 0x80000000, 0, 0, 3, 0x80, 0);
			if (file == 0 || file == -1 || !Native.GetFileSizeEx(file, out long size))
			{
				throw Throw.Win32("Could not open alternate data stream.");
			}

			byte[] buffer = new byte[size];
			if (!Native.ReadFile(file, buffer, (int)size, out int bytesRead, 0) || bytesRead != size)
			{
				throw Throw.Win32("Could not open alternate data stream.");
			}

			return buffer;
		}
		finally
		{
			if (file is not 0 and not -1) Native.CloseHandle(file);
		}
	}
	/// <summary>
	/// Deletes the alternate data stream.
	/// </summary>
	public void Delete()
	{
		Native.DeleteFile(FullPath);
	}

	/// <summary>
	/// Returns the name of this <see cref="AlternateDataStream" />.
	/// </summary>
	/// <returns>
	/// The name of this <see cref="AlternateDataStream" />.
	/// </returns>
	public override string ToString()
	{
		return Name;
	}
}

[SupportedOSPlatform("windows")]
file static class Native
{
	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool CloseHandle(nint obj);
	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern nint CreateFileW([MarshalAs(UnmanagedType.LPWStr)] string path, uint desiredAccess, int shareMode, nint securityAttributes, int creationDisposition, int flagsAndAttributes, nint templateFile);
	[DllImport("kernel32.dll")]
	public static extern bool GetFileSizeEx(nint file, out long fileSize);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool ReadFile(nint file, [Out] byte[] buffer, int numberOfBytesToRead, out int numberOfBytesRead, nint overlapped);
	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool DeleteFile(string name);
}