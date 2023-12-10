﻿using BytecodeApi.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace BytecodeApi.IniParser;

/// <summary>
/// Represents an INI file.
/// </summary>
public class IniFile
{
	/// <summary>
	/// Gets the <see cref="IniSection" /> containing all properties prior to the first section declaration associated with this INI file.
	/// </summary>
	public IniSection GlobalProperties { get; private init; }
	/// <summary>
	/// Gets the collection of INI sections associated with this INI file.
	/// </summary>
	public IniSectionCollection Sections { get; private init; }
	/// <summary>
	/// Gets the collection of lines that could not be parsed. This collection popuplated, if <see cref="IniFileParsingOptions.IgnoreErrors" /> is set to <see langword="true" />.
	/// </summary>
	public ReadOnlyCollection<IniErrorLine> ErrorLines { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="IniFile" /> class.
	/// </summary>
	public IniFile()
	{
		GlobalProperties = new(null);
		Sections = new();
		ErrorLines = Array.Empty<IniErrorLine>().ToReadOnlyCollection();
	}
	/// <summary>
	/// Creates an <see cref="IniFile" /> object from the specified file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to an INI file.</param>
	/// <returns>
	/// The <see cref="IniFile" /> this method creates.
	/// </returns>
	public static IniFile FromFile(string path)
	{
		return FromFile(path, Encoding.UTF8);
	}
	/// <summary>
	/// Creates an <see cref="IniFile" /> object from the specified file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to an INI file.</param>
	/// <param name="encoding">The encoding to use to read the file.</param>
	/// <returns>
	/// The <see cref="IniFile" /> this method creates.
	/// </returns>
	public static IniFile FromFile(string path, Encoding? encoding)
	{
		return FromFile(path, encoding, null);
	}
	/// <summary>
	/// Creates an <see cref="IniFile" /> object from the specified file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to an INI file.</param>
	/// <param name="encoding">The encoding to use to read the file.</param>
	/// <param name="parsingOptions">A <see cref="IniFileParsingOptions" /> object with format specifications for INI parsing, or <see langword="null" /> to use default parsing options.</param>
	/// <returns>
	/// The <see cref="IniFile" /> this method creates.
	/// </returns>
	public static IniFile FromFile(string path, Encoding? encoding, IniFileParsingOptions? parsingOptions)
	{
		Check.ArgumentNull(path);
		Check.FileNotFound(path);

		using FileStream file = File.OpenRead(path);
		return FromStream(file, encoding, parsingOptions);
	}
	/// <summary>
	/// Creates an <see cref="IniFile" /> object from the specified <see cref="byte" />[] that represents an INI file.
	/// </summary>
	/// <param name="file">The <see cref="byte" />[] that represents an INI file to parse.</param>
	/// <returns>
	/// The <see cref="IniFile" /> this method creates.
	/// </returns>
	public static IniFile FromBinary(byte[] file)
	{
		return FromBinary(file, Encoding.UTF8);
	}
	/// <summary>
	/// Creates an <see cref="IniFile" /> object from the specified <see cref="byte" />[] that represents an INI file.
	/// </summary>
	/// <param name="file">The <see cref="byte" />[] that represents an INI file to parse.</param>
	/// <param name="encoding">The encoding to use to read the file.</param>
	/// <returns>
	/// The <see cref="IniFile" /> this method creates.
	/// </returns>
	public static IniFile FromBinary(byte[] file, Encoding? encoding)
	{
		return FromBinary(file, encoding, null);
	}
	/// <summary>
	/// Creates an <see cref="IniFile" /> object from the specified <see cref="byte" />[] that represents an INI file.
	/// </summary>
	/// <param name="file">The <see cref="byte" />[] that represents an INI file to parse.</param>
	/// <param name="encoding">The encoding to use to read the file.</param>
	/// <param name="parsingOptions">A <see cref="IniFileParsingOptions" /> object with format specifications for INI parsing, or <see langword="null" /> to use default parsing options.</param>
	/// <returns>
	/// The <see cref="IniFile" /> this method creates.
	/// </returns>
	public static IniFile FromBinary(byte[] file, Encoding? encoding, IniFileParsingOptions? parsingOptions)
	{
		Check.ArgumentNull(file);

		using MemoryStream memoryStream = new(file);
		return FromStream(memoryStream, encoding, parsingOptions);
	}
	/// <summary>
	/// Creates an <see cref="IniFile" /> object from the specified <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> from which to parse the INI file from.</param>
	/// <returns>
	/// The <see cref="IniFile" /> this method creates.
	/// </returns>
	public static IniFile FromStream(Stream stream)
	{
		return FromStream(stream, Encoding.UTF8);
	}
	/// <summary>
	/// Creates an <see cref="IniFile" /> object from the specified <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> from which to parse the INI file from.</param>
	/// <param name="encoding">The encoding to use to read the file.</param>
	/// <returns>
	/// The <see cref="IniFile" /> this method creates.
	/// </returns>
	public static IniFile FromStream(Stream stream, Encoding? encoding)
	{
		return FromStream(stream, encoding, null);
	}
	/// <summary>
	/// Creates an <see cref="IniFile" /> object from the specified <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> from which to parse the INI file from.</param>
	/// <param name="encoding">The encoding to use to read the file.</param>
	/// <param name="parsingOptions">A <see cref="IniFileParsingOptions" /> object with format specifications for INI parsing, or <see langword="null" /> to use default parsing options.</param>
	/// <returns>
	/// The <see cref="IniFile" /> this method creates.
	/// </returns>
	public static IniFile FromStream(Stream stream, Encoding? encoding, IniFileParsingOptions? parsingOptions)
	{
		return FromStream(stream, encoding, parsingOptions, false);
	}
	/// <summary>
	/// Creates an <see cref="IniFile" /> object from the specified <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> from which to parse the INI file from.</param>
	/// <param name="encoding">The encoding to use to read the file.</param>
	/// <param name="parsingOptions">A <see cref="IniFileParsingOptions" /> object with format specifications for INI parsing, or <see langword="null" /> to use default parsing options.</param>
	/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
	/// <returns>
	/// The <see cref="IniFile" /> this method creates.
	/// </returns>
	public static IniFile FromStream(Stream stream, Encoding? encoding, IniFileParsingOptions? parsingOptions, bool leaveOpen)
	{
		Check.ArgumentNull(stream);
		Check.ArgumentNull(encoding);

		parsingOptions ??= new();

		IniFile ini = new();
		IniSection section = ini.GlobalProperties;
		bool ignoreSection = false;
		List<IniErrorLine> errorLines = new();

		using StreamReader reader = new(stream, encoding, true, -1, leaveOpen);

		for (int lineNumber = 1; !reader.EndOfStream; lineNumber++)
		{
			string line = reader.ReadLine() ?? "";
			string trimmedLine = line.Trim();

			if (trimmedLine.StartsWith(';') && parsingOptions.AllowSemicolonComments || trimmedLine.StartsWith('#') && parsingOptions.AllowNumberSignComments)
			{
			}
			else if (trimmedLine == "")
			{
				AbortIf(!parsingOptions.AllowEmptyLines);
			}
			else if (trimmedLine.StartsWith('[') && trimmedLine.EndsWith(']'))
			{
				string newSection = trimmedLine[1..^1];
				if (parsingOptions.TrimSectionNames)
				{
					newSection = newSection.Trim();
				}

				AbortIf(!parsingOptions.AllowSectionNameClosingBracket && newSection.Contains(']'));

				IniSection? duplicate = ini.Sections.FirstOrDefault(sect => sect.Name?.Equals(newSection, parsingOptions.DuplicateSectionNameIgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == true);
				bool create = false;

				switch (parsingOptions.DuplicateSectionNameBehavior)
				{
					case IniDuplicateSectionNameBehavior.Abort:
						AbortIf(duplicate != null);
						create = true;
						break;
					case IniDuplicateSectionNameBehavior.Ignore:
						if (duplicate == null)
						{
							create = true;
						}
						else
						{
							ignoreSection = true;
						}
						break;
					case IniDuplicateSectionNameBehavior.Merge:
						if (duplicate == null)
						{
							create = true;
						}
						else
						{
							section = duplicate;
						}
						break;
					case IniDuplicateSectionNameBehavior.Duplicate:
						create = true;
						break;
					default:
						throw Throw.InvalidEnumArgument(nameof(parsingOptions.DuplicatePropertyNameBehavior), parsingOptions.DuplicatePropertyNameBehavior);
				}

				if (create)
				{
					section = new(newSection);
					ini.Sections.Add(section);
				}
			}
			else if (parsingOptions.PropertyDelimiter == IniPropertyDelimiter.EqualSign && line.Contains('='))
			{
				ParseProperty("=");
			}
			else if (parsingOptions.PropertyDelimiter == IniPropertyDelimiter.Colon && line.Contains(':'))
			{
				ParseProperty(":");
			}
			else
			{
				Abort();
			}

			void Abort()
			{
				if (parsingOptions.IgnoreErrors)
				{
					errorLines.Add(new(lineNumber, line));
				}
				else
				{
					throw new IniParsingException(lineNumber, line);
				}
			}
			void AbortIf(bool condition)
			{
				if (condition)
				{
					Abort();
				}
			}
			void ParseProperty(string delimiter)
			{
				if (!ignoreSection)
				{
					AbortIf(!parsingOptions.AllowGlobalProperties && section == ini.GlobalProperties);

					IniProperty property = new(line.SubstringUntil(delimiter), line.SubstringFrom(delimiter));

					if (parsingOptions.TrimPropertyNames)
					{
						property.Name = property.Name.Trim();
					}

					if (parsingOptions.TrimPropertyValues)
					{
						property.Value = property.Value.Trim();
					}

					if (section.Properties.FirstOrDefault(prop => prop.Name.Equals(property.Name, parsingOptions.DuplicatePropertyNameIgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)) is IniProperty duplicate)
					{
						switch (parsingOptions.DuplicatePropertyNameBehavior)
						{
							case IniDuplicatePropertyNameBehavior.Abort:
								Abort();
								break;
							case IniDuplicatePropertyNameBehavior.Ignore:
								break;
							case IniDuplicatePropertyNameBehavior.Overwrite:
								duplicate.Value = property.Value;
								break;
							case IniDuplicatePropertyNameBehavior.Duplicate:
								section.Properties.Add(property);
								break;
							default:
								throw Throw.InvalidEnumArgument(nameof(parsingOptions.DuplicatePropertyNameBehavior), parsingOptions.DuplicatePropertyNameBehavior);
						}
					}
					else
					{
						section.Properties.Add(property);
					}
				}
			}
		}

		ini.ErrorLines = errorLines.AsReadOnly();
		return ini;
	}

	/// <summary>
	/// Retrieves an <see cref="IniSection" /> with the specified name.
	/// </summary>
	/// <param name="name">A <see cref="string" /> specifying the name of the <see cref="IniSection" />.</param>
	/// <returns>
	/// The <see cref="IniSection" /> with the specified name, or <see langword="null" /> if no matching section was found.
	/// </returns>
	public IniSection? Section(string name)
	{
		return Section(name, false);
	}
	/// <summary>
	/// Retrieves an <see cref="IniSection" /> with the specified name.
	/// </summary>
	/// <param name="name">A <see cref="string" /> specifying the name of the <see cref="IniSection" />.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during name comparison.</param>
	/// <returns>
	/// The <see cref="IniSection" /> with the specified name, or <see langword="null" /> if no matching section was found.
	/// </returns>
	public IniSection? Section(string name, bool ignoreCase)
	{
		Check.ArgumentNull(name);

		return Sections.FirstOrDefault(section => section.Name?.Equals(name, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == true);
	}

	/// <summary>
	/// Writes the contents of this INI file to a file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying the path of a file to which this INI file is written to.</param>
	public void Save(string path)
	{
		Save(path, Encoding.UTF8);
	}
	/// <summary>
	/// Writes the contents of this INI file to a file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying the path of a file to which this INI file is written to.</param>
	/// <param name="encoding">The encoding to use to write to the file.</param>
	public void Save(string path, Encoding encoding)
	{
		Save(path, encoding, null);
	}
	/// <summary>
	/// Writes the contents of this INI file to a file.
	/// </summary>
	/// <param name="path">A <see cref="string" /> specifying the path of a file to which this INI file is written to.</param>
	/// <param name="encoding">The encoding to use to write to the file.</param>
	/// <param name="formattingOptions">An <see cref="IniFileFormattingOptions" /> object specifying how to format the INI file.</param>
	public void Save(string path, Encoding encoding, IniFileFormattingOptions? formattingOptions)
	{
		Check.ArgumentNull(path);
		Check.ArgumentNull(encoding);

		using FileStream file = File.Create(path);
		Save(file, encoding, formattingOptions, false);
	}
	/// <summary>
	/// Writes the contents of this INI file to a <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> to which this INI file is written to.</param>
	public void Save(Stream stream)
	{
		Save(stream, Encoding.UTF8);
	}
	/// <summary>
	/// Writes the contents of this INI file to a <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> to which this INI file is written to.</param>
	/// <param name="encoding">The encoding to use to write to the file.</param>
	public void Save(Stream stream, Encoding encoding)
	{
		Save(stream, encoding, null);
	}
	/// <summary>
	/// Writes the contents of this INI file to a <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> to which this INI file is written to.</param>
	/// <param name="encoding">The encoding to use to write to the file.</param>
	/// <param name="formattingOptions">An <see cref="IniFileFormattingOptions" /> object specifying how to format the INI file.</param>
	public void Save(Stream stream, Encoding encoding, IniFileFormattingOptions? formattingOptions)
	{
		Save(stream, encoding, formattingOptions, false);
	}
	/// <summary>
	/// Writes the contents of this INI file to a <see cref="Stream" />.
	/// </summary>
	/// <param name="stream">The <see cref="Stream" /> to which this INI file is written to.</param>
	/// <param name="encoding">The encoding to use to write to the file.</param>
	/// <param name="formattingOptions">An <see cref="IniFileFormattingOptions" /> object specifying how to format the INI file.</param>
	/// <param name="leaveOpen">A <see cref="bool" /> value indicating whether to leave <paramref name="stream" /> open.</param>
	public void Save(Stream stream, Encoding encoding, IniFileFormattingOptions? formattingOptions, bool leaveOpen)
	{
		Check.ArgumentNull(stream);
		Check.ArgumentNull(encoding);

		formattingOptions ??= new();

		string delimiter =
			(formattingOptions.UseDelimiterSpaceBefore ? " " : null) +
			formattingOptions.PropertyDelimiter.GetDescription() +
			(formattingOptions.UseDelimiterSpaceAfter ? " " : null);

		using StreamWriter streamWriter = new(stream, encoding, -1, leaveOpen);

		if (GlobalProperties.Properties.Count > 0)
		{
			WriteSection(GlobalProperties);

			if (formattingOptions.UseNewLineBetweenSections)
			{
				streamWriter.WriteLine();
			}
		}

		for (int i = 0; i < Sections.Count; i++)
		{
			WriteSection(Sections[i]);

			if (i < Sections.Count - 1 && formattingOptions.UseNewLineBetweenSections)
			{
				streamWriter.WriteLine();
			}
		}

		void WriteSection(IniSection section)
		{
			if (section.Name != null)
			{
				streamWriter.WriteLine($"[{section.Name}]");
			}

			foreach (IniProperty property in section.Properties)
			{
				streamWriter.WriteLine(property.Name + delimiter + property.Value);
			}
		}
	}
}