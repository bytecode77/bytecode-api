using BytecodeApi.Extensions;
using BytecodeApi.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BytecodeApi.Data
{
	/// <summary>
	/// Represents a tree node within a <see cref="BlobTree" />.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class BlobTreeNode
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay => CSharp.DebuggerDisplay<BlobTreeNode>("Name = {0}, Nodes: {1}, Blobs: {2}, Tag = {3}", new QuotedString(Name), Nodes.Count, Blobs.Count, Tag);
		/// <summary>
		/// Gets or sets the name of the <see cref="BlobTreeNode" />.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Gets the collection of nodes associated with this <see cref="BlobTreeNode" />.
		/// </summary>
		public BlobTreeNodeCollection Nodes { get; private set; }
		/// <summary>
		/// Gets the collection of <see cref="Blob" /> objects associated with this <see cref="BlobTreeNode" />.
		/// </summary>
		public BlobCollection Blobs { get; private set; }
		/// <summary>
		/// Gets or sets the object that contains data about the <see cref="BlobTreeNode" />.
		/// </summary>
		public object Tag { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BlobTreeNode" /> class.
		/// </summary>
		public BlobTreeNode()
		{
			Nodes = new BlobTreeNodeCollection();
			Blobs = new BlobCollection();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="BlobTreeNode" /> class with the specified name.
		/// </summary>
		/// <param name="name">The name of the <see cref="BlobTreeNode" />.</param>
		public BlobTreeNode(string name) : this()
		{
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
			Nodes.AddRange(nodes);
			Blobs.AddRange(blobs);
		}

		/// <summary>
		/// Determines whether a <see cref="BlobTreeNode" /> with the specified name exists in this <see cref="BlobTreeNode" />.
		/// </summary>
		/// <param name="name">The name of the <see cref="BlobTreeNode" /> to check.</param>
		/// <returns>
		/// <see langword="true" />, if the <see cref="BlobTreeNode" /> with the specified name exists;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool HasNode(string name)
		{
			return HasNode(name, false);
		}
		/// <summary>
		/// Determines whether a <see cref="BlobTreeNode" /> with the specified name exists in this <see cref="BlobTreeNode" />.
		/// </summary>
		/// <param name="name">The name of the <see cref="BlobTreeNode" /> to check.</param>
		/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during comparison.</param>
		/// <returns>
		/// <see langword="true" />, if the <see cref="BlobTreeNode" /> with the specified name exists;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool HasNode(string name, bool ignoreCase)
		{
			return Nodes.HasNode(name, ignoreCase);
		}
		/// <summary>
		/// Determines whether a <see cref="Blob" /> with the specified name exists in this <see cref="BlobTreeNode" />.
		/// </summary>
		/// <param name="name">The name of the <see cref="Blob" /> to check.</param>
		/// <returns>
		/// <see langword="true" />, if the <see cref="Blob" /> with the specified name exists;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool HasBlob(string name)
		{
			return HasBlob(name, false);
		}
		/// <summary>
		/// Determines whether a <see cref="Blob" /> with the specified name exists in this <see cref="BlobTreeNode" />.
		/// </summary>
		/// <param name="name">The name of the <see cref="Blob" /> to check.</param>
		/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during comparison.</param>
		/// <returns>
		/// <see langword="true" />, if the <see cref="Blob" /> with the specified name exists;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool HasBlob(string name, bool ignoreCase)
		{
			return Blobs.HasBlob(name, ignoreCase);
		}

		/// <summary>
		/// Tries to find a node by the specified case sensitive path. The path contains each node name, separated by a backslash. Returns <see langword="null" />, if the <see cref="BlobTreeNode" /> could not be found.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying a case sensitive path. The path contains each node name, separated by a backslash.</param>
		/// <returns>
		/// The <see cref="BlobTreeNode" /> that was found by the specified path, or <see langword="null" />, if it could not be found.
		/// </returns>
		public BlobTreeNode FindNode(string path)
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
		public BlobTreeNode FindNode(string path, bool ignoreCase)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.ArgumentEx.StringNotEmpty(path, nameof(path));

			BlobTreeNode node = this;

			foreach (string pathPart in path.Trim('\\').Split('\\'))
			{
				node = node.HasNode(pathPart, ignoreCase) ? node.Nodes[pathPart, ignoreCase] : null;
				if (node == null) break;
			}

			return node;
		}
		/// <summary>
		/// Tries to find a blob by the specified case sensitive path. The path contains each node name, separated by a backslash. The last element represents the name of the blob. Returns <see langword="null" />, if the <see cref="Blob" /> could not be found.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying a case sensitive path. The path contains each node name, separated by a backslash. The last element represents the name of the blob.</param>
		/// <returns>
		/// The <see cref="Blob" /> that was found by the specified path, or <see langword="null" />, if it could not be found.
		/// </returns>
		public Blob FindBlob(string path)
		{
			return FindBlob(path, false);
		}
		/// <summary>
		/// Tries to find a blob by the specified path. The path contains each node name, separated by a backslash. The last element represents the name of the blob. Returns <see langword="null" />, if the <see cref="Blob" /> could not be found.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying a path. The path contains each node name, separated by a backslash. The last element represents the name of the blob.</param>
		/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during name comparison.</param>
		/// <returns>
		/// The <see cref="Blob" /> that was found by the specified path, or <see langword="null" />, if it could not be found.
		/// </returns>
		public Blob FindBlob(string path, bool ignoreCase)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.ArgumentEx.StringNotEmpty(path, nameof(path));

			path = path.Trim('\\');

			if (FindNode(Path.GetDirectoryName(path), ignoreCase) is BlobTreeNode node)
			{
				string name = Path.GetFileName(path);
				return node.HasBlob(name, ignoreCase) ? node.Blobs[name, ignoreCase] : null;
			}
			else
			{
				return null;
			}

		}
		/// <summary>
		/// Creates a new one-dimensional <see cref="BlobCollection" /> containing all <see cref="Blob" /> objects including the root node and all child nodes recursively.
		/// </summary>
		/// <returns>
		/// The <see cref="BlobCollection" /> this method creates.
		/// </returns>
		public BlobCollection Flatten()
		{
			BlobCollection blobs = new BlobCollection();
			AddNode(this);
			return blobs;

			void AddNode(BlobTreeNode node)
			{
				foreach (BlobTreeNode childNode in node.Nodes) AddNode(childNode);
				blobs.AddRange(node.Blobs);
			}
		}
		/// <summary>
		/// Computes the size, in bytes, of all <see cref="Blob" /> objects within this <see cref="BlobTreeNode" /> recursively.
		/// </summary>
		/// <returns>
		/// The size, in bytes, of all <see cref="Blob" /> objects within this <see cref="BlobTreeNode" /> recursively.
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
}