using BytecodeApi.Extensions;
using System;
using System.IO;

namespace BytecodeApi.IO.Http
{
	/// <summary>
	/// Represents a file for HTTP multipart requests, used by the <see cref="HttpClient" /> class.
	/// </summary>
	public sealed class PostFile : IEquatable<PostFile>
	{
		/// <summary>
		/// Gets or sets the posted form name of the HTTP multipart entry.
		/// </summary>
		public string FormName { get; set; }
		/// <summary>
		/// Gets or sets the uploaded filename of the HTTP multipart entry.
		/// </summary>
		public string FileName { get; set; }
		/// <summary>
		/// Gets or sets the binary content of the HTTP multipart entry.
		/// </summary>
		public byte[] Content { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PostFile" /> class.
		/// </summary>
		public PostFile()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="PostFile" /> class with the specified name, filename and content.
		/// </summary>
		/// <param name="formName">The posted form name of the HTTP multipart entry.</param>
		/// <param name="fileName">The uploaded filename of the HTTP multipart entry.</param>
		/// <param name="content">The binary content of the HTTP multipart entry.</param>
		public PostFile(string formName, string fileName, byte[] content) : this()
		{
			FormName = formName;
			FileName = fileName;
			Content = content;
		}
		/// <summary>
		/// Creates a <see cref="PostFile" /> from the specified file with the specified form name. The name of the file will be assigned to <see cref="FileName" />.
		/// </summary>
		/// <param name="formName">The posted form name of the HTTP multipart entry.</param>
		/// <param name="path">A <see cref="string" /> specifying the path of a file from which to create the <see cref="PostFile" />.</param>
		/// <returns>
		/// The <see cref="PostFile" /> this method creates.
		/// </returns>
		public static PostFile FromFile(string formName, string path)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.FileNotFound(path);

			return new PostFile(formName, Path.GetFileName(path), File.ReadAllBytes(path));
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + FormName + ", " + FileName + ", Size: " + Content?.Length + "]";
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
			return obj is PostFile file && Equals(file);
		}
		/// <summary>
		/// Determines whether this instance is equal to another <see cref="PostFile" />.
		/// </summary>
		/// <param name="other">The <see cref="PostFile" /> to compare to this instance.</param>
		/// <returns>
		/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(PostFile other)
		{
			return other != null && (this == other || FormName == other.FormName && FileName == other.FileName && Content.Compare(other.Content));
		}
		/// <summary>
		/// Returns a hash code for this <see cref="PostFile" />.
		/// </summary>
		/// <returns>
		/// The hash code for this <see cref="PostFile" /> instance.
		/// </returns>
		public override int GetHashCode()
		{
			return (FormName?.GetHashCode() ?? 0) ^ (FileName?.GetHashCode() ?? 0) ^ (Content?.GetHashCode() ?? 0);
		}
	}
}