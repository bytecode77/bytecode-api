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
		And,
		Or,
		Xor
		//CURRENT: ~ << >>
	}
}