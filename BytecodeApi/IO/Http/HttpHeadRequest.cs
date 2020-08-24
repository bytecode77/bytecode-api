using System.Net;

namespace BytecodeApi.IO.Http
{
	/// <summary>
	/// Represents a HTTP HEAD requests.
	/// </summary>
	public sealed class HttpHeadRequest : HttpRequest
	{
		internal HttpHeadRequest(HttpClient client, string url) : base(client, url)
		{
		}

		/// <summary>
		/// Adds a query parameter with the specified name and value to the HTTP HEAD request.
		/// </summary>
		/// <param name="name">The name of the query parameter.</param>
		/// <param name="value">The value of the query parameter.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public HttpHeadRequest AddQueryParameter(string name, string value)
		{
			QueryParameters.Add(new HttpParameter(name, value));
			return this;
		}
		/// <summary>
		/// Gets the response headers of this HTTP HEAD request.
		/// </summary>
		/// <returns>
		/// A new <see cref="HttpResponseHeaders" /> object with the response headers.
		/// </returns>
		public HttpResponseHeaders GetResponse()
		{
			return Try(() =>
			{
				using (HttpWebResponse response = (HttpWebResponse)GetWebRequest().GetResponse())
				{
					return new HttpResponseHeaders
					{
						SupportsHeaders = response.SupportsHeaders,
						IsFromCache = response.IsFromCache,
						IsMutuallyAuthenticated = response.IsMutuallyAuthenticated,
						ResponseUri = response.ResponseUri,
						ProtocolVersion = response.ProtocolVersion,
						StatusDescription = response.StatusDescription,
						StatusCode = response.StatusCode,
						ContentType = response.ContentType,
						ContentLength = response.ContentLength,
						LastModified = response.LastModified,
						Server = response.Server,
						CharacterSet = response.CharacterSet,
						ContentEncoding = response.ContentEncoding,
						Headers = response.Headers
					};
				}
			});
		}

		private protected override HttpWebRequest GetWebRequest()
		{
			HttpWebRequest request = CreateRequest(WebRequestMethods.Http.Head);
			OnWebRequestCreated(request);
			return request;
		}
	}
}