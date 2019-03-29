namespace BytecodeApi.FileFormats.Ini
{
	/// <summary>
	/// Represents a set of formatting options for INI files that specify how an INI file is exported when using <see cref="IniFile.Save(string)" />.
	/// </summary>
	public sealed class IniFileFormattingOptions
	{
		/// <summary>
		/// Gets or sets a value specifying what delimiter is used between property names and values.
		/// <para>The default value is <see cref="IniPropertyDelimiter.EqualSign" />.</para>
		/// </summary>
		public IniPropertyDelimiter PropertyDelimiter { get; set; }
		/// <summary>
		/// <see langword="true" /> to use a space before the delimiter; <see langword="false" /> to append the delimiter directly after the property name.
		/// <para>The default value is <see langword="true" />.</para>
		/// </summary>
		public bool UseDelimiterSpaceBefore { get; set; }
		/// <summary>
		/// <see langword="true" /> to use a space after the delimiter; <see langword="false" /> to append the property value directly after the delimiter.
		/// <para>The default value is <see langword="true" />.</para>
		/// </summary>
		public bool UseDelimiterSpaceAfter { get; set; }
		/// <summary>
		/// <see langword="true" /> to add a linebreak between each section; otherwise, <see langword="false" />.
		/// <para>The default value is <see langword="true" />.</para>
		/// </summary>
		public bool UseNewLineBetweenSections { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="IniFileFormattingOptions" /> class with default formatting options.
		/// </summary>
		public IniFileFormattingOptions()
		{
			PropertyDelimiter = IniPropertyDelimiter.EqualSign;
			UseDelimiterSpaceBefore = true;
			UseDelimiterSpaceAfter = true;
			UseNewLineBetweenSections = true;
		}
	}
}