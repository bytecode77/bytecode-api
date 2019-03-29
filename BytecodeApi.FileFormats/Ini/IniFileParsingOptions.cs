namespace BytecodeApi.FileFormats.Ini
{
	//FEATURE: IniFileParsingOptions: bool AllowCommentsAfterContent, implement escape characters, Quoted values (single & double quotes)
	/// <summary>
	/// Represents a set of parsing options for INI files to support the common feature variations.
	/// </summary>
	public sealed class IniFileParsingOptions
	{
		/// <summary>
		/// <see langword="true" /> to ignore parsing errors and add error lines to <see cref="IniFile.ErrorLines" />; <see langword="false" /> to throw an <see cref="IniParsingException" />.
		/// <para>The default value is <see langword="false" />.</para>
		/// </summary>
		public bool IgnoreErrors { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether section names should be trimmed.
		/// <para>The default value is <see langword="true" />.</para>
		/// </summary>
		public bool TrimSectionNames { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether property names should be trimmed.
		/// <para>The default value is <see langword="true" />.</para>
		/// </summary>
		public bool TrimPropertyNames { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether property values should be trimmed.
		/// <para>The default value is <see langword="true" />.</para>
		/// </summary>
		public bool TrimPropertyValues { get; set; }
		/// <summary>
		/// <see langword="true" /> to allow properties with no section and add them to <see cref="IniFile.GlobalProperties" />; <see langword="false" /> to throw an <see cref="IniParsingException" />.
		/// <para>The default value is <see langword="true" />.</para>
		/// </summary>
		public bool AllowGlobalProperties { get; set; }
		/// <summary>
		/// <see langword="true" /> to allow section names containing closing brackets ("]"); <see langword="false" /> to throw an <see cref="IniParsingException" />.
		/// <para>The default value is <see langword="true" />.</para>
		/// </summary>
		public bool AllowSectionNameClosingBracket { get; set; }
		/// <summary>
		/// <see langword="true" /> to allow empty or whitespace lines; <see langword="false" /> to throw an <see cref="IniParsingException" />.
		/// <para>The default value is <see langword="true" />.</para>
		/// </summary>
		public bool AllowEmptyLines { get; set; }
		/// <summary>
		/// Gets or sets a value specifying what delimiter is used between property names and values.
		/// <para>The default value is <see cref="IniPropertyDelimiter.EqualSign" />.</para>
		/// </summary>
		public IniPropertyDelimiter PropertyDelimiter { get; set; }
		/// <summary>
		/// <see langword="true" /> to ignore lines that start with a semicolon; <see langword="false" /> to treat it as a normal character, which will be included in the property name.
		/// <para>The default value is <see langword="true" />.</para>
		/// </summary>
		public bool AllowSemicolonComments { get; set; }
		/// <summary>
		/// <see langword="true" /> to ignore lines that start with a number sign ("#"); <see langword="false" /> to treat it as a normal character, which will be included in the property name.
		/// <para>The default value is <see langword="false" />.</para>
		/// </summary>
		public bool AllowNumberSignComments { get; set; }
		/// <summary>
		/// Gets or sets how duplicate section names are treated.
		/// <para>The default value is <see cref="IniDuplicateSectionNameBehavior.Merge" />.</para>
		/// </summary>
		public IniDuplicateSectionNameBehavior DuplicateSectionNameBehavior { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether duplicate checking in section names should ignore character casing. The property <see cref="DuplicateSectionNameBehavior" /> defines how duplicates are treated.
		/// <para>The default value is <see langword="true" />.</para>
		/// </summary>
		public bool DuplicateSectionNameIgnoreCase { get; set; }
		/// <summary>
		/// Gets or sets how duplicate property names are treated.
		/// <para>The default value is <see cref="IniDuplicatePropertyNameBehavior.Duplicate" />.</para>
		/// </summary>
		public IniDuplicatePropertyNameBehavior DuplicatePropertyNameBehavior { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether duplicate checking in property names should ignore character casing. The property <see cref="DuplicatePropertyNameBehavior" /> defines how duplicates are treated.
		/// <para>The default value is <see langword="true" />.</para>
		/// </summary>
		public bool DuplicatePropertyNameIgnoreCase { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="IniFileParsingOptions" /> class with default parsing options.
		/// </summary>
		public IniFileParsingOptions()
		{
			TrimSectionNames = true;
			TrimPropertyNames = true;
			TrimPropertyValues = true;
			AllowGlobalProperties = true;
			AllowSectionNameClosingBracket = true;
			AllowEmptyLines = true;
			PropertyDelimiter = IniPropertyDelimiter.EqualSign;
			AllowSemicolonComments = true;
			DuplicateSectionNameBehavior = IniDuplicateSectionNameBehavior.Merge;
			DuplicateSectionNameIgnoreCase = true;
			DuplicatePropertyNameBehavior = IniDuplicatePropertyNameBehavior.Duplicate;
			DuplicatePropertyNameIgnoreCase = true;
		}
	}
}