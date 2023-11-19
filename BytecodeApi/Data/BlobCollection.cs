﻿using BytecodeApi.Extensions;
using System.Collections;

namespace BytecodeApi.Data;

/// <summary>
/// Represents a collection of <see cref="Blob" /> objects.
/// </summary>
public sealed class BlobCollection : ICollection<Blob>
{
	private readonly List<Blob> Blobs;
	/// <summary>
	/// Gets the <see cref="Blob" /> at the specified index.
	/// </summary>
	/// <param name="index">The index at which to retrieve the <see cref="Blob" />.</param>
	public Blob this[int index]
	{
		get
		{
			Check.IndexOutOfRange(index, Count);
			return Blobs[index];
		}
		set
		{
			Check.IndexOutOfRange(index, Count);
			Blobs[index] = value;
		}
	}
	/// <summary>
	/// Gets the <see cref="Blob" /> with the specified case sensitive name and throws an exception, if it was not found.
	/// </summary>
	/// <param name="name">A <see cref="string" /> specifying the name of the <see cref="Blob" />.</param>
	public Blob this[string name]
	{
		get
		{
			Check.ArgumentNull(name);
			return Blobs.FirstOrDefault(b => b.Name == name) ?? throw Throw.KeyNotFound($"A blob with the name '{name}' was not found.");
		}
	}
	/// <summary>
	/// Gets the number of elements contained in the <see cref="BlobCollection" />.
	/// </summary>
	public int Count => Blobs.Count;
	/// <summary>
	/// Gets a value indicating whether the <see cref="BlobCollection" /> is read-only.
	/// </summary>
	public bool IsReadOnly => false;

	/// <summary>
	/// Initializes a new instance of the <see cref="BlobCollection" /> class.
	/// </summary>
	public BlobCollection()
	{
		Blobs = new();
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="BlobCollection" /> class with the specified collection of blobs.
	/// </summary>
	/// <param name="blobs">A collection of <see cref="Blob" /> objects to add to this <see cref="BlobCollection" />.</param>
	public BlobCollection(IEnumerable<Blob> blobs) : this()
	{
		Check.ArgumentNull(blobs);

		Blobs.AddRange(blobs);
	}
	/// <summary>
	/// Creates a <see cref="BlobCollection" /> from the specified directory.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying the path to a directory from which to create the <see cref="BlobCollection" />. Subdirectories are not searched.</param>
	/// <returns>
	/// The <see cref="BlobCollection" /> this method creates.
	/// </returns>
	public static BlobCollection FromDirectory(string path)
	{
		return FromDirectory(path, false);
	}
	/// <summary>
	/// Creates a <see cref="BlobCollection" /> from the specified directory.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying the path to a directory from which to create the <see cref="BlobCollection" />.</param>
	/// <param name="recursive"><see langword="true" /> to search subdirectories recursively.</param>
	/// <returns>
	/// The <see cref="BlobCollection" /> this method creates.
	/// </returns>
	public static BlobCollection FromDirectory(string path, bool recursive)
	{
		Check.ArgumentNull(path);
		Check.DirectoryNotFound(path);

		return new
		(
			new DirectoryInfo(path)
				.GetFiles("*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
				.Select(file => Blob.FromFile(file.FullName))
		);
	}

	/// <summary>
	/// Computes the size, in bytes, of all <see cref="Blob" /> objects within this <see cref="BlobCollection" />.
	/// </summary>
	/// <returns>
	/// The size, in bytes, of all <see cref="Blob" /> objects within this <see cref="BlobCollection" />.
	/// </returns>
	public long ComputeSize()
	{
		return Blobs.Sum(blob => blob.Content?.Length ?? 0);
	}
	/// <summary>
	/// Writes the contents of all <see cref="Blob" /> objects to the specified directory, where <see cref="Blob.Name" /> is used as the filename and <see cref="Blob.Content" /> is written to the file. Existing files are overwritten.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying the path to a directory to which this <see cref="BlobCollection" /> is written to.</param>
	public void SaveToDirectory(string path)
	{
		Check.ArgumentNull(path);
		Check.DirectoryNotFound(path);

		if (Blobs.FirstOrDefault(blob => !Validate.FileName(blob.Name)) is Blob illegalBlob)
		{
			throw Blob.CreateIllegalFilenameException(illegalBlob);
		}

		foreach (Blob blob in Blobs)
		{
			File.WriteAllBytes(Path.Combine(path, blob.Name), blob.Content);
		}
	}

	/// <summary>
	/// Adds a <see cref="Blob" /> to the end of the <see cref="BlobCollection" />.
	/// </summary>
	/// <param name="item">The <see cref="Blob" /> to be added to the end of the <see cref="BlobCollection" />.</param>
	public void Add(Blob item)
	{
		Check.ArgumentNull(item);

		Blobs.Add(item);
	}
	/// <summary>
	/// Removes the first occurrence of a specific <see cref="Blob" /> from the <see cref="BlobCollection" />.
	/// </summary>
	/// <param name="item">The <see cref="Blob" /> to remove from the <see cref="BlobCollection" />.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="item" /> is successfully removed;
	/// otherwise, <see langword="false" />.
	/// This method also returns <see langword="false" />, if <paramref name="item" /> was not found in the <see cref="BlobCollection" />.
	/// </returns>
	public bool Remove(Blob item)
	{
		return Blobs.Remove(item);
	}
	/// <summary>
	/// Removes all elements from the <see cref="BlobCollection" />.
	/// </summary>
	public void Clear()
	{
		Blobs.Clear();
	}
	/// <summary>
	/// Determines whether an element is in the <see cref="BlobCollection" />.
	/// </summary>
	/// <param name="item">The <see cref="Blob" /> to locate in the <see cref="BlobCollection" />.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="item" /> is found in the <see cref="BlobCollection" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Contains(Blob item)
	{
		return Blobs.Contains(item);
	}
	void ICollection<Blob>.CopyTo(Blob[] array, int arrayIndex)
	{
		Check.ArgumentNull(array);
		Check.IndexOutOfRange(arrayIndex, array.Length - Count + 1);

		Blobs.CopyTo(array, arrayIndex);
	}
	/// <summary>
	/// Returns an enumerator that iterates through the <see cref="BlobCollection" />.
	/// </summary>
	/// <returns>
	/// An enumerator that can be used to iterate through the <see cref="BlobCollection" />.
	/// </returns>
	public IEnumerator<Blob> GetEnumerator()
	{
		return Blobs.GetEnumerator();
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return Blobs.GetEnumerator();
	}
}