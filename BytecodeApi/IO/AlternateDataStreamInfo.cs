using BytecodeApi.Extensions;
using BytecodeApi.Interop;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace BytecodeApi.IO;

/// <summary>
/// Provides properties and instance methods for NTFS alternate data streams.
/// </summary>
[SupportedOSPlatform("windows")]
public sealed class AlternateDataStreamInfo
{
	/// <summary>
	/// Gets the path of the file.
	/// </summary>
	public string Path { get; private init; }
	/// <summary>
	/// Gets a collection of <see cref="AlternateDataStream" /> objects associated with the specified file.
	/// </summary>
	public ReadOnlyCollection<AlternateDataStream> Streams { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="AlternateDataStreamInfo" /> class with the specified file path.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying the path of a file from which to access alternate data streams.</param>
	public AlternateDataStreamInfo(string path)
	{
		Check.ArgumentNull(path);
		Check.ArgumentEx.StringNotEmpty(path);
		Check.FileNotFound(path);

		Path = path;

		nint file = 0;
		nint context = 0;

		try
		{
			file = Native.CreateFileW(Path, 0x80000000, 1, 0, 3, 0x2000000, 0);
			if (file == 0 || file == -1 || Native.GetFileType(file) != 1)
			{
				throw Throw.Win32("Could not open alternate data stream.");
			}

			Native.Win32StreamId streamId = new();
			int streamHeaderSize = Marshal.SizeOf(streamId);

			List<AlternateDataStream> streams = [];

			for (bool done = false; !done;)
			{
				if (!Native.BackupRead(file, ref streamId, streamHeaderSize, out int bytesRead, false, false, ref context) || bytesRead != streamHeaderSize)
				{
					done = true;
				}
				else
				{
					string name = "";
					if (streamId.StreamNameSize > 0)
					{
						using HGlobal streamName = new(streamId.StreamNameSize);

						if (!Native.BackupRead(file, streamName.Handle, streamId.StreamNameSize, out bytesRead, false, false, ref context))
						{
							done = true;
						}
						else
						{
							name = (streamName.ToStringUnicode() ?? throw Throw.Win32())[1..].SubstringUntilLast(':');
						}
					}

					if (!name.IsNullOrEmpty())
					{
						streams.Add(new($"{path}:{name}", name, streamId.Size.ToInt64(), (AlternateDataStreamType)streamId.StreamId, (AlternateDataStreamAttributes)streamId.StreamAttributes));
					}

					if (streamId.Size.Low != 0 || streamId.Size.High != 0)
					{
						if (!done && !Native.BackupSeek(file, streamId.Size.Low, streamId.Size.High, out _, out _, ref context))
						{
							done = true;
						}
					}
				}
			}

			Streams = streams.AsReadOnly();
		}
		finally
		{
			Native.BackupRead(file, 0, 0, out _, true, false, ref context);
			if (file != 0 && file != -1) Native.CloseHandle(file);
		}
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
	public static extern int GetFileType(nint file);
	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool BackupRead(nint file, ref Win32StreamId buffer, int bytesToRead, out int bytesRead, [MarshalAs(UnmanagedType.Bool)] bool abort, [MarshalAs(UnmanagedType.Bool)] bool processSecurity, ref nint context);
	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool BackupRead(nint file, nint buffer, int bytesToRead, out int bytesRead, [MarshalAs(UnmanagedType.Bool)] bool abort, [MarshalAs(UnmanagedType.Bool)] bool processSecurity, ref nint context);
	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool BackupSeek(nint file, int bytesToSeekLow, int bytesToSeekHigh, out int bytesSeekedLow, out int bytesSeekedHigh, ref nint context);

	[StructLayout(LayoutKind.Sequential)]
	public struct Win32StreamId
	{
		public readonly int StreamId;
		public readonly int StreamAttributes;
		public LargeInteger Size;
		public readonly int StreamNameSize;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct LargeInteger
	{
		public readonly int Low;
		public readonly int High;

		public long ToInt64()
		{
			return High << 32 | Low;
		}
	}
}