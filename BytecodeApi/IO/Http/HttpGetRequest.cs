using System.Net;

namespace BytecodeApi.IO.Http
{
	/// <summary>
	/// Represents a HTTP GET requests.
	/// </summary>
	public sealed class HttpGetRequest : HttpRequest
	{
		internal HttpGetRequest(HttpClient client, string url) : base(client, url)
		{
		}

		/// <summary>
		/// Adds an HTTP GET parameter with the specified name and value to the HTTP GET request.
		/// </summary>
		/// <param name="name">The name of the GET parameter.</param>
		/// <param name="value">The value of the GET parameter.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public HttpGetRequest AddQueryParameter(string name, string value)
		{
			QueryParameters.Add(new HttpParameter(name, value));
			return this;
		}

		private protected override HttpWebRequest GetWebRequest()
		{
			HttpWebRequest request = CreateRequest(WebRequestMethods.Http.Get);
			OnWebRequestCreated(request);
			return request;
		}
	}
}