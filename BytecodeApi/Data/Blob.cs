using BytecodeApi.Extensions;
using System.Diagnostics;

namespace BytecodeApi.Data;

/// <summary>
/// Represents an entity composed of a name and binary content in form or a <see cref="byte" />[].
/// </summary>
[DebuggerDisplay($"{nameof(Blob)}: Name = {{Name}}, Content = {{Content}}")]
public class Blob
{
	/// <summary>
	/// Gets or sets the name of the <see cref="Blob" />.
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// Gets or sets the binary content of the <see cref="Blob" />.
	/// </summary>
	public byte[] Content { get; set; }
	/// <summary>
	/// Gets or sets the object that contains data about the <see cref="Blob" />.
	/// </summary>
	public object? Tag { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="Blob" /> class.
	/// </summary>
	public Blob()
	{
		Name = "";
		Content = Array.Empty<byte>();
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="Blob" /> class with the specified name.
	/// </summary>
	/// <param name="name">The name of the <see cref="Blob" />.</param>
	public Blob(string name) : this()
	{
		Name = name;
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="Blob" /> class with the specified name and content.
	/// </summary>
	/// <param name="name">The name of the <see cref="Blob" />.</param>
	/// <param name="content">The binary content of the <see cref="Blob" />.</param>
	public Blob(string name, byte[] content) : this(name)
	{
		Content = content;
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="Blob" /> class with the specified name, content and a tag.
	/// </summary>
	/// <param name="name">The name of the <see cref="Blob" />.</param>
	/// <param name="content">The binary content of the <see cref="Blob" />.</param>
	/// <param name="tag">the object that contains data about the <see cref="Blob" />.</param>
	public Blob(string name, byte[] content, object tag) : this(name, content)
	{
		Tag = tag;
	}
	/// <summary>
	/// Creates a <see cref="Blob" /> from the specified file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying the path of a file from which to create the <see cref="Blob" />.</param>
	/// <returns>
	/// The <see cref="Blob" /> this method creates.
	/// </returns>
	public static Blob FromFile(string path)
	{
		Check.ArgumentNull(path);
		Check.FileNotFound(path);

		return new(Path.GetFileName(path), File.ReadAllBytes(path));
	}

	/// <summary>
	/// Compares this <see cref="Blob" /> agains another <see cref="Blob" />, including binary content. Returns <see langword="true" />, if both objects contain the exact same set of data.
	/// </summary>
	/// <param name="other">A <see cref="Blob" /> to compare to this instance to.</param>
	/// <returns>
	/// <see langword="true" />, if both objects contain the exact same set of data;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Compare([NotNullWhen(true)] Blob? other)
	{
		return other != null && (this == other || Name == other.Name && Content.Compare(other.Content) && Equals(Tag, other.Tag));
	}
	/// <summary>
	/// Writes the contents of <see cref="Content" /> to a binary file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying the path of a file to which <see cref="Content" /> is written to.</param>
	public void Save(string path)
	{
		Check.ArgumentNull(path);

		File.WriteAllBytes(path, Content);
	}

	/// <summary>
	/// Returns the name of this <see cref="Blob" />.
	/// </summary>
	/// <returns>
	/// The name of this <see cref="Blob" />.
	/// </returns>
	public override string ToString()
	{
		return Name;
	}

	internal static Exception CreateIllegalFilenameException(Blob blob)
	{
		return Throw.InvalidOperation($"Blob with the name '{blob.Name}' has illegal filename characters.");
	}
	internal static Exception CreateIllegalFilenameException(BlobTreeNode node)
	{
		return Throw.InvalidOperation($"Blob tree node with the name '{node.Name}' has illegal filename characters.");
	}
}