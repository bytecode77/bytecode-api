using System.Diagnostics;

namespace BytecodeApi.IO;

/// <summary>
/// Helper class for interoperability with the current user's temporary folder.
/// </summary>
public static class TempDirectory
{
	/// <summary>
	/// Creates a subdirectory in the current user's temporary folder named by a <see cref="Guid" /> with the <see cref="GuidFormat.Braces" /> format. The subdirectory is created with <see cref="FileAttributes.NotContentIndexed" />.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> value with the full path to the created directory.
	/// </returns>
	public static string CreateDirectory()
	{
		string path = Path.Combine(Path.GetTempPath(), Create.Guid(GuidFormat.Braces));
		Directory.CreateDirectory(path).Attributes |= FileAttributes.NotContentIndexed;
		return path;
	}
	/// <summary>
	/// Creates a file in the current user's temporary folder with the specified filename and writes the value of <paramref name="content" /> to it. The file is created in a subdirectory named by a <see cref="Guid" /> with the <see cref="GuidFormat.Braces" /> format. The subdirectory is created with <see cref="FileAttributes.NotContentIndexed" /> and the file's attributes are set to <see cref="FileAttributes.NotContentIndexed" /> | <see cref="FileAttributes.Temporary" />.
	/// </summary>
	/// <param name="fileName">A <see cref="string" /> value specifying the filename.</param>
	/// <param name="content">A <see cref="byte" />[] specifying the content that is written to the file.</param>
	/// <returns>
	/// A <see cref="string" /> value with the full path to the created file.
	/// </returns>
	public static string CreateFile(string fileName, byte[] content)
	{
		Check.ArgumentNull(fileName);
		Check.ArgumentNull(content);

		string path = Path.Combine(CreateDirectory(), fileName);
		File.WriteAllBytes(path, content);
		new FileInfo(path).Attributes |= FileAttributes.NotContentIndexed | FileAttributes.Temporary | FileAttributes.ReadOnly;
		return path;
	}
	/// <summary>
	/// Creates a file in the current user's temporary folder with the specified filename and writes the value of <paramref name="content" /> to it. The file is created in a subdirectory named by a <see cref="Guid" /> with the <see cref="GuidFormat.Braces" /> format. The subdirectory is created with <see cref="FileAttributes.NotContentIndexed" /> and the file's attributes are set to <see cref="FileAttributes.NotContentIndexed" /> | <see cref="FileAttributes.Temporary" />. The file is then executed and the <see cref="Process" /> is returned and <see langword="null" />, if the file could not be executed.
	/// </summary>
	/// <param name="fileName">A <see cref="string" /> value specifying the filename.</param>
	/// <param name="content">A <see cref="byte" />[] specifying the content that is written to the file.</param>
	/// <returns>
	/// The <see cref="Process" /> of the executed file and
	/// <see langword="null" />, if <see cref="Process" /> creation failed.
	/// </returns>
	public static Process? ExecuteFile(string fileName, byte[] content)
	{
		return ExecuteFile(fileName, content, false);
	}
	/// <summary>
	/// Creates a file in the current user's temporary folder with the specified filename and writes the value of <paramref name="content" /> to it. The file is created in a subdirectory named by a <see cref="Guid" /> with the <see cref="GuidFormat.Braces" /> format. The subdirectory is created with <see cref="FileAttributes.NotContentIndexed" /> and the file's attributes are set to <see cref="FileAttributes.NotContentIndexed" /> | <see cref="FileAttributes.Temporary" />. The file is then executed and the <see cref="Process" /> is returned and <see langword="null" />, if the file could not be executed.
	/// </summary>
	/// <param name="fileName">A <see cref="string" /> value specifying the filename.</param>
	/// <param name="content">A <see cref="byte" />[] specifying the content that is written to the file.</param>
	/// <param name="runas"><see langword="true" /> to execute this file with the "runas" verb.</param>
	/// <returns>
	/// The <see cref="Process" /> of the executed file and
	/// <see langword="null" />, if <see cref="Process" /> creation failed.
	/// </returns>
	public static Process? ExecuteFile(string fileName, byte[] content, bool runas)
	{
		Check.ArgumentNull(fileName);
		Check.ArgumentNull(content);

		try
		{
			return Process.Start(new ProcessStartInfo(CreateFile(fileName, content))
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
}