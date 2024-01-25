using System.Net;

namespace BytecodeApi.Rest;

/// <summary>
/// Represents an error that was raised by a REST API.
/// </summary>
public sealed class RestException : Exception
{
	/// <summary>
	/// Gets the response status code that is associated with this <see cref="RestException" />.
	/// </summary>
	public HttpStatusCode StatusCode { get; private init; }
	/// <summary>
	/// Gets the HTTP body that is associated with this <see cref="RestException" />.
	/// </summary>
	public string Content { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="RestException" /> class.
	/// </summary>
	/// <param name="statusCode">The error code that is associated with this <see cref="RestException" />.</param>
	/// <param name="content">The HTTP body that is associated with this <see cref="RestException" />.</param>
	public RestException(HttpStatusCode statusCode, string content) : base("The REST response was not successful.")
	{
		Check.ArgumentNull(content);

		StatusCode = statusCode;
		Content = content;
	}
}