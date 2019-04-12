namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Specifies the method that is used to check <see cref="string" /> values for emptiness.
	/// </summary>
	public enum StringEmptyConverterMethod
	{
		/// <summary>
		/// Checks the <see cref="string" /> value for emptiness and returns <see langword="true" />, if the <see cref="string" /> is <see langword="null" /> or <see cref="string.Empty" />; otherwise, <see langword="false" />.
		/// </summary>
		NotNullOrEmpty,
		/// <summary>
		/// Checks the <see cref="string" /> value for emptiness and returns <see langword="true" />, if the <see cref="string" /> is <see langword="null" />, <see cref="string.Empty" />, or consists only of white-space characters; otherwise, <see langword="false" />.
		/// </summary>
		NotNullOrWhiteSpace
	}
}