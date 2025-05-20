﻿using BytecodeApi.Data;
using BytecodeApi.Extensions;
using System.IO.Compression;

namespace BytecodeApi.IO;

/// <summary>
/// Class to create and decompress ZIP archive files from <see cref="Blob" /> objects.
/// </summary>
public static class ZipCompression
{
	/// <summary>
	/// Creates a ZIP archive from a single <see cref="Blob" /> object and returns a <see cref="byte" />[] representing the compressed archive.
	/// </summary>
	/// <param name="blob">The <see cref="Blob" /> object to create the ZIP archive from.</param>
	/// <returns>
	/// A <see cref="byte" />[] representing the compressed ZIP archive with the specified <see cref="Blob" /> object.
	/// </returns>
	public static byte[] Compress(Blob blob)
	{
		Check.ArgumentNull(blob);

		using MemoryStream memoryStream = new();
		Compress(blob, memoryStream);
		return memoryStream.ToArray();
	}
	/// <summary>
	/// Creates a ZIP archive from a single <see cref="Blob" /> object and writes the compressed archive to <paramref name="stream" />.
	/// </summary>
	/// <param name="blob">The <see cref="Blob" /> object to create the ZIP archive from.</param>
	/// <param name="stream">The <see cref="Stream" /> to write the compressed archive to.</param>
	public static void Compress(Blob blob, Stream stream)
	{
		Compress(blob, stream, false);
	}
	/// <summary>
	/// Creates a ZIP archive from a single <see cref="Blob" /> object and writes the compressed archive to <paramref name="stream" />.
	/// </summary>
	/// <param name="blob">The <see cref="Blob" /> object to create the ZIP archive from.</param>
	/// <param name="stream">The <see cref="Stream" /> to write the compressed archive to.</param>
	/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
	public static void Compress(Blob blob, Stream stream, bool leaveOpen)
	{
		Check.ArgumentNull(blob);
		Check.ArgumentNull(stream);

		using ZipArchive archive = new(stream, ZipArchiveMode.Create, leaveOpen);
		archive.CreateEntry(blob.Name, blob.Content);
	}
	/// <summary>
	/// Creates a ZIP archive from the specified collection of <see cref="Blob" /> objects and returns a <see cref="byte" />[] representing the compressed archive.
	/// </summary>
	/// <param name="blobs">A collection of <see cref="Blob" /> objects to create the ZIP archive from.</param>
	/// <returns>
	/// A <see cref="byte" />[] representing the compressed ZIP archive with all <see cref="Blob" /> objects from the specified collection.
	/// </returns>
	public static byte[] Compress(BlobCollection blobs)
	{
		Check.ArgumentNull(blobs);

		using MemoryStream memoryStream = new();
		Compress(blobs, memoryStream);
		return memoryStream.ToArray();
	}
	/// <summary>
	/// Creates a ZIP archive from the specified collection of <see cref="Blob" /> objects and writes the compressed archive to <paramref name="stream" />.
	/// </summary>
	/// <param name="blobs">A collection of <see cref="Blob" /> objects to create the ZIP archive from.</param>
	/// <param name="stream">The <see cref="Stream" /> to write the compressed archive to.</param>
	public static void Compress(BlobCollection blobs, Stream stream)
	{
		Compress(blobs, stream, false);
	}
	/// <summary>
	/// Creates a ZIP archive from the specified collection of <see cref="Blob" /> objects and writes the compressed archive to <paramref name="stream" />.
	/// </summary>
	/// <param name="blobs">A collection of <see cref="Blob" /> objects to create the ZIP archive from.</param>
	/// <param name="stream">The <see cref="Stream" /> to write the compressed archive to.</param>
	/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
	public static void Compress(BlobCollection blobs, Stream stream, bool leaveOpen)
	{
		Check.ArgumentNull(blobs);
		Check.ArgumentNull(stream);

		using ZipArchive archive = new(stream, ZipArchiveMode.Create, leaveOpen);

		foreach (Blob blob in blobs)
		{
			archive.CreateEntry(blob.Name, blob.Content);
		}
	}
	/// <summary>
	/// Creates a ZIP archive from the specified <see cref="BlobTree" /> and returns a <see cref="byte" />[] representing the compressed archive.
	/// </summary>
	/// <param name="blobs">A tree of <see cref="Blob" /> objects to create the ZIP archive from.</param>
	/// <returns>
	/// A <see cref="byte" />[] representing the compressed ZIP archive with all <see cref="Blob" /> objects from the specified tree.
	/// </returns>
	public static byte[] Compress(BlobTree blobs)
	{
		Check.ArgumentNull(blobs);

		using MemoryStream memoryStream = new();
		Compress(blobs, memoryStream);
		return memoryStream.ToArray();
	}
	/// <summary>
	/// Creates a ZIP archive from the specified <see cref="BlobTree" /> and writes the compressed archive to <paramref name="stream" />.
	/// </summary>
	/// <param name="blobs">A tree of <see cref="Blob" /> objects to create the ZIP archive from.</param>
	/// <param name="stream">The <see cref="Stream" /> to write the compressed archive to.</param>
	public static void Compress(BlobTree blobs, Stream stream)
	{
		Compress(blobs, stream, false);
	}
	/// <summary>
	/// Creates a ZIP archive from the specified <see cref="BlobTree" /> and writes the compressed archive to <paramref name="stream" />.
	/// </summary>
	/// <param name="blobs">A tree of <see cref="Blob" /> objects to create the ZIP archive from.</param>
	/// <param name="stream">The <see cref="Stream" /> to write the compressed archive to.</param>
	/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
	public static void Compress(BlobTree blobs, Stream stream, bool leaveOpen)
	{
		Check.ArgumentNull(blobs);
		Check.ArgumentNull(stream);

		using ZipArchive archive = new(stream, ZipArchiveMode.Create, leaveOpen);
		CreateEntries("", blobs.Root);

		void CreateEntries(string path, BlobTreeNode node)
		{
			foreach (BlobTreeNode childNode in node.Nodes)
			{
				CreateEntries(Path.Combine(path, childNode.Name), childNode);
			}

			foreach (Blob blob in node.Blobs)
			{
				archive.CreateEntry(Path.Combine(path, blob.Name), blob.Content);
			}
		}
	}
	/// <summary>
	/// Creates a <see cref="BlobTree" /> from the ZIP archive read from the specified file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a ZIP file.</param>
	/// <returns>
	/// The <see cref="BlobTree" /> this method creates.
	/// </returns>
	public static BlobTree Decompress(string path)
	{
		Check.ArgumentNull(path);
		Check.FileNotFound(path);

		using FileStream file = File.OpenRead(path);
		return Decompress(file);
	}
	/// <summary>
	/// Creates a <see cref="BlobTree" /> from the ZIP archive in the specified <see cref="byte" />[].
	/// </summary>
	/// <param name="file">A <see cref="byte" />[] that represents a ZIP archive.</param>
	/// <returns>
	/// The <see cref="BlobTree" /> this method creates.
	/// </returns>
	public static BlobTree Decompress(byte[] file)
	{
		Check.ArgumentNull(file);

		using MemoryStream memoryStream = new(file);
		return Decompress(memoryStream);
	}
	/// <summary>
	/// Creates a <see cref="BlobTree" /> from the ZIP archive read from the specified <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> from which to read the ZIP archive from.</param>
	/// <returns>
	/// The <see cref="BlobTree" /> this method creates.
	/// </returns>
	public static BlobTree Decompress(Stream stream)
	{
		return Decompress(stream, false);
	}
	/// <summary>
	/// Creates a <see cref="BlobTree" /> from the ZIP archive read from the specified <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> from which to read the ZIP archive from.</param>
	/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
	/// <returns>
	/// The <see cref="BlobTree" /> this method creates.
	/// </returns>
	public static BlobTree Decompress(Stream stream, bool leaveOpen)
	{
		Check.ArgumentNull(stream);

		BlobTree tree = new();

		using ZipArchive archive = new(stream, ZipArchiveMode.Read, leaveOpen);

		foreach (ZipArchiveEntry entry in archive.Entries)
		{
			BlobTreeNode node = tree.Root;

			foreach (string pathPart in entry.FullName.TrimEndString(entry.Name, StringComparison.OrdinalIgnoreCase, true).TrimEnd('\\', '/').Split('\\', StringSplitOptions.RemoveEmptyEntries))
			{
				if (node.Node(pathPart, true) is BlobTreeNode childNode)
				{
					node = childNode;
				}
				else
				{
					BlobTreeNode newChildNode = new(pathPart);
					node.Nodes.Add(newChildNode);
					node = newChildNode;
				}
			}

			node.Blobs.Add(new(entry.Name, entry.GetContent()));
		}

		return tree;
	}
}