using BytecodeApi.Extensions;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;

namespace BytecodeApi.IO.FileSystem
{
	//FEATURE: Exists, Delete, Create/Update
	/// <summary>
	/// Provides properties and instance methods for NTFS alternate data streams.
	/// </summary>
	public sealed class AlternateDataStreamInfo
	{
		private ReadOnlyCollection<AlternateDataStream> _Streams;
		/// <summary>
		/// Gets the path of the file.
		/// </summary>
		public string Path { get; private set; }
		/// <summary>
		/// Gets a collection of <see cref="AlternateDataStream" /> objects associated with the specified file.
		/// </summary>
		public ReadOnlyCollection<AlternateDataStream> Streams
		{
			get
			{
				Check.FileNotFound(Path);

				if (_Streams == null)
				{
					using (SafeFileHandle file = Native.CreateFile(Path, 0x80000000, FileShare.Read, IntPtr.Zero, FileMode.Open, 0x2000000, IntPtr.Zero))
					{
						if (!file.IsInvalid && Native.GetFileType(file) != 1) throw Throw.Win32(2);

						Native.Win32StreamId streamId = new Native.Win32StreamId();
						int streamHeaderSize = Marshal.SizeOf(streamId);
						IntPtr context = IntPtr.Zero;
						Native.SafeHGlobalHandle streamName = new Native.SafeHGlobalHandle();

						try
						{
							List<AlternateDataStream> streams = new List<AlternateDataStream>();

							for (bool done = false; !done;)
							{
								if (!Native.BackupRead(file, ref streamId, streamHeaderSize, out int bytesRead, false, false, ref context) || bytesRead != streamHeaderSize)
								{
									done = true;
								}
								else
								{
									string name = null;
									if (streamId.StreamNameSize > 0)
									{
										streamName = Native.SafeHGlobalHandle.Allocate(streamId.StreamNameSize);

										if (!Native.BackupRead(file, streamName, streamId.StreamNameSize, out bytesRead, false, false, ref context))
										{
											done = true;
										}
										else
										{
											name = Marshal.PtrToStringUni(streamName.DangerousGetHandle(), bytesRead / 2).Substring(1).SubstringUntil(":", true);
										}
									}

									if (!name.IsNullOrEmpty())
									{
										streams.Add(new AlternateDataStream(name, streamId.Size.ToInt64(), (AlternateDataStreamType)streamId.StreamId, (AlternateDataStreamAttributes)streamId.StreamAttributes));
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

							_Streams = streams.AsReadOnly();
						}
						finally
						{
							Native.BackupRead(file, streamName, 0, out _, true, false, ref context);
						}
					}
				}

				return _Streams;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AlternateDataStreamInfo" /> class with the specified file path.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path of a file from which to access alternate data streams.</param>
		public AlternateDataStreamInfo(string path)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.ArgumentEx.StringNotEmpty(path, nameof(path));

			Path = path;
		}
	}
}