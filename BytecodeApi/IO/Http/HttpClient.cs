using System.Net;

namespace BytecodeApi.IO.Http
{
	/// <summary>
	/// Provides a base class for sending HTTP requests and receiving HTTP responses from a resource identified by a URL.
	/// </summary>
	public class HttpClient
	{
		/// <summary>
		/// Represents a singleton <see cref="HttpClient" /> object. This field is read-only.
		/// </summary>
		public static readonly HttpClient Default = new HttpClient();
		/// <summary>
		/// Represents a singleton <see cref="HttpClient" /> object with a cookie container. This field is read-only.
		/// </summary>
		public static readonly HttpClient DefaultWithCookies = new HttpClient(true);

		/// <summary>
		/// Gets or sets value indicating whether to use a <see cref="System.Net.CookieContainer" />. Changing the value will not clear existing cookies, until <see cref="ClearCookies" /> is invoked.
		/// </summary>
		public bool UseCookies { get; set; }
		/// <summary>
		/// Gets or sets the user agent that is used for HTTP requests.
		/// </summary>
		public string UserAgent { get; set; }
		/// <summary>
		/// Gets or sets a value that indicates whether the request should follow redirection responses.
		/// </summary>
		public bool AllowAutoRedirect { get; set; }
		/// <summary>
		/// Gets the collection of cookies that is used for HTTP requests. If <see cref="UseCookies" /> is set to <see langword="true" />, <see cref="CookieContainer" /> is used; otherwise, it is ignored, however cookies will not be deleted.
		/// </summary>
		public CookieContainer CookieContainer { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpClient" /> class.
		/// </summary>
		public HttpClient()
		{
			AllowAutoRedirect = true;
			CookieContainer = new CookieContainer();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="HttpClient" /> class.
		/// </summary>
		/// <param name="useCookies"><see langword="true" /> to use cookies; otherwise, <see langword="false" />.</param>
		public HttpClient(bool useCookies) : this()
		{
			UseCookies = useCookies;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="HttpClient" /> class.
		/// </summary>
		/// <param name="useCookies"><see langword="true" /> to use cookies; otherwise, <see langword="false" />.</param>
		/// <param name="userAgent">The user agent that is used for HTTP requests.</param>
		public HttpClient(bool useCookies, string userAgent) : this(useCookies)
		{
			UserAgent = userAgent;
		}

		/// <summary>
		/// Creates a new HTTP GET request using the specified URL.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <returns>
		/// A new <see cref="HttpGetRequest" /> object that represents the HTTP GET request.
		/// </returns>
		public HttpGetRequest CreateGetRequest(string url)
		{
			Check.ArgumentNull(url, nameof(url));

			return new HttpGetRequest(this, url);
		}
		/// <summary>
		/// Creates a new HTTP POST request using the specified URL.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <returns>
		/// A new <see cref="HttpPostRequest" /> object that represents the HTTP POST request.
		/// </returns>
		public HttpPostRequest CreatePostRequest(string url)
		{
			Check.ArgumentNull(url, nameof(url));

			return new HttpPostRequest(this, url);
		}
		/// <summary>
		/// Creates a new HTTP multipart request using the specified URL.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <returns>
		/// A new <see cref="HttpMultipartRequest" /> object that represents the HTTP multipart request.
		/// </returns>
		public HttpMultipartRequest CreateMultipartRequest(string url)
		{
			Check.ArgumentNull(url, nameof(url));

			return new HttpMultipartRequest(this, url);
		}
		/// <summary>
		/// Creates a new HTTP HEAD request using the specified URL.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <returns>
		/// A new <see cref="HttpHeadRequest" /> object that represents the HTTP HEAD request.
		/// </returns>
		public HttpHeadRequest CreateHeadRequest(string url)
		{
			Check.ArgumentNull(url, nameof(url));

			return new HttpHeadRequest(this, url);
		}
		/// <summary>
		/// Removes all cookies from the underlying <see cref="System.Net.CookieContainer" />.
		/// </summary>
		public void ClearCookies()
		{
			CookieContainer = new CookieContainer();
		}
	}
}