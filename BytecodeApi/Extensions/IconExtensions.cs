using System.Drawing;
using System.Runtime.Versioning;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Icon" /> objects.
/// </summary>
[SupportedOSPlatform("windows")]
public static class IconExtensions
{
	/// <summary>
	/// Saves this <see cref="Icon" /> to the specified file.
	/// </summary>
	/// <param name="icon">The <see cref="Icon" /> to save.</param>
	/// <param name="path">A <see cref="string" /> that contains the name of the file to which to save this <see cref="Icon" />.</param>
	public static void Save(this Icon icon, string path)
	{
		Check.ArgumentNull(icon);
		Check.ArgumentNull(path);

		using FileStream file = File.Create(path);
		icon.Save(file);
	}
	/// <summary>
	/// Converts this <see cref="Icon" /> to its <see cref="byte" />[] representation.
	/// </summary>
	/// <param name="icon">The <see cref="Icon" /> to save.</param>
	/// <returns>
	/// A new <see cref="byte" />[] representing this <see cref="Icon" />.
	/// </returns>
	public static byte[] ToArray(this Icon icon)
	{
		Check.ArgumentNull(icon);

		using MemoryStream memoryStream = new();
		icon.Save(memoryStream);
		return memoryStream.ToArray();
	}
}