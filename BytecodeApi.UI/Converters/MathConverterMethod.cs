namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Specifies the method that is used to perform mathematical calculations on the value and parameter.
	/// </summary>
	public enum MathConverterMethod
	{
		/// <summary>
		/// Adds the value and parameter:
		/// <para>value + parameter</para>
		/// </summary>
		Add,
		/// <summary>
		/// Subtracts the parameter from value:
		/// <para>value - parameter</para>
		/// </summary>
		Subtract,
		/// <summary>
		/// Multiplies the value and parameter:
		/// <para>value * parameter</para>
		/// </summary>
		Multiply,
		/// <summary>
		/// Divides the value by parameter:
		/// <para>value / parameter</para>
		/// </summary>
		Divide,
		/// <summary>
		/// Divides the value by parameter and returns the remainder:
		/// <para>value % parameter</para>
		/// </summary>
		Modulo,
		/// <summary>
		/// Performs a binary and operation on the value and parameter:
		/// <para>value &amp; parameter</para>
		/// </summary>
		And,
		/// <summary>
		/// Performs a binary or operation on the value and parameter:
		/// <para>value | parameter</para>
		/// </summary>
		Or,
		/// <summary>
		/// Performs a binary xor operation on the value and parameter:
		/// <para>value ^ parameter</para>
		/// </summary>
		Xor
		//CURRENT: ~ << >>
	}
}