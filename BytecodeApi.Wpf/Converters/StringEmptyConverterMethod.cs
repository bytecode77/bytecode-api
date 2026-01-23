namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Specifies the method that is used to check <see cref="string" /> values for emptiness.
/// </summary>
public enum StringEmptyConverterMethod
{
	/// <summary>
	/// Checks the <see cref="string" /> value for emptiness and returns <see langword="true" />, if the <see cref="string" /> is not <see langword="null" /> and not <see cref="string.Empty" />; otherwise, <see langword="false" />.
	/// </summary>
	NotNullOrEmpty,
	/// <summary>
	/// Checks the <see cref="string" /> value for emptiness and returns <see langword="true" />, if the <see cref="string" /> is not <see langword="null" />, not <see cref="string.Empty" />, and does not consist only of white-space characters; otherwise, <see langword="false" />.
	/// </summary>
	NotNullOrWhiteSpace
}