using BytecodeApi.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BytecodeApi.Data
{
	//FEATURE: FindBlob(string path)
	/// <summary>
	/// Represents a tree node within a <see cref="BlobTree" />.
	/// </summary>
	public class BlobTreeNode
	{
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

			foreach (string pathPart in path.Split('\\'))
			{
				node = CSharp.Try(() => node.Nodes[pathPart, ignoreCase]);
				if (node == null) break;
			}

			return node;
		}
		/// <summary>
		/// Computes the size, in bytes, of all <see cref="Blob" /> objects within this <see cref="BlobTreeNode" /> recursively.
		/// </summary>
		/// <returns>
		/// The size, in bytes, of all <see cref="Blob" /> objects within this <see cref="BlobTreeNode" /> recursively.
		/// </returns>
		public long ComputeSize()
		{
			return Nodes.Sum(node => node.ComputeSize()) + Blobs.ComputeSize();
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + Name + ", Nodes: " + Nodes.Count + ", Blobs: " + Blobs.Count + "]";
		}
	}
}