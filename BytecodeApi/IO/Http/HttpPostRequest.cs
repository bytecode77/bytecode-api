using BytecodeApi.Extensions;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace BytecodeApi.IO.Http
{
	/// <summary>
	/// Represents a HTTP POST requests.
	/// </summary>
	public sealed class HttpPostRequest : HttpRequest
	{
		internal HttpPostRequest(HttpClient client, string url) : base(client, url)
		{
		}

		/// <summary>
		/// Adds an HTTP GET parameter with the specified name and value to the HTTP POST request.
		/// </summary>
		/// <param name="name">The name of the GET parameter.</param>
		/// <param name="value">The value of the GET parameter.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public HttpPostRequest AddQueryParameter(string name, string value)
		{
			QueryParameters.Add(new HttpParameter(name, value));
			return this;
		}
		/// <summary>
		/// Adds an HTTP POST value with the specified key and value to the HTTP POST request.
		/// </summary>
		/// <param name="key">The key of the POST value.</param>
		/// <param name="value">The value of the POST value.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public HttpPostRequest AddPostValue(string key, string value)
		{
			PostValues.Add(new HttpParameter(key, value));
			return this;
		}
		/// <summary>
		/// Sets the raw POST data of the HTTP POST request. If required, this can be used instead of the <see cref="AddPostValue(string, string)" /> method. Both methods cannot be used in the same request.
		/// </summary>
		/// <param name="data">A <see cref="string" /> with the raw POST data. This string is encoded using the <see cref="Encoding.UTF8" /> encoding.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public HttpPostRequest SetPostData(string data)
		{
			return SetPostData(data.ToUTF8Bytes());
		}
		/// <summary>
		/// Sets the raw POST data of the HTTP POST request. If required, this can be used instead of the <see cref="AddPostValue(string, string)" /> method. Both methods cannot be used in the same request.
		/// </summary>
		/// <param name="data">A <see cref="byte" />[] with the raw POST data..</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public HttpPostRequest SetPostData(byte[] data)
		{
			PostData = data;
			return this;
		}

		private protected override HttpWebRequest GetWebRequest()
		{
			Check.InvalidOperation(!(PostData != null && PostValues.Any()), "Cannot add raw post data and post parameters simultaneously.");

			HttpWebRequest webRequest = CreateRequest(WebRequestMethods.Http.Post);
			webRequest.ContentType = "application/x-www-form-urlencoded";
			OnWebRequestCreated(webRequest);

			byte[] data = PostData ?? PostValues
				.Select(value => value.Key + "=" + HttpUtility.UrlEncode(value.Value ?? ""))
				.AsString("&")
				.ToUTF8Bytes();

			webRequest.ContentLength = data.Length;
			using (Stream stream = webRequest.GetRequestStream())
			{
				stream.Write(data);
			}

			return webRequest;
		}
	}
}