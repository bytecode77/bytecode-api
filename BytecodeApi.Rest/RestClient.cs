namespace BytecodeApi.Rest;

/// <summary>
/// Defines the base class for REST clients.
/// </summary>
public abstract class RestClient : IDisposable
{
	internal bool Disposed;
	/// <summary>
	/// Gets the base URL of the REST service without a trailing slash.
	/// </summary>
	public string BaseUrl { get; private init; }
	/// <summary>
	/// Gets the <see cref="System.Net.Http.HttpClient" /> that is used to process requests.
	/// </summary>
	protected internal HttpClient HttpClient { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="RestClient" /> class.
	/// </summary>
	/// <param name="baseUrl">The base URL of the REST service. Trailing slashes will be removed.</param>
	protected RestClient(string baseUrl)
	{
		Check.ArgumentNull(baseUrl);

		BaseUrl = baseUrl.TrimEnd('/');
		HttpClient = new();
	}
	/// <summary>
	/// Releases all resources used by the current instance of the <see cref="RestClient" /> class.
	/// </summary>
	public void Dispose()
	{
		if (!Disposed)
		{
			HttpClient.Dispose();

			Disposed = true;
		}
	}

	/// <summary>
	/// Performs a GET request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	protected RestRequest Get(string url)
	{
		Check.ObjectDisposed<RestClient>(Disposed);
		Check.ArgumentNull(url);

		return new(this, HttpMethod.Get, url);
	}
	/// <summary>
	/// Performs a POST request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	protected RestRequest Post(string url)
	{
		Check.ObjectDisposed<RestClient>(Disposed);
		Check.ArgumentNull(url);

		return new(this, HttpMethod.Post, url);
	}
	/// <summary>
	/// Performs a PUT request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	protected RestRequest Put(string url)
	{
		Check.ObjectDisposed<RestClient>(Disposed);
		Check.ArgumentNull(url);

		return new(this, HttpMethod.Put, url);
	}
	/// <summary>
	/// Performs a PATCH request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	protected RestRequest Patch(string url)
	{
		Check.ObjectDisposed<RestClient>(Disposed);
		Check.ArgumentNull(url);

		return new(this, HttpMethod.Patch, url);
	}
	/// <summary>
	/// Performs a DELETE request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	protected RestRequest Delete(string url)
	{
		Check.ObjectDisposed<RestClient>(Disposed);
		Check.ArgumentNull(url);

		return new(this, HttpMethod.Delete, url);
	}
	/// <summary>
	/// Performs a HEAD request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	protected RestRequest Head(string url)
	{
		Check.ObjectDisposed<RestClient>(Disposed);
		Check.ArgumentNull(url);

		return new(this, HttpMethod.Head, url);
	}
	/// <summary>
	/// Performs a OPTIONS request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	protected RestRequest Options(string url)
	{
		Check.ObjectDisposed<RestClient>(Disposed);
		Check.ArgumentNull(url);

		return new(this, HttpMethod.Options, url);
	}
	/// <summary>
	/// Performs a TRACE request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	protected RestRequest Trace(string url)
	{
		Check.ObjectDisposed<RestClient>(Disposed);
		Check.ArgumentNull(url);

		return new(this, HttpMethod.Trace, url);
	}
}