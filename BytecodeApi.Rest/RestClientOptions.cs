namespace BytecodeApi.Rest;

/// <summary>
/// Represents a set of formatting options for a <see cref="RestClient" /> instance.
/// </summary>
public sealed class RestClientOptions
{
	/// <summary>
	/// Gets or sets a value specifying the format that is used to convert <see cref="DateTime" /> values.
	/// <para>The default value is "yyyy-MM-ddTHH:mm:sszzz".</para>
	/// </summary>
	public string QueryParameterDateTimeFormat { get; set; }
	/// <summary>
	/// Gets or sets a value specifying the format that is used to convert <see cref="DateOnly" /> values.
	/// <para>The default value is "yyyy-MM-dd".</para>
	/// </summary>
	public string QueryParameterDateOnlyFormat { get; set; }
	/// <summary>
	/// Gets or sets a value specifying the format that is used to convert <see cref="TimeOnly" /> values.
	/// <para>The default value is "HH:mm:ss".</para>
	/// </summary>
	public string QueryParameterTimeOnlyFormat { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="RestClientOptions" /> class with default formatting options.
	/// </summary>
	public RestClientOptions()
	{
		QueryParameterDateTimeFormat = "yyyy-MM-ddTHH:mm:sszzz";
		QueryParameterDateOnlyFormat = "yyyy-MM-dd";
		QueryParameterTimeOnlyFormat = "HH:mm:ss";
	}
}