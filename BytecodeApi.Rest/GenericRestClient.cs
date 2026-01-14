namespace BytecodeApi.Rest;

/// <summary>
/// Represents a REST client that is not tied to a specific API. This class can be used to perform singular HTTP requests that have no API context.
/// </summary>
public class GenericRestClient : RestClient
{
	/// <summary>
	/// Represents a singleton <see cref="GenericRestClient" /> instance. Changes to properties of this object are global.
	/// </summary>
	public static GenericRestClient Instance { get; }

	static GenericRestClient()
	{
		Instance = new();
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="GenericRestClient" /> class.
	/// </summary>
	public GenericRestClient() : base("")
	{
	}

	/// <summary>
	/// Performs a GET request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	new public RestRequest Get(string url)
	{
		return base.Get(url);
	}
	/// <summary>
	/// Performs a POST request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	new public RestRequest Post(string url)
	{
		return base.Post(url);
	}
	/// <summary>
	/// Performs a PUT request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	new public RestRequest Put(string url)
	{
		return base.Put(url);
	}
	/// <summary>
	/// Performs a PATCH request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	new public RestRequest Patch(string url)
	{
		return base.Patch(url);
	}
	/// <summary>
	/// Performs a DELETE request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	new public RestRequest Delete(string url)
	{
		return base.Delete(url);
	}
	/// <summary>
	/// Performs a PURGE request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	new public RestRequest Purge(string url)
	{
		return base.Purge(url);
	}
	/// <summary>
	/// Performs a HEAD request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	new public RestRequest Head(string url)
	{
		return base.Head(url);
	}
	/// <summary>
	/// Performs a OPTIONS request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	new public RestRequest Options(string url)
	{
		return base.Options(url);
	}
	/// <summary>
	/// Performs a TRACE request on the specified URL.
	/// </summary>
	/// <param name="url">The URL to perform the request on.</param>
	/// <returns>
	/// A <see cref="RestRequest" /> object to be used to further refine, and then send the request.
	/// </returns>
	new public RestRequest Trace(string url)
	{
		return base.Trace(url);
	}
}