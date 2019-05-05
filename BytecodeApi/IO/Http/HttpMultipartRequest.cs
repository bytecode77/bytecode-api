using BytecodeApi.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace BytecodeApi.IO.Http
{
	/// <summary>
	/// Represents a HTTP multipart requests.
	/// </summary>
	public sealed class HttpMultipartRequest : HttpRequest
	{
		internal HttpMultipartRequest(HttpClient client, string url) : base(client, url)
		{
		}

		/// <summary>
		/// Adds an HTTP GET parameter with the specified name and value to the HTTP multipart request.
		/// </summary>
		/// <param name="name">The name of the GET parameter.</param>
		/// <param name="value">The value of the GET parameter.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public HttpMultipartRequest AddQueryParameter(string name, string value)
		{
			QueryParameters.Add(new HttpParameter(name, value));
			return this;
		}
		/// <summary>
		/// Adds an HTTP POST value with the specified key and value to the HTTP multipart request.
		/// </summary>
		/// <param name="key">The key of the POST value.</param>
		/// <param name="value">The value of the POST value.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public HttpMultipartRequest AddPostValue(string key, string value)
		{
			PostValues.Add(new HttpParameter(key, value));
			return this;
		}
		/// <summary>
		/// Adds a file to the HTTP multipart request.
		/// </summary>
		/// <param name="formName">The posted form name of the HTTP multipart entry.</param>
		/// <param name="fileName">The uploaded filename of the HTTP multipart entry.</param>
		/// <param name="content">The binary content of the HTTP multipart entry.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public HttpMultipartRequest AddFile(string formName, string fileName, byte[] content)
		{
			Files.Add(new HttpFile(formName, fileName, content));
			return this;
		}

		private protected override HttpWebRequest GetWebRequest()
		{
			const string contentDisposition = "Content-Disposition: form-data; name=\"";
			const string contentDispositionSeparatorValue = "\"\r\n\r\n";
			const string contentDispositionSeparatorFile1 = "\"; filename=\"";
			const string contentDispositionSeparatorFile2 = "\"\r\nContent-Type: application/octet-stream\r\n\r\n";

			string boundary = "---------------------------" + DateTime.Now.Ticks.ToStringInvariant("x").Left(12);
			byte[] firstBoundaryBytes = ("--" + boundary + "\r\n").ToAnsiBytes();
			byte[] boundaryBytes = ("\r\n--" + boundary + "\r\n").ToAnsiBytes();
			byte[] lastBoundaryBytes = ("\r\n--" + boundary + "--\r\n").ToAnsiBytes();

			HttpWebRequest webRequest = CreateRequest(WebRequestMethods.Http.Post);
			webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
			webRequest.KeepAlive = true;
			OnWebRequestCreated(webRequest);

			webRequest.ContentLength = 0;
			byte[] currentBoundary = firstBoundaryBytes;

			if (PostValues.Any())
			{
				foreach (HttpParameter value in PostValues)
				{
					webRequest.ContentLength +=
						currentBoundary.Length +
						contentDisposition.Length +
						contentDispositionSeparatorValue.Length +
						Encoding.UTF8.GetByteCount(value.Key ?? "") +
						Encoding.UTF8.GetByteCount(value.Value ?? "");

					currentBoundary = boundaryBytes;
				}
			}
			if (Files.Any())
			{
				foreach (HttpFile file in Files)
				{
					webRequest.ContentLength +=
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

			webRequest.ContentLength += lastBoundaryBytes.Length;

			using (Stream stream = webRequest.GetRequestStream())
			{
				if (PostValues.Any())
				{
					foreach (HttpParameter value in PostValues)
					{
						stream.Write(firstBoundaryBytes);
						firstBoundaryBytes = boundaryBytes;

						stream.Write((contentDisposition + value.Key + contentDispositionSeparatorValue + value.Value).ToUTF8Bytes());
					}
				}
				if (Files.Any())
				{
					foreach (HttpFile file in Files)
					{
						stream.Write(firstBoundaryBytes);
						firstBoundaryBytes = boundaryBytes;

						stream.Write((contentDisposition + HttpUtility.HtmlEncode(file.FormName ?? "") + contentDispositionSeparatorFile1 + HttpUtility.HtmlEncode(file.FileName ?? "") + contentDispositionSeparatorFile2).ToUTF8Bytes());
						if (file.Content != null) stream.Write(file.Content);
					}
				}

				stream.Write(lastBoundaryBytes);
			}

			return webRequest;
		}
	}
}