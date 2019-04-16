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
		/// The <see cref="string" /> is camel case, where the first character and each character following a whitespace or punctuation mark is uppercase. All other characters are lowercase.
		/// <para>Example: Hello World</para>
		/// </summary>
		CamelCase,
		/// <summary>
		/// The <see cref="string" /> is snake case. All characters are lowercase. Punctuation marks are removed and sequences of one or multiple whitespace characters are replaced by a single underscore.
		/// <para>Example: hello_world</para>
		/// </summary>
		LowerSnakeCase,
		/// <summary>
		/// The <see cref="string" /> is snake case. All characters are uppercase. Punctuation marks are removed and sequences of one or multiple whitespace characters are replaced by a single underscore.
		/// <para>Example: HELLO_WORLD</para>
		/// </summary>
		UpperSnakeCase,
		/// <summary>
		/// The <see cref="string" /> is kebab case. All characters are lowercase. Punctuation marks are removed and sequences of one or multiple whitespace characters are replaced by a single dash.
		/// <para>Example: hello-world</para>
		/// </summary>
		LowerKebabCase,
		/// <summary>
		/// The <see cref="string" /> is kebab case. All characters are uppercase. Punctuation marks are removed and sequences of one or multiple whitespace characters are replaced by a single dash.
		/// <para>Example: HELLO-WORLD</para>
		/// </summary>
		UpperKebabCase
	}
}