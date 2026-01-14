using System.Xml;
using System.Xml.Linq;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="XDocument" /> and related objects.
/// </summary>
public static class XDocumentExtensions
{
	/// <summary>
	/// Represents the <see cref="XmlWriterSettings" /> object that is used in the <see cref="SaveFormatted(XDocument, string)" /> method. This <see cref="object" /> is <see langword="static" /> and can be modified.
	/// <para>The default value is <see langword="new" /> <see cref="XmlWriterSettings" /> { Indent = <see langword="true" />, IndentChars = "\t" }</para>
	/// </summary>
	public static XmlWriterSettings FormattedXmlWriterSettings { get; set; }

	static XDocumentExtensions()
	{
		FormattedXmlWriterSettings = new()
		{
			Indent = true,
			IndentChars = "\t"
		};
	}

	extension(XDocument xml)
	{
		/// <summary>
		/// Serializes this <see cref="XDocument" /> to a file with the specified filename.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path of a file to which this <see cref="XDocument" /> is written to.</param>
		/// <param name="settings">An <see cref="XmlWriterSettings" /> value with serialization settings.</param>
		public void Save(string path, XmlWriterSettings settings)
		{
			Check.ArgumentNull(xml);
			Check.ArgumentNull(path);
			Check.ArgumentNull(settings);

			using XmlWriter xmlWriter = XmlWriter.Create(path, settings);
			xml.Save(xmlWriter);
		}
		/// <summary>
		/// Serializes this <see cref="XDocument" /> to a <see cref="Stream" />.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which this <see cref="XDocument" /> is written to.</param>
		/// <param name="settings">An <see cref="XmlWriterSettings" /> value with serialization settings.</param>
		public void Save(Stream stream, XmlWriterSettings settings)
		{
			Check.ArgumentNull(xml);
			Check.ArgumentNull(stream);
			Check.ArgumentNull(settings);

			using XmlWriter xmlWriter = XmlWriter.Create(stream, settings);
			xml.Save(xmlWriter);
		}
		/// <summary>
		/// Serializes this <see cref="XDocument" /> to a file with the specified filename using the formatting settings as specified in <see cref="FormattedXmlWriterSettings" />.
		/// </summary>
		/// <param name="path">A <see cref="string" /> specifying the path of a file to which this <see cref="XDocument" /> is written to.</param>
		public void SaveFormatted(string path)
		{
			Check.ArgumentNull(xml);
			Check.ArgumentNull(path);
			Check.ArgumentNull(FormattedXmlWriterSettings);

			xml.Save(path, FormattedXmlWriterSettings);
		}
		/// <summary>
		/// Serializes this <see cref="XDocument" /> to a <see cref="Stream" /> using the formatting settings as specified in <see cref="FormattedXmlWriterSettings" />.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which this <see cref="XDocument" /> is written to.</param>
		public void SaveFormatted(Stream stream)
		{
			Check.ArgumentNull(xml);
			Check.ArgumentNull(stream);
			Check.ArgumentNull(FormattedXmlWriterSettings);

			xml.Save(stream, FormattedXmlWriterSettings);
		}
		/// <summary>
		/// Serializes this <see cref="XDocument" /> to a <see cref="byte" />[].
		/// </summary>
		/// <returns>
		/// A new <see cref="byte" />[] representing this <see cref="XDocument" />.
		/// </returns>
		public byte[] ToArray()
		{
			Check.ArgumentNull(xml);

			using MemoryStream memoryStream = new();
			xml.Save(memoryStream);
			return memoryStream.ToArray();
		}
		/// <summary>
		/// Serializes this <see cref="XDocument" /> to a <see cref="byte" />[].
		/// </summary>
		/// <param name="settings">An <see cref="XmlWriterSettings" /> value with serialization settings.</param>
		/// <returns>
		/// A new <see cref="byte" />[] representing this <see cref="XDocument" />.
		/// </returns>
		public byte[] ToArray(XmlWriterSettings settings)
		{
			Check.ArgumentNull(xml);
			Check.ArgumentNull(settings);

			using MemoryStream memoryStream = new();
			xml.Save(memoryStream, settings);
			return memoryStream.ToArray();
		}
		/// <summary>
		/// Serializes this <see cref="XDocument" /> to a <see cref="byte" />[] using the formatting settings as specified in <see cref="FormattedXmlWriterSettings" />.
		/// </summary>
		/// <returns>
		/// A new <see cref="byte" />[] representing this <see cref="XDocument" />.
		/// </returns>
		public byte[] ToArrayFormatted()
		{
			Check.ArgumentNull(xml);

			using MemoryStream memoryStream = new();
			xml.SaveFormatted(memoryStream);
			return memoryStream.ToArray();
		}
	}

	extension(XElement element)
	{
		/// <summary>
		/// Returns the <see cref="XAttribute" /> of this <see cref="XElement" /> that has the specified <see cref="XName" />, or returns a new <see cref="XAttribute" /> with a default value.
		/// </summary>
		/// <param name="name">The <see cref="XName" /> of the <see cref="XAttribute" /> to get.</param>
		/// <param name="defaultValue">The value that is used if the <see cref="XAttribute" /> was not found.</param>
		/// <returns>
		/// The <see cref="XAttribute" /> of this <see cref="XElement" /> that has the specified <see cref="XName" />, or a new <see cref="XAttribute" /> with a default value, if the <see cref="XAttribute" /> was not found.
		/// </returns>
		public XAttribute Attribute(XName name, object defaultValue)
		{
			Check.ArgumentNull(element);
			Check.ArgumentNull(name);

			return element.Attribute(name) ?? new(name, defaultValue);
		}
	}
}