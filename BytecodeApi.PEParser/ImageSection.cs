using System.Diagnostics;

namespace BytecodeApi.PEParser;

/// <summary>
/// Represents a section of a PE image file, containing the header and a <see cref="byte" />[] representing the contents of the section.
/// </summary>
[DebuggerDisplay($"{nameof(ImageSection)}: Header = {{Header.Name,nq}}, Size: {{Data.Length}}")]
public sealed class ImageSection
{
	/// <summary>
	/// Gets the section header.
	/// </summary>
	public ImageSectionHeader Header { get; }
	/// <summary>
	/// Gets a <see cref="byte" />[] representing the contents of the section.
	/// </summary>
	public byte[] Data { get; }

	internal ImageSection(ImageSectionHeader header, byte[] data)
	{
		Header = header;
		Data = data;
	}
}