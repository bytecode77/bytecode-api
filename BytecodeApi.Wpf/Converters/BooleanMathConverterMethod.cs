namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Specifies the method that is used to perform boolean calculations on the value and parameter.
/// </summary>
public enum BooleanMathConverterMethod
{
	/// <summary>
	/// Performs a boolean and operation on the value and parameter:
	/// <para>value &amp;&amp; parameter</para>
	/// </summary>
	And,
	/// <summary>
	/// Performs a boolean or operation on the value and parameter:
	/// <para>value || parameter</para>
	/// </summary>
	Or,
	/// <summary>
	/// Performs a boolean xor operation on the value and parameter:
	/// <para>value ^ parameter</para>
	/// </summary>
	Xor
}