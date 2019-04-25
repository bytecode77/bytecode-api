using BytecodeApi.Extensions;
using System;
using System.IO;

namespace BytecodeApi.Data
{
	/// <summary>
	/// Represents an entity composed of a name and binary content in form or a <see cref="byte" />[].
	/// </summary>
	public class Blob : IEquatable<Blob>
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
		public object Tag { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Blob" /> class.
		/// </summary>
		public Blob()
		{
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
			Check.ArgumentNull(path, nameof(path));
			Check.FileNotFound(path);

			return new Blob(Path.GetFileName(path), File.ReadAllBytes(path));
		}

		/// <summary>
		/// Writes the contents of <see cref="Content" /> to a binary file.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path of a file to which <see cref="Content" /> is written to.</param>
		public void Save(string path)
		{
			Check.ArgumentNull(path, nameof(path));

			File.WriteAllBytes(path, Content);
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + Name + ", Size: " + Content?.Length + "]";
		}
		/// <summary>
		/// Determines whether the specified <see cref="object" /> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
		/// <returns>
		/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public override bool Equals(object obj)
		{
			return obj is Blob blob && Equals(blob);
		}
		/// <summary>
		/// Determines whether this instance is equal to another <see cref="Blob" />.
		/// </summary>
		/// <param name="other">The <see cref="Blob" /> to compare to this instance.</param>
		/// <returns>
		/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(Blob other)
		{
			return other != null && GetType() == other.GetType() && (this == other || Name == other.Name && Content.Compare(other.Content) && Equals(Tag, other.Tag));
		}
		/// <summary>
		/// Returns a hash code for this <see cref="Blob" />.
		/// </summary>
		/// <returns>
		/// The hash code for this <see cref="Blob" /> instance.
		/// </returns>
		public override int GetHashCode()
		{
			return (Name?.GetHashCode() ?? 0) ^ (Content?.GetHashCode() ?? 0) ^ (Tag?.GetHashCode() ?? 0);
		}
	}
}