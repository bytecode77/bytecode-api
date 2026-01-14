namespace BytecodeApi.Rest;

/// <summary>
/// Represents a set of options for REST requests.
/// </summary>
public sealed class RestRequestOptions
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
	/// Initializes a new instance of the <see cref="RestRequestOptions" /> class with default options.
	/// </summary>
	public RestRequestOptions()
	{
		QueryParameterDateTimeFormat = "yyyy-MM-ddTHH:mm:sszzz";
		QueryParameterDateOnlyFormat = "yyyy-MM-dd";
		QueryParameterTimeOnlyFormat = "HH:mm:ss";
	}
}