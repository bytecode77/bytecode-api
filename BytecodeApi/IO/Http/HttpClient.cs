using BytecodeApi.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Web;

namespace BytecodeApi.IO.Http
{
	//FEATURE: PostMultipart TransferCallback
	/// <summary>
	/// Provides a base class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.
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
		/// Represents a singleton <see cref="WebClient" /> object. This field is read-only.
		/// </summary>
		public static readonly WebClient DefaultWebClient = new WebClient();
		/// <summary>
		/// Specifies a <see cref="RemoteCertificateValidationCallback" /> callback that always returns <see langword="true" />, thus always validating the certificate. This field is read-only.
		/// </summary>
		public static readonly RemoteCertificateValidationCallback AlwaysValidCertificateValidationCallback = delegate { return true; };
		/// <summary>
		/// Gets or sets <see cref="bool" /> value indicating whether to use a <see cref="CookieContainer" /> or not. Changing the value will not empty the <see cref="CookieContainer" />, until <see cref="ClearCookies" /> is called.
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
		/// Gets the collection of cookies that is used for HTTP requests. If <see cref="UserAgent" /> is set to <see langword="true" />, <see cref="CookieContainer" /> is used; otherwise, it is ignored, however cookies will not be deleted.
		/// </summary>
		public CookieContainer CookieContainer { get; private set; }
		/// <summary>
		/// Occurs when a <see cref="HttpWebRequest" /> is created, prior to sending of the request. This can be used as a hook to modify the internal <see cref="HttpWebRequest" /> object that is temporarily created with each HTTP request.
		/// </summary>
		public event EventHandler<HttpWebRequest> WebRequestCreated;

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
		/// Sends a GET request to the specified URL.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <returns>
		/// A <see cref="string" /> containing the body of the HTTP response.
		/// </returns>
		public string GetString(string url)
		{
			Check.ArgumentNull(url, nameof(url));

			return CreateGetRequest(url).ReadToEndString();
		}
		/// <summary>
		/// Sends a GET request to the specified URL.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <returns>
		/// A <see cref="byte" />[] containing the body of the HTTP response.
		/// </returns>
		public byte[] GetBytes(string url)
		{
			return GetBytes(url, null);
		}
		/// <summary>
		/// Sends a GET request to the specified URL.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <param name="callback">The method that is called periodically while binary data is transferred, or <see langword="null" />.</param>
		/// <returns>
		/// A <see cref="byte" />[] containing the body of the HTTP response.
		/// </returns>
		public byte[] GetBytes(string url, TransferCallback callback)
		{
			Check.ArgumentNull(url, nameof(url));

			return CreateGetRequest(url).ReadToEndBytes(callback);
		}
		/// <summary>
		/// Sends a POST request to the specified URL with the specified POST values.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <param name="values">A collection of POST values to include in the POST request.</param>
		/// <returns>
		/// A <see cref="string" /> containing the body of the HTTP response.
		/// </returns>
		public string PostString(string url, IEnumerable<PostValue> values)
		{
			Check.ArgumentNull(url, nameof(url));
			Check.ArgumentNull(values, nameof(values));

			return CreatePostRequest(url, values).ReadToEndString();
		}
		/// <summary>
		/// Sends a POST request to the specified URL with a <see cref="string" /> specifying the POST request body.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <param name="data">A <see cref="string" /> that specifies the POST request body.</param>
		/// <returns>
		/// A <see cref="string" /> containing the body of the HTTP response.
		/// </returns>
		public string PostString(string url, string data)
		{
			Check.ArgumentNull(url, nameof(url));
			Check.ArgumentNull(data, nameof(data));

			return CreatePostRequest(url, data).ReadToEndString();
		}
		/// <summary>
		/// Sends a POST request to the specified URL with a <see cref="byte" />[] specifying the POST request body.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <param name="data">A <see cref="byte" />[] that specifies the POST request body.</param>
		/// <returns>
		/// A <see cref="string" /> containing the body of the HTTP response.
		/// </returns>
		public string PostString(string url, byte[] data)
		{
			Check.ArgumentNull(url, nameof(url));
			Check.ArgumentNull(data, nameof(data));

			return CreatePostRequest(url, data).ReadToEndString();
		}
		/// <summary>
		/// Sends a POST request to the specified URL with the specified POST values.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <param name="values">A collection of POST values to include in the POST request.</param>
		/// <returns>
		/// A <see cref="byte" />[] containing the body of the HTTP response.
		/// </returns>
		public byte[] PostBytes(string url, IEnumerable<PostValue> values)
		{
			return PostBytes(url, values, null);
		}
		/// <summary>
		/// Sends a POST request to the specified URL with a <see cref="string" /> specifying the POST request body.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <param name="data">A <see cref="string" /> that specifies the POST request body.</param>
		/// <returns>
		/// A <see cref="byte" />[] containing the body of the HTTP response.
		/// </returns>
		public byte[] PostBytes(string url, string data)
		{
			return PostBytes(url, data, null);
		}
		/// <summary>
		/// Sends a POST request to the specified URL with a <see cref="byte" />[] specifying the POST request body.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <param name="data">A <see cref="byte" />[] that specifies the POST request body.</param>
		/// <returns>
		/// A <see cref="byte" />[] containing the body of the HTTP response.
		/// </returns>
		public byte[] PostBytes(string url, byte[] data)
		{
			return PostBytes(url, data, null);
		}
		/// <summary>
		/// Sends a POST request to the specified URL with the specified POST values.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <param name="values">A collection of POST values to include in the POST request.</param>
		/// <param name="callback">The method that is called periodically while binary data is transferred, or <see langword="null" />.</param>
		/// <returns>
		/// A <see cref="byte" />[] containing the body of the HTTP response.
		/// </returns>
		public byte[] PostBytes(string url, IEnumerable<PostValue> values, TransferCallback callback)
		{
			Check.ArgumentNull(url, nameof(url));
			Check.ArgumentNull(values, nameof(values));

			return CreatePostRequest(url, values).ReadToEndBytes(callback);
		}
		/// <summary>
		/// Sends a POST request to the specified URL with a <see cref="string" /> specifying the POST request body.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <param name="data">A <see cref="string" /> that specifies the POST request body.</param>
		/// <param name="callback">The method that is called periodically while binary data is transferred, or <see langword="null" />.</param>
		/// <returns>
		/// A <see cref="byte" />[] containing the body of the HTTP response.
		/// </returns>
		public byte[] PostBytes(string url, string data, TransferCallback callback)
		{
			Check.ArgumentNull(url, nameof(url));
			Check.ArgumentNull(data, nameof(data));

			return CreatePostRequest(url, data).ReadToEndBytes(callback);
		}
		/// <summary>
		/// Sends a POST request to the specified URL with a <see cref="byte" />[] specifying the POST request body.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <param name="data">A <see cref="byte" />[] that specifies the POST request body.</param>
		/// <param name="callback">The method that is called periodically while binary data is transferred, or <see langword="null" />.</param>
		/// <returns>
		/// A <see cref="byte" />[] containing the body of the HTTP response.
		/// </returns>
		public byte[] PostBytes(string url, byte[] data, TransferCallback callback)
		{
			Check.ArgumentNull(url, nameof(url));
			Check.ArgumentNull(data, nameof(data));

			return CreatePostRequest(url, data).ReadToEndBytes(callback);
		}
		/// <summary>
		/// Sends a multipart POST request to the specified URL with the specified POST values and the specified files to upload. At least one of <paramref name="values" /> or <paramref name="files" /> needs to be not <see langword="null" />.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <param name="values">A collection of POST values to include in the POST request.</param>
		/// <param name="files">A collection of files to upload in the POST request.</param>
		/// <returns>
		/// A <see cref="string" /> containing the body of the HTTP response.
		/// </returns>
		public string PostMultipartString(string url, IEnumerable<PostValue> values, IEnumerable<PostFile> files)
		{
			Check.ArgumentNull(url, nameof(url));
			Check.Argument(values != null || files != null, null, "Either of '" + nameof(values) + "' or '" + nameof(files) + "' must not be null.");

			return CreateFileRequest(url, values, files).ReadToEndString();
		}
		/// <summary>
		/// Sends a multipart POST request to the specified URL with the specified POST values and the specified files to upload. At least one of <paramref name="values" /> or <paramref name="files" /> needs to be not <see langword="null" />.
		/// </summary>
		/// <param name="url">The URL the request is sent to.</param>
		/// <param name="values">A collection of POST values to include in the POST request.</param>
		/// <param name="files">A collection of files to upload in the POST request.</param>
		/// <returns>
		/// A <see cref="byte" />[] containing the body of the HTTP response.
		/// </returns>
		public byte[] PostMultipartBytes(string url, IEnumerable<PostValue> values, IEnumerable<PostFile> files)
		{
			Check.ArgumentNull(url, nameof(url));
			Check.Argument(values != null || files != null, null, "Either of '" + nameof(values) + "' or '" + nameof(files) + "' must not be null.");

			return CreateFileRequest(url, values, files).ReadToEndBytes();
		}
		/// <summary>
		/// Removes all cookies from the <see cref="CookieContainer" /> object.
		/// </summary>
		public void ClearCookies()
		{
			CookieContainer = new CookieContainer();
		}

