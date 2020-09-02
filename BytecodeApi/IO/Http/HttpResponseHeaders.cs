using System;
using System.Net;

namespace BytecodeApi.IO.Http
{
	/// <summary>
	/// Represents the headers of a HTTP request.
	/// </summary>
	public sealed class HttpResponseHeaders
	{
		/// <summary>
		/// Gets a <see cref="bool" /> value that indicates whether headers are supported.
		/// </summary>
		public bool SupportsHeaders { get; internal set; }
		/// <summary>
		/// Gets a <see cref="bool" /> value that indicates whether the response was obtained from the cache.
		/// </summary>
		public bool IsFromCache { get; internal set; }
		/// <summary>
		/// Gets a <see cref="bool" /> value that indicates whether mutual authentication occurred.
		/// </summary>
		public bool IsMutuallyAuthenticated { get; internal set; }
		/// <summary>
		/// Gets the <see cref="Uri" /> of the response.
		/// </summary>
		public Uri ResponseUri { get; internal set; }
		/// <summary>
		/// Gets the version of the HTTP protocol that is used in the response.
		/// </summary>
		public Version ProtocolVersion { get; internal set; }
		/// <summary>
		/// Gets the status description returned with the response.
		/// </summary>
		public string StatusDescription { get; internal set; }
		/// <summary>
		/// Gets the status of the response.
		/// </summary>
		public HttpStatusCode StatusCode { get; internal set; }
		/// <summary>
		/// Gets the content type of the response.
		/// </summary>
		public string ContentType { get; internal set; }
		/// <summary>
		/// Gets content length in bytes of the response.
		/// </summary>
		public long ContentLength { get; internal set; }
		/// <summary>
		/// Gets the last <see cref="DateTime" /> that the contents of the response were modified.
		/// </summary>
		public DateTime LastModified { get; internal set; }
		/// <summary>
		/// Gets the name of the server that sent the response.
		/// </summary>
		public string Server { get; internal set; }
		/// <summary>
		/// Gets the character set of the response.
		/// </summary>
		public string CharacterSet { get; internal set; }
		/// <summary>
		/// Gets the method that is used to encode the body of the response.
		/// </summary>
		public string ContentEncoding { get; internal set; }
		/// <summary>
		/// Gets a collection of headers of the response.
		/// </summary>
		public WebHeaderCollection Headers { get; internal set; }
	}
}