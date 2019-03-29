namespace BytecodeApi
{
	/// <summary>
	/// Specifies the character casing of a <see cref="string" />.
	/// </summary>
	public enum StringCasing
	{
		/// <summary>
		/// The <see cref="string" /> is lowercase.
		/// <para>Example: hello world</para>
		/// </summary>
		Lower,
		/// <summary>
		/// The <see cref="string" /> is uppercase.
		/// <para>Example: HELLO WORLD</para>
		/// </summary>
		Upper,
		/// <summary>
		/// The <see cref="string" /> is camelcase, where the first character and each character following a whitespace or puncuation mark is uppercase. All other characters are lowercase.
		/// <para>Example: Hello World</para>
		/// </summary>
		CamelCase
	}
}