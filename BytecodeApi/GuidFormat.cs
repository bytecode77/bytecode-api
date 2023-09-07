namespace BytecodeApi;

/// <summary>
/// Specifies the format for a <see cref="Guid" />.
/// </summary>
public enum GuidFormat
{
	/// <summary>
	/// The <see cref="Guid" /> is displayed using 32 hexadecimal digits.
	/// <para>Example: 00000000000000000000000000000000</para>
	/// </summary>
	Default = 'N',
	/// <summary>
	/// The <see cref="Guid" /> is displayed using 32 hexadecimal digits separated by hyphens.
	/// <para>Example: 00000000-0000-0000-0000-000000000000</para>
	/// </summary>
	Hyphens = 'D',
	/// <summary>
	/// The <see cref="Guid" /> is displayed using 32 hexadecimal digits separated by hyphens, enclosed in braces.
	/// <para>Example: {00000000-0000-0000-0000-000000000000}</para>
	/// </summary>
	Braces = 'B',
	/// <summary>
	/// The <see cref="Guid" /> is displayed using 32 hexadecimal digits separated by hyphens, enclosed in parentheses.
	/// <para>Example: (00000000-0000-0000-0000-000000000000)</para>
	/// </summary>
	Parentheses = 'P',
	/// <summary>
	/// The <see cref="Guid" /> is displayed with four hexadecimal values enclosed in braces, where the fourth value is a subset of eight hexadecimal values that is also enclosed in braces.
	/// <para>Example: {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}</para>
	/// </summary>
	Hexadecimal = 'X'
}