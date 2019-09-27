using BytecodeApi.Extensions;
using BytecodeApi.Threading;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace BytecodeApi.IO.Http
{
	//FEATURE: TransferCallback also for request
	/// <summary>
	/// Represents the base class for HTTP requests. This is an abstract class.
	/// </summary>
	public abstract class HttpRequest
	{
		private protected readonly HttpClient Client;
		private protected readonly string Url;
		private protected readonly List<HttpParameter> QueryParameters;
		private protected readonly List<HttpParameter> PostValues;
		private protected byte[] PostData;
		private protected readonly List<HttpFile> Files;
		/// <summary>
		/// Occurs when a <see cref="HttpWebRequest" /> is created, prior to sending of the request. This can be used as a hook to modify the internal <see cref="HttpWebRequest" /> object that is created with each HTTP request.
		/// </summary>
		public event EventHandler<HttpWebRequest> WebRequestCreated;

		private protected HttpRequest(HttpClient client, string url)
		{
			Client = client;
			Url = url;
			QueryParameters = new List<HttpParameter>();
			PostValues = new List<HttpParameter>();
			Files = new List<HttpFile>();
		}

		/// <summary>
		/// Sends the HTTP request, reads the response and returns a <see cref="string" /> with the content. Throws a <see cref="HttpException" />, if the request failed.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> with the content of the HTTP request.
		/// </returns>
		public string ReadString()
		{
			return Try(() =>
			{
				using WebResponse response = GetWebRequest().GetResponse();
				using StreamReader reader = new StreamReader(response.GetResponseStream());
				return reader.ReadToEnd();
			});
		}
		/// <summary>
		/// Sends the HTTP request, reads the response and returns a <see cref="byte" />[] with the content. Throws a <see cref="HttpException" />, if the request failed.
		/// </summary>
		/// <returns>
		/// A <see cref="byte" />[] with the content of the HTTP request.
		/// </returns>
		public byte[] ReadBytes()
		{
			return ReadBytes(null);
		}
		/// <summary>
		/// Sends the HTTP request, reads the response and returns a <see cref="byte" />[] with the content. Throws a <see cref="HttpException" />, if the request failed.
		/// </summary>
		/// <param name="callback">The method that is called periodically while binary data is transferred, or <see langword="null" />.</param>
		/// <returns>
		/// A <see cref="byte" />[] with the content of the HTTP request.
		/// </returns>
		public byte[] ReadBytes(TransferCallback callback)
		{
			return Try(() =>
			{
				using WebResponse response = GetWebRequest().GetResponse();
				using Stream stream = response.GetResponseStream();
				using MemoryStream memoryStream = new MemoryStream();

				byte[] buffer = new byte[4096];
				int bytesRead;
				long totalBytesRead = 0;
				long callbackBytesRead = 0;
				Stopwatch stopwatch = ThreadFactory.StartStopwatch();

				do
				{
					bytesRead = stream.Read(buffer);
					totalBytesRead += bytesRead;
					memoryStream.Write(buffer, 0, bytesRead);

					if (callback != null)
					{
						callbackBytesRead += bytesRead;
						if (stopwatch.Elapsed > TimeSpan.FromMilliseconds(100))
						{
							stopwatch.Restart();
							callback(callbackBytesRead, totalBytesRead);
							callbackBytesRead = 0;
						}
					}
				}
				while (bytesRead > 0);

				if (callback != null && callbackBytesRead > 0) callback(callbackBytesRead, totalBytesRead);
				return memoryStream.ToArray();
			});
		}

		private protected abstract HttpWebRequest GetWebRequest();
		private protected HttpWebRequest CreateRequest(string method)
		{
			string requestUrl = Url;
			if (QueryParameters.Any())
			{
				UriBuilder builder = new UriBuilder(Url) { Port = -1 };

				NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
				foreach (HttpParameter parameter in QueryParameters) query.Add(parameter.Key, parameter.Value);
				builder.Query = query.ToString();

				requestUrl = builder.ToString();
			}

			HttpWebRequest webRequest = WebRequest.CreateHttp(requestUrl);
			webRequest.Method = method;
			webRequest.UserAgent = Client.UserAgent;
			webRequest.AllowAutoRedirect = Client.AllowAutoRedirect;
			webRequest.CookieContainer = Client.UseCookies ? Client.CookieContainer : null;
			return webRequest;
		}
		private static T Try<T>(Func<T> func)
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
						using StreamReader reader = new StreamReader(ex.Response.GetResponseStream());
						htmlBody = reader.ReadToEnd();
					}
					catch { }
				}

				throw new HttpException(ex.Status, ex.Response, htmlBody, ex);
			}
		}

		/// <summary>
		/// Raises the <see cref="WebRequestCreated" /> event.
		/// </summary>
		/// <param name="request">The internal <see cref="HttpWebRequest" /> object that is temporarily created with each HTTP request.</param>
		protected virtual void OnWebRequestCreated(HttpWebRequest request)
		{
			Check.ArgumentNull(request, nameof(request));

			WebRequestCreated?.Invoke(this, request);
		}
	}
}