		private HttpWebRequest CreateGetRequest(string url)
		{
			HttpWebRequest request = CreateRequest(url, WebRequestMethods.Http.Get);
			OnWebRequestCreated(request);
			return request;
		}
		private HttpWebRequest CreatePostRequest(string url, IEnumerable<PostValue> values)
		{
			return CreatePostRequest(url, values.Select(value => value.Key + "=" + HttpUtility.UrlEncode(value.Value ?? "")).AsString("&"));
		}
		private HttpWebRequest CreatePostRequest(string url, string data)
		{
			return CreatePostRequest(url, data.ToUTF8Bytes());
		}
		private HttpWebRequest CreatePostRequest(string url, byte[] data)
		{
			HttpWebRequest request = CreateRequest(url, WebRequestMethods.Http.Post);
			request.ContentType = "application/x-www-form-urlencoded";
			OnWebRequestCreated(request);

			request.ContentLength = data.Length;
			using (Stream stream = request.GetRequestStream())
			{
				stream.Write(data);
			}

			return request;
		}
		private HttpWebRequest CreateFileRequest(string url, IEnumerable<PostValue> values, IEnumerable<PostFile> files)
		{
			const string contentDisposition = "Content-Disposition: form-data; name=\"";
			const string contentDispositionSeparatorValue = "\"\r\n\r\n";
			const string contentDispositionSeparatorFile1 = "\"; filename=\"";
			const string contentDispositionSeparatorFile2 = "\"\r\nContent-Type: application/octet-stream\r\n\r\n";

			string boundary = "---------------------------" + DateTime.Now.Ticks.ToStringInvariant("x").Left(12);
			byte[] firstBoundaryBytes = ("--" + boundary + "\r\n").ToAnsiBytes();
			byte[] boundaryBytes = ("\r\n--" + boundary + "\r\n").ToAnsiBytes();
			byte[] lastBoundaryBytes = ("\r\n--" + boundary + "--\r\n").ToAnsiBytes();

			HttpWebRequest request = CreateRequest(url, WebRequestMethods.Http.Post);
			request.ContentType = "multipart/form-data; boundary=" + boundary;
			request.KeepAlive = true;
			OnWebRequestCreated(request);

			request.ContentLength = 0;
			byte[] currentBoundary = firstBoundaryBytes;

			if (values != null)
			{
				foreach (PostValue value in values)
				{
					request.ContentLength +=
						currentBoundary.Length +
						contentDisposition.Length +
						contentDispositionSeparatorValue.Length +
						Encoding.UTF8.GetByteCount(value.Key ?? "") +
						Encoding.UTF8.GetByteCount(value.Value ?? "");

					currentBoundary = boundaryBytes;
				}
			}
			if (files != null)
			{
				foreach (PostFile file in files)
				{
					request.ContentLength +=
						currentBoundary.Length +
						contentDisposition.Length +
						HttpUtility.HtmlEncode(file.FormName ?? "").Length +
						contentDispositionSeparatorFile1.Length +
						HttpUtility.HtmlEncode(file.FileName ?? "").Length +
						contentDispositionSeparatorFile2.Length +
						file.Content?.Length ?? 0;

					currentBoundary = boundaryBytes;
				}
			}

			request.ContentLength += lastBoundaryBytes.Length;

			using (Stream stream = request.GetRequestStream())
			{
				if (values != null)
				{
					foreach (PostValue value in values)
					{
						stream.Write(firstBoundaryBytes);
						firstBoundaryBytes = boundaryBytes;

						stream.Write((contentDisposition + value.Key + contentDispositionSeparatorValue + value.Value).ToUTF8Bytes());
					}
				}
				if (files != null)
				{
					foreach (PostFile file in files)
					{
						stream.Write(firstBoundaryBytes);
						firstBoundaryBytes = boundaryBytes;

						stream.Write((contentDisposition + HttpUtility.HtmlEncode(file.FormName ?? "") + contentDispositionSeparatorFile1 + HttpUtility.HtmlEncode(file.FileName ?? "") + contentDispositionSeparatorFile2).ToUTF8Bytes());
						if (file.Content != null) stream.Write(file.Content);
					}
				}

				stream.Write(lastBoundaryBytes);
			}

			return request;
		}
		private HttpWebRequest CreateRequest(string url, string method)
		{
			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
			request.Method = method;
			request.UserAgent = UserAgent;
			request.AllowAutoRedirect = AllowAutoRedirect;
			request.CookieContainer = UseCookies ? CookieContainer : null;
			return request;
		}

		/// <summary>
		/// Raises the <see cref="WebRequestCreated" /> event.
		/// </summary>
		/// <param name="request">The internal <see cref="HttpWebRequest" /> object that is temporarily created with each HTTP request.</param>
		protected void OnWebRequestCreated(HttpWebRequest request)
		{
			Check.ArgumentNull(request, nameof(request));

			WebRequestCreated?.Invoke(this, request);
		}
	}
}