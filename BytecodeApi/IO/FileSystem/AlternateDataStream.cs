using BytecodeApi.Text;
using System;
using System.Diagnostics;
using System.Text;

namespace BytecodeApi.IO.FileSystem
{
	//FEATURE: Create new or update existing stream
	/// <summary>
	/// Represents an alternate data stream entry of a file or directory.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class AlternateDataStream
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay => CSharp.DebuggerDisplay<AlternateDataStream>("Name = {0}, Size = {1}", new QuotedString(Name), Size);
		/// <summary>
		/// Gets the full path to the file, including the alternate data stream name.
		/// <para>Example: C:\path\to\file.txt:NameOfADS</para>
		/// </summary>
		public string FullPath { get; private set; }
		/// <summary>
		/// Gets the name of the alternate data stream without the leading colon.
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Gets the size of the alternate data stream.
		/// </summary>
		public long Size { get; private set; }
		/// <summary>
		/// Gets the type of the alternate data stream.
		/// </summary>
		public AlternateDataStreamType Type { get; private set; }
		/// <summary>
		/// Gets the alternate data stream attributes.
		/// </summary>
		public AlternateDataStreamAttributes Attributes { get; private set; }

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
			IntPtr file = IntPtr.Zero;

			try
			{
				file = Native.CreateFileW(FullPath, 0x80000000, 0, IntPtr.Zero, 3, 0x80, IntPtr.Zero);
				if (file == IntPtr.Zero || file == (IntPtr)(-1)) throw Throw.Win32("Could not open alternate data stream.");

				if (!Native.GetFileSizeEx(file, out long size)) throw Throw.Win32("Could not read alternate data stream.");

				byte[] buffer = new byte[size];
				if (!Native.ReadFile(file, buffer, (int)size, out int bytesRead, IntPtr.Zero) || bytesRead != size) throw Throw.Win32("Could not open alternate data stream.");

				return buffer;
			}
			finally
			{
				if (file != IntPtr.Zero && file != (IntPtr)(-1)) Native.CloseHandle(file);
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
}