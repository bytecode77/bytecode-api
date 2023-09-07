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

	/// <summary>
	/// Serializes this <see cref="XDocument" /> to a file with the specified filename.
	/// </summary>
	/// <param name="xml">The <see cref="XDocument" /> to be serialized.</param>
	/// <param name="path">A <see cref="string" /> specifying the path of a file to which this <see cref="XDocument" /> is written to.</param>
	/// <param name="settings">An <see cref="XmlWriterSettings" /> value with serialization settings.</param>
	public static void Save(this XDocument xml, string path, XmlWriterSettings settings)
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
	/// <param name="xml">The <see cref="XDocument" /> to be serialized.</param>
	/// <param name="stream">The <see cref="Stream" /> to which this <see cref="XDocument" /> is written to.</param>
	/// <param name="settings">An <see cref="XmlWriterSettings" /> value with serialization settings.</param>
	public static void Save(this XDocument xml, Stream stream, XmlWriterSettings settings)
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
	/// <param name="xml">The <see cref="XDocument" /> to be serialized.</param>
	/// <param name="path">A <see cref="string" /> specifying the path of a file to which this <see cref="XDocument" /> is written to.</param>
	public static void SaveFormatted(this XDocument xml, string path)
	{
		Check.ArgumentNull(xml);
		Check.ArgumentNull(path);
		Check.ArgumentNull(FormattedXmlWriterSettings);

		xml.Save(path, FormattedXmlWriterSettings);
	}
	/// <summary>
	/// Serializes this <see cref="XDocument" /> to a <see cref="Stream" /> using the formatting settings as specified in <see cref="FormattedXmlWriterSettings" />.
	/// </summary>
	/// <param name="xml">The <see cref="XDocument" /> to be serialized.</param>
	/// <param name="stream">The <see cref="Stream" /> to which this <see cref="XDocument" /> is written to.</param>
	public static void SaveFormatted(this XDocument xml, Stream stream)
	{
		Check.ArgumentNull(xml);
		Check.ArgumentNull(stream);
		Check.ArgumentNull(FormattedXmlWriterSettings);

		xml.Save(stream, FormattedXmlWriterSettings);
	}
	/// <summary>
	/// Returns the <see cref="XAttribute" /> of this <see cref="XElement" /> that has the specified <see cref="XName" />, or returns a new <see cref="XAttribute" /> with a default value.
	/// </summary>
	/// <param name="element">The <see cref="XElement" /> to be searched by the <paramref name="name" /> parameter.</param>
	/// <param name="name">The <see cref="XName" /> of the <see cref="XAttribute" /> to get.</param>
	/// <param name="defaultValue">The value that is used if the <see cref="XAttribute" /> was not found.</param>
	/// <returns>
	/// The <see cref="XAttribute" /> of this <see cref="XElement" /> that has the specified <see cref="XName" />, or a new <see cref="XAttribute" /> with a default value, if the <see cref="XAttribute" /> was not found.
	/// </returns>
	public static XAttribute Attribute(this XElement element, XName name, object defaultValue)
	{
		Check.ArgumentNull(element);
		Check.ArgumentNull(name);

		return element.Attribute(name) ?? new(name, defaultValue);
	}
}