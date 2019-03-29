namespace BytecodeApi
{
	/// <summary>
	/// Specifies the subset of a <see cref="System.DateTime" /> representation.
	/// </summary>
	public enum DateTimePart
	{
		/// <summary>
		/// The full <see cref="System.DateTime" /> is used.
		/// </summary>
		Full,
		/// <summary>
		/// The date and time including seconds of the <see cref="System.DateTime" /> object is used.
		/// </summary>
		DateTimeWithSeconds,
		/// <summary>
		/// The date and time excluding seconds of the <see cref="System.DateTime" /> object is used.
		/// </summary>
		DateTime,
		/// <summary>
		/// The date part excluding time information of the <see cref="System.DateTime" /> object is used. This is equal to the <see cref="System.DateTime.Date" /> property.
		/// </summary>
		Date,
		/// <summary>
		/// The year and month of the <see cref="System.DateTime" /> object is used. <see cref="System.DateTime.Day" /> is set to 1.
		/// </summary>
		YearMonth,
		/// <summary>
		/// The year is used. <see cref="System.DateTime.Month" /> and <see cref="System.DateTime.Day" /> are both set to 1.
		/// </summary>
		Year
	}
}