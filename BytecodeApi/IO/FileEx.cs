using BytecodeApi.Extensions;
using System.Runtime.Versioning;

namespace BytecodeApi.IO;

/// <summary>
/// Provides <see langword="static" /> methods that extend the <see cref="File" /> class.
/// </summary>
public static class FileEx
{
	/// <summary>
	/// Sends the specified file to the recycle bin.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a file.</param>
	[SupportedOSPlatform("windows")]
	public static void SendToRecycleBin(string path)
	{
		Check.ArgumentNull(path);
		Check.FileNotFound(path);

		new FileInfo(path).SendToRecycleBin();
	}
	/// <summary>
	/// Deletes the :Zone.Identifier alternate data stream for the specified file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a file.</param>
	/// <returns>
	/// <see langword="true" />, if the :Zone.Identifier alternate data stream was present and could be deleted;
	/// otherwise, <see langword="false" />.
	/// </returns>
	[SupportedOSPlatform("windows")]
	public static bool Unblock(string path)
	{
		Check.ArgumentNull(path);
		Check.FileNotFound(path);

		return new FileInfo(path).Unblock();
	}
	//TODO:FEATURE: Icon GetIcon(string path)
	/// <summary>
	/// Shows the properties dialog for the specified file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a file.</param>
	[SupportedOSPlatform("windows")]
	public static void ShowPropertiesDialog(string path)
	{
		Check.ArgumentNull(path);
		Check.FileNotFound(path);

		new FileInfo(path).ShowPropertiesDialog();
	}
	/// <summary>
	/// Compares the contents of two files. Returns <see langword="true" />, if both files are of equal size and equal binary content.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a file.</param>
	/// <param name="otherPath">A <see cref="string" /> representing the path to the other file to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both files are of equal size and equal binary content;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool CompareContents(string path, string otherPath)
	{
		Check.ArgumentNull(path);
		Check.FileNotFound(path);
		Check.ArgumentNull(otherPath);
		Check.FileNotFound(otherPath);

		return new FileInfo(path).CompareContents(new(otherPath));
	}
	/// <summary>
	/// Searches the file for the first occurrence of <paramref name="sequence" />. If not found, returns -1.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a file.</param>
	/// <param name="sequence">The <see cref="byte" />[] to search for.</param>
	/// <returns>
	/// The index of the first occurrence of <paramref name="sequence" /> and -1, if not found.
	/// </returns>
	public static long FindSequence(string path, byte[] sequence)
	{
		return FindSequence(path, sequence, 0);
	}
	/// <summary>
	/// Searches the file for the first occurrence of <paramref name="sequence" /> starting from <paramref name="startIndex" />. If not found, returns -1.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a file.</param>
	/// <param name="sequence">The <see cref="byte" />[] to search for.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <returns>
	/// The index of the first occurrence of <paramref name="sequence" /> and -1, if not found.
	/// </returns>
	public static long FindSequence(string path, byte[] sequence, int startIndex)
	{
		Check.ArgumentNull(path);
		Check.FileNotFound(path);
		Check.ArgumentNull(sequence);
		Check.ArgumentEx.ArrayElementsRequired(sequence);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(startIndex);

		return new FileInfo(path).FindSequence(sequence, startIndex);
	}
}