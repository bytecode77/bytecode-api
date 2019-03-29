using BytecodeApi.Extensions;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace BytecodeApi.IO
{
	/// <summary>
	/// The exception that is thrown when a <see cref="WebRequest" /> instance fails to process a request. This exception is used by methods of <see cref="WebRequestExtensions" />.
	/// </summary>
	public sealed class WebRequestException : Exception
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
		/// Initializes a new instance of the <see cref="WebRequestException" /> class.
		/// </summary>
		/// <param name="status">The status of the response.</param>
		/// <param name="response">The response that the remote host returned.</param>
		/// <param name="responseBody">The response body.</param>
		/// <param name="innerException">A nested <see cref="Exception" />.</param>
		public WebRequestException(WebExceptionStatus status, WebResponse response, string responseBody, Exception innerException) : base("The web request failed.", innerException)
		{
			Status = status;
			Response = response;
			ResponseBody = responseBody;
		}

		[DebuggerStepThrough]
		internal static T Try<T>(Func<T> func)
		{
			try
			{
				return func();
			}
			catch (WebException ex)
			{
				string htmlBody = null;

				if (ex.Response != null)
				{
					try
					{
						using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
						{
							htmlBody = reader.ReadToEnd();
						}
					}
					catch { }
				}

				throw new WebRequestException(ex.Status, ex.Response, htmlBody, ex);
			}
		}
	}
}