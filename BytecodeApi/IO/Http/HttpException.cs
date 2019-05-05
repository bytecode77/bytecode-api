using BytecodeApi.IO.Http;
using System;
using System.Net;

namespace BytecodeApi.IO
{
	/// <summary>
	/// The exception that is thrown when a <see cref="HttpRequest" /> instance fails to process a request.
	/// </summary>
	public sealed class HttpException : Exception
	{
		/// <summary>
		/// Gets the status of the response.
		/// </summary>
		public WebExceptionStatus Status { get; private set; }
		/// <summary>
		/// Gets the response that the remote host returned.
		/// </summary>
		public WebResponse Response { get; private set; }
		/// <summary>
		/// Gets the response body, typically an HTML error page.
		/// </summary>
		public string ResponseBody { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpException" /> class.
		/// </summary>
		/// <param name="status">The status of the response.</param>
		/// <param name="response">The response that the remote host returned.</param>
		/// <param name="responseBody">The response body.</param>
		/// <param name="innerException">A nested <see cref="Exception" />.</param>
		public HttpException(WebExceptionStatus status, WebResponse response, string responseBody, Exception innerException) : base("The web request failed.", innerException)
		{
			Status = status;
			Response = response;
			ResponseBody = responseBody;
		}
	}
}