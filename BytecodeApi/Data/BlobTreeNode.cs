using BytecodeApi.Extensions;
using System.Diagnostics;

namespace BytecodeApi.Data;

/// <summary>
/// Represents a tree node within a <see cref="BlobTree" />.
/// </summary>
[DebuggerDisplay($"{nameof(BlobTreeNode)}: Name = {{Name}}, Nodes: {{Nodes.Count}}, Blobs: {{Blobs.Count}}")]
public sealed class BlobTreeNode
{
	/// <summary>
	/// Gets or sets the name of the <see cref="BlobTreeNode" />.
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// Gets the collection of nodes associated with this <see cref="BlobTreeNode" />.
	/// </summary>
	public BlobTreeNodeCollection Nodes { get; private init; }
	/// <summary>
	/// Gets the collection of <see cref="Data.Blob" /> objects associated with this <see cref="BlobTreeNode" />.
	/// </summary>
	public BlobCollection Blobs { get; private init; }
	/// <summary>
	/// Gets or sets the object that contains data about the <see cref="BlobTreeNode" />.
	/// </summary>
	public object? Tag { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="BlobTreeNode" /> class.
	/// </summary>
	public BlobTreeNode()
	{
		Name = "";
		Nodes = new();
		Blobs = new();
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="BlobTreeNode" /> class with the specified name.
	/// </summary>
	/// <param name="name">The name of the <see cref="BlobTreeNode" />.</param>
	public BlobTreeNode(string name) : this()
	{
		Check.ArgumentNull(name);

		Name = name;
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="BlobTreeNode" /> class with the specified name and elemets.
	/// </summary>
	/// <param name="name">The name of the <see cref="BlobTreeNode" />.</param>
	/// <param name="nodes">A collection of nodes to add to the <see cref="BlobTreeNode" />.</param>
	/// <param name="blobs">A collection of blobs to add to the <see cref="BlobTreeNode" />.</param>
	public BlobTreeNode(string name, IEnumerable<BlobTreeNode> nodes, IEnumerable<Blob> blobs) : this(name)
	{
		Check.ArgumentNull(blobs);

		Nodes.AddRange(nodes);
		Blobs.AddRange(blobs);
	}

	/// <summary>
	/// Retrieves a <see cref="BlobTreeNode" /> with the specified name.
	/// </summary>
	/// <param name="name">A <see cref="string" /> specifying the name of the <see cref="BlobTreeNode" />.</param>
	/// <returns>
	/// The <see cref="BlobTreeNode" /> with the specified name, or <see langword="null" /> if no matching node was found.
	/// </returns>
	public BlobTreeNode? Node(string name)
	{
		return Node(name, false);
	}
	/// <summary>
	/// Retrieves a <see cref="BlobTreeNode" /> with the specified name.
	/// </summary>
	/// <param name="name">A <see cref="string" /> specifying the name of the <see cref="BlobTreeNode" />.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during name comparison.</param>
	/// <returns>
	/// The <see cref="BlobTreeNode" /> with the specified name, or <see langword="null" /> if no matching node was found.
	/// </returns>
	public BlobTreeNode? Node(string name, bool ignoreCase)
	{
		Check.ArgumentNull(name);

		return Nodes.FirstOrDefault(node => node.Name.Equals(name, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
	}
	/// <summary>
	/// Retrieves a <see cref="Data.Blob" /> with the specified name.
	/// </summary>
	/// <param name="name">A <see cref="string" /> specifying the name of the <see cref="Data.Blob" />.</param>
	/// <returns>
	/// The <see cref="Data.Blob" /> with the specified name, or <see langword="null" /> if no matching blob was found.
	/// </returns>
	public Blob? Blob(string name)
	{
		return Blob(name, false);
	}
	/// <summary>
	/// Retrieves a <see cref="Data.Blob" /> with the specified name.
	/// </summary>
	/// <param name="name">A <see cref="string" /> specifying the name of the <see cref="Data.Blob" />.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during name comparison.</param>
	/// <returns>
	/// The <see cref="Data.Blob" /> with the specified name, or <see langword="null" /> if no matching blob was found.
	/// </returns>
	public Blob? Blob(string name, bool ignoreCase)
	{
		Check.ArgumentNull(name);

		return Blobs.FirstOrDefault(blob => blob.Name.Equals(name, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
	}
	/// <summary>
	/// Tries to find a node by the specified case sensitive path. The path contains each node name, separated by a backslash. Returns <see langword="null" />, if the <see cref="BlobTreeNode" /> could not be found.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying a case sensitive path. The path contains each node name, separated by a backslash.</param>
	/// <returns>
	/// The <see cref="BlobTreeNode" /> that was found by the specified path, or <see langword="null" />, if it could not be found.
	/// </returns>
	public BlobTreeNode? FindNode(string path)
	{
		return FindNode(path, false);
	}
	/// <summary>
	/// Tries to find a node by the specified path. The path contains each node name, separated by a backslash. Returns <see langword="null" />, if the <see cref="BlobTreeNode" /> could not be found.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying a path. The path contains each node name, separated by a backslash.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during name comparison.</param>
	/// <returns>
	/// The <see cref="BlobTreeNode" /> that was found by the specified path, or <see langword="null" />, if it could not be found.
	/// </returns>
	public BlobTreeNode? FindNode(string path, bool ignoreCase)
	{
		Check.ArgumentNull(path);
		Check.ArgumentEx.StringNotEmpty(path);

		BlobTreeNode? node = this;

		foreach (string pathPart in path.Trim('\\').Split('\\'))
		{
			node = node.Node(pathPart, ignoreCase);
			if (node == null) break;
		}

		return node;
	}
	/// <summary>
	/// Tries to find a blob by the specified case sensitive path. The path contains each node name, separated by a backslash. The last element represents the name of the blob. Returns <see langword="null" />, if the <see cref="Data.Blob" /> could not be found.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying a case sensitive path. The path contains each node name, separated by a backslash. The last element represents the name of the blob.</param>
	/// <returns>
	/// The <see cref="Data.Blob" /> that was found by the specified path, or <see langword="null" />, if it could not be found.
	/// </returns>
	public Blob? FindBlob(string path)
	{
		return FindBlob(path, false);
	}
	/// <summary>
	/// Tries to find a blob by the specified path. The path contains each node name, separated by a backslash. The last element represents the name of the blob. Returns <see langword="null" />, if the <see cref="Data.Blob" /> could not be found.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying a path. The path contains each node name, separated by a backslash. The last element represents the name of the blob.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during name comparison.</param>
	/// <returns>
	/// The <see cref="Data.Blob" /> that was found by the specified path, or <see langword="null" />, if it could not be found.
	/// </returns>
	public Blob? FindBlob(string path, bool ignoreCase)
	{
		Check.ArgumentNull(path);
		Check.ArgumentEx.StringNotEmpty(path);

		path = path.Trim('\\');

		if (Path.GetDirectoryName(path) is string nodePath &&
			Path.GetFileName(path) is string blobName &&
			FindNode(nodePath, ignoreCase) is BlobTreeNode node)
		{
			return node.Blob(blobName, ignoreCase);
		}
		else
		{
			return null;
		}
	}
	/// <summary>
	/// Creates a new one-dimensional <see cref="BlobCollection" /> containing all <see cref="Data.Blob" /> objects including the root node and all child nodes recursively.
	/// </summary>
	/// <returns>
	/// The <see cref="BlobCollection" /> this method creates.
	/// </returns>
	public BlobCollection Flatten()
	{
		BlobCollection blobs = new();
		AddNode(this);
		return blobs;

		void AddNode(BlobTreeNode node)
		{
			foreach (BlobTreeNode childNode in node.Nodes)
			{
				AddNode(childNode);
			}

			blobs.AddRange(node.Blobs);
		}
	}
	/// <summary>
	/// Computes the size, in bytes, of all <see cref="Data.Blob" /> objects within this <see cref="BlobTreeNode" /> recursively.
	/// </summary>
	/// <returns>
	/// The size, in bytes, of all <see cref="Data.Blob" /> objects within this <see cref="BlobTreeNode" /> recursively.
	/// </returns>
	public long ComputeSize()
	{
		return Nodes.ComputeSize() + Blobs.ComputeSize();
	}

	/// <summary>
	/// Returns the name of this <see cref="BlobTreeNode" />.
	/// </summary>
	/// <returns>
	/// The name of this <see cref="BlobTreeNode" />.
	/// </returns>
	public override string ToString()
	{
		return Name;
	}
}