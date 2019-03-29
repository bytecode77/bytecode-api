namespace BytecodeApi.Text
{
	/// <summary>
	/// Specifies the display format for byte size values to be used in <see cref="string" /> formatting logic.
	/// </summary>
	public enum ByteSizeFormat
	{
		/// <summary>
		/// Specifies that the closest unit (Bytes, KB, MB, GB, TB, ...) is displayed. The result is rounded with no decimal places.
		/// <para>Example: 12345 is converted to "12 KB"</para>
		/// </summary>
		ByteSize,
		/// <summary>
		/// Specifies that the closest unit (Bytes, KB, MB, GB, TB, ...) is displayed. The result is rounded with two decimal places.
		/// <para>Example: 12345 is converted to "12,06 KB"</para>
		/// </summary>
		ByteSizeTwoDigits,
		/// <summary>
		/// Specifies that the exact amount of bytes is displayed. A thousands separator is displayed as a dot.
		/// <para>Example: 12345 is converted to "12.345 bytes"</para>
		/// </summary>
		Bytes,
		/// <summary>
		/// Specifies that the value is displayed as kilobytes. The result is rounded up. A thousands separator is displayed as a dot.
		/// <para>Example: 12345 is converted to "13 KB"</para>
		/// </summary>
		KiloBytes,
		/// <summary>
		/// Specifies that the value is displayed as kilobytes. The result is rounded with two decimal places. A thousands separator is displayed as a dot.
		/// <para>Example: 12345 is converted to "13 KB"</para>
		/// </summary>
		KiloBytesTwoDigits
	}
}