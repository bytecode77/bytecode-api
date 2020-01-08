using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="FileInfo" /> objects.
	/// </summary>
	public static class FileInfoExtension
	{
		/// <summary>
		/// Opens the file, reads the contents of the file into a <see cref="byte" />[], and then closes the file.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> to process.</param>
		/// <returns>
		/// A new <see cref="byte" />[] containing the contents of the file.
		/// </returns>
		public static byte[] ReadAllBytes(this FileInfo file)
		{
			Check.ArgumentNull(file, nameof(file));
			Check.FileNotFound(file.FullName);

			return File.ReadAllBytes(file.FullName);
		}
		/// <summary>
		/// Opens the file, reads all lines of the file into a <see cref="string" />[], and then closes the file.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> to process.</param>
		/// <returns>
		/// A new <see cref="string" />[] containing all lines of the file.
		/// </returns>
		public static string[] ReadAllLines(this FileInfo file)
		{
			Check.ArgumentNull(file, nameof(file));
			Check.FileNotFound(file.FullName);

			return File.ReadAllLines(file.FullName);
		}
		/// <summary>
		/// Opens the file, reads all lines of the file with the specified encoding into a <see cref="string" />[], and then closes the file.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> to process.</param>
		/// <param name="encoding">The encoding applied to the contents of the file.</param>
		/// <returns>
		/// A new <see cref="string" />[] containing all lines of the file.
		/// </returns>
		public static string[] ReadAllLines(this FileInfo file, Encoding encoding)
		{
			Check.ArgumentNull(file, nameof(file));
			Check.FileNotFound(file.FullName);

			return File.ReadAllLines(file.FullName, encoding);
		}
		/// <summary>
		/// Opens a text file, reads all lines of the file into a <see cref="string" />, and then closes the file.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> to process.</param>
		/// <returns>
		/// A <see cref="string" /> containing all lines of the file.
		/// </returns>
		public static string ReadAllText(this FileInfo file)
		{
			Check.ArgumentNull(file, nameof(file));
			Check.FileNotFound(file.FullName);

			return File.ReadAllText(file.FullName);
		}
		/// <summary>
		/// Opens a text file, reads all lines of the file with the specified encoding into a <see cref="string" />, and then closes the file.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> to process.</param>
		/// <param name="encoding">The encoding applied to the contents of the file.</param>
		/// <returns>
		/// A <see cref="string" /> containing all lines of the file.
		/// </returns>
		public static string ReadAllText(this FileInfo file, Encoding encoding)
		{
			Check.ArgumentNull(file, nameof(file));
			Check.FileNotFound(file.FullName);

			return File.ReadAllText(file.FullName, encoding);
		}
		/// <summary>
		/// Reads the lines of a file.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> to process.</param>
		/// <returns>
		/// All the lines of the file, or the lines that are the result of a query.
		/// </returns>
		public static IEnumerable<string> ReadLines(this FileInfo file)
		{
			Check.ArgumentNull(file, nameof(file));
			Check.FileNotFound(file.FullName);

			return File.ReadLines(file.FullName);
		}
		/// <summary>
		/// Reads the lines of a file that has a specified encoding.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> to process.</param>
		/// <param name="encoding">The encoding applied to the contents of the file.</param>
		/// <returns>
		/// All the lines of the file, or the lines that are the result of a query.
		/// </returns>
		public static IEnumerable<string> ReadLines(this FileInfo file, Encoding encoding)
		{
			Check.ArgumentNull(file, nameof(file));
			Check.FileNotFound(file.FullName);

			return File.ReadLines(file.FullName, encoding);
		}
		/// <summary>
		/// Sends this file to the recycle bin.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> to process.</param>
		public static void SendToRecycleBin(this FileInfo file)
		{
			Check.ArgumentNull(file, nameof(file));
			Check.FileNotFound(file.FullName);

			FileSystem.DeleteFile(file.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
		}
		/// <summary>
		/// Deletes the :Zone.Identifier alternate data stream for this file.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> to process.</param>
		/// <returns>
		/// <see langword="true" />, if the :Zone.Identifier alternate data stream was present and could be deleted;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool Unblock(this FileInfo file)
		{
			Check.ArgumentNull(file, nameof(file));
			Check.FileNotFound(file.FullName);

			return Native.DeleteFile(file.FullName + ":Zone.Identifier");
		}
		//TODO: Bug: Size specification parameter unclear, quality bad
		/// <summary>
		/// Extracts the icon of this file. Returns <see langword="null" />, if extraction failed.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> to process.</param>
		/// <param name="large"><see langword="true" /> to extract the large icon; otherwise, <see langword="false" />.</param>
		/// <returns>
		/// A new <see cref="Icon" /> that has been extracted from this file and <see langword="null" />, if extraction failed.
		/// </returns>
		public static Icon GetIcon(this FileInfo file, bool large)
		{
			Check.ArgumentNull(file, nameof(file));

			Native.SHFileInfo fileInfo = new Native.SHFileInfo();
			try
			{
				if (File.Exists(file.FullName))
				{
					Native.SHGetFileInfo(file.FullName, 0, ref fileInfo, Marshal.SizeOf(fileInfo), large ? 0x100u : 0x101u);
					return (Icon)Icon.FromHandle(fileInfo.Icon).Clone();
				}
				else
				{
					return null;
				}
			}
			catch
			{
				return null;
			}
			finally
			{
				if (fileInfo.Icon != IntPtr.Zero) Native.DestroyIcon(fileInfo.Icon);
			}
		}
		/// <summary>
		/// Compares the contents of two files. Returns <see langword="true" />, if both files are of equal size and equal binary content.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> to process.</param>
		/// <param name="other">The other <see cref="FileInfo" /> to compare to <paramref name="file" />.</param>
		/// <returns>
		/// <see langword="true" />, if both files are of equal size and equal binary content;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool CompareContents(this FileInfo file, FileInfo other)
		{
			Check.ArgumentNull(file, nameof(file));
			Check.FileNotFound(file.FullName);
			Check.ArgumentNull(other, nameof(other));
			Check.FileNotFound(other.FullName);

			FileInfo a = new FileInfo(file.FullName);
			FileInfo b = new FileInfo(other.FullName);

			if (a.Length == b.Length)
			{
				using FileStream streamA = a.OpenRead();
				using FileStream streamB = b.OpenRead();

				byte[] bufferA = new byte[4096];
				byte[] bufferB = new byte[4096];
				int bytesReadA;
				int bytesReadB;

				do
				{
					bytesReadA = streamA.Read(bufferA);
					bytesReadB = streamB.Read(bufferB);
					if (bytesReadA != bytesReadB || !bufferA.Compare(bufferB)) return false;
				}
				while (bytesReadA > 0 || bytesReadB > 0);

				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// Searches the file for the first occurrence of <paramref name="sequence" />. If not found, returns -1.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> to be searched.</param>
		/// <param name="sequence">The <see cref="byte" />[] to search for.</param>
		/// <returns>
		/// The index of the first occurrence of <paramref name="sequence" /> and -1, if not found.
		/// </returns>
		public static long FindSequence(this FileInfo file, byte[] sequence)
		{
			return file.FindSequence(sequence, 0);
		}
		/// <summary>
		/// Searches the file for the first occurrence of <paramref name="sequence" /> starting from <paramref name="startIndex" />. If not found, returns -1.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> to be searched.</param>
		/// <param name="sequence">The <see cref="byte" />[] to search for.</param>
		/// <param name="startIndex">The zero-based starting position to start searching from.</param>
		/// <returns>
		/// The index of the first occurrence of <paramref name="sequence" /> and -1, if not found.
		/// </returns>
		public static long FindSequence(this FileInfo file, byte[] sequence, int startIndex)
		{
			Check.ArgumentNull(file, nameof(file));
			Check.FileNotFound(file.FullName);
			Check.ArgumentNull(sequence, nameof(sequence));
			Check.ArgumentEx.ArrayElementsRequired(sequence, nameof(sequence));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(startIndex, nameof(startIndex));

			using FileStream stream = file.OpenRead();

			stream.Seek(startIndex, SeekOrigin.Begin);
			long index = stream.FindSequence(sequence);
			return index == -1 ? -1 : index + startIndex;
		}
	}
}