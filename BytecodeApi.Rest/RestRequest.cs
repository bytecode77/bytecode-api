using BytecodeApi.Data;
using BytecodeApi.Extensions;
using System.Collections;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace BytecodeApi.Rest;

/// <summary>
/// Represents a REST request to be used to further refine, and then send the request.
/// </summary>
public sealed class RestRequest
{
	/// <summary>
	/// Gets the <see cref="BytecodeApi.Rest.RestClient" /> from which this <see cref="RestRequest" /> has been initiated.
	/// </summary>
	public RestClient RestClient { get; private init; }
	/// <summary>
	/// Gets the <see cref="System.Net.Http.HttpMethod" /> of this <see cref="RestRequest" />.
	/// </summary>
	public HttpMethod HttpMethod { get; private init; }
	/// <summary>
	/// Gets the URL of this <see cref="RestRequest" />.
	/// </summary>
	public string Url { get; private set; }
	private HttpContent? HttpContent;
	private readonly Dictionary<string, string> Headers;

	internal RestRequest(RestClient restClient, HttpMethod method, string url)
	{
		RestClient = restClient;
		HttpMethod = method;
		Url = url;
		Headers = [];
	}

	/// <summary>
	/// Adds a HTTP header to this REST request.
	/// </summary>
	/// <param name="name">The name of the header.</param>
	/// <param name="value">The value of the header</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public RestRequest Header(string name, string value)
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);
		Check.ArgumentNull(name);
		Check.ArgumentNull(value);
		Check.ArgumentNull(RestClient.RequestOptions);

		Headers[name] = value;

		return this;
	}
	/// <summary>
	/// Adds a query parameter to this REST request.
	/// </summary>
	/// <param name="name">The name of the query parameter.</param>
	/// <param name="value">The value of the query parameter.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public RestRequest QueryParameter(string name, object? value)
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);
		Check.ArgumentNull(name);
		Check.ArgumentNull(RestClient.RequestOptions);

		if (value == null)
		{
		}
		else if (value is IEnumerable enumerable and not string)
		{
			foreach (object? arrayElement in enumerable)
			{
				QueryParameter(name, arrayElement);
			}
		}
		else
		{
			string valueString = value switch
			{
				string str => str,
				Enum enumValue => Convert.ToInt32(enumValue).ToString(),
				DateTime dateTimeParameter => dateTimeParameter.ToStringInvariant(RestClient.RequestOptions.QueryParameterDateTimeFormat),
				DateOnly dateOnlyParameter => dateOnlyParameter.ToStringInvariant(RestClient.RequestOptions.QueryParameterDateOnlyFormat),
				TimeOnly timeOnlyParameter => timeOnlyParameter.ToStringInvariant(RestClient.RequestOptions.QueryParameterTimeOnlyFormat),
				_ => value?.ToString() ?? ""
			};

			Url = AddQueryString(Url, name, valueString);
		}

		return this;
	}
	/// <summary>
	/// Sets the content of this REST request.
	/// </summary>
	/// <param name="content">The <see cref="string" /> content to be sent with the request.</param>
	/// <param name="contentType">The content type to be sent with the request.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public RestRequest StringContent(string content, string contentType)
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);
		Check.ArgumentNull(content);
		Check.ArgumentNull(contentType);

		HttpContent = new StringContent(content, Encoding.UTF8, contentType);
		return this;
	}
	/// <summary>
	/// Sets the content of this REST request to a JSON object.
	/// </summary>
	/// <typeparam name="T">The type of the JSON object.</typeparam>
	/// <param name="content">An <see cref="object" /> to be serialized to JSON and then sent with the request.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public RestRequest JsonContent<T>(T? content)
	{
		return JsonContent(content, null);
	}
	/// <summary>
	/// Sets the content of this REST request to a JSON object.
	/// </summary>
	/// <typeparam name="T">The type of the JSON object.</typeparam>
	/// <param name="content">An <see cref="object" /> to be serialized to JSON and then sent with the request.</param>
	/// <param name="serializerOptions">Options to control the conversion behavior.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public RestRequest JsonContent<T>(T? content, JsonSerializerOptions? serializerOptions)
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);

		return StringContent(JsonSerializer.Serialize(content, serializerOptions), "application/json");
	}
	/// <summary>
	/// Sets the content of this REST request to an x-www-form-urlencoded body.
	/// </summary>
	/// <typeparam name="T">The type of the body.</typeparam>
	/// <param name="content">An <see cref="object" /> to be serialized into an x-www-form-urlencoded body.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public RestRequest FormUrlEncodedContent<T>(T content)
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);
		Check.ArgumentNull(content);

		Dictionary<string, string> nameValueCollection = content
			.GetType()
			.GetProperties(BindingFlags.Instance | BindingFlags.Public)
			.ToDictionary(property => property.Name, property => property.GetValue(content)?.ToString() ?? "");

		HttpContent = new FormUrlEncodedContent(nameValueCollection);
		return this;
	}
	/// <summary>
	/// Sets the content of this REST request to an x-www-form-urlencoded body.
	/// </summary>
	/// <typeparam name="T">The type of the value of <paramref name="content" />.</typeparam>
	/// <param name="content">A <see cref="Dictionary{TKey, TValue}" /> to be serialized into an x-www-form-urlencoded body.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public RestRequest FormUrlEncodedContent<T>(Dictionary<string, T?> content)
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);
		Check.ArgumentNull(content);

		HttpContent = new FormUrlEncodedContent(content.ToDictionary(property => property.Key, property => property.Value?.ToString() ?? ""));
		return this;
	}
	/// <summary>
	/// Includes a file into the multipart request.
	/// </summary>
	/// <param name="name">The name of the HTTP content.</param>
	/// <param name="file">A <see cref="Blob" /> with the file to send.</param>
	/// <param name="contentType">The content type of <paramref name="file" />.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public RestRequest MultipartFileContent(string name, Blob file, string contentType)
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);
		Check.ArgumentNull(name);
		Check.ArgumentNull(file);
		Check.ArgumentNull(contentType);

		ByteArrayContent fileContent = new(file.Content);
		fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
		AddMultipartContent().Add(fileContent, name, file.Name);
		return this;
	}
	/// <summary>
	/// Includes a <see cref="string" /> into the multipart request.
	/// </summary>
	/// <param name="name">The name of the HTTP content.</param>
	/// <param name="content">The <see cref="string" /> content to be sent with the multipart request.</param>
	/// <param name="contentType">The content type of <paramref name="content" />.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public RestRequest MultipartStringContent(string name, string content, string contentType)
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);
		Check.ArgumentNull(name);
		Check.ArgumentNull(content);
		Check.ArgumentNull(contentType);

		AddMultipartContent().Add(new StringContent(content, Encoding.UTF8, contentType), name);
		return this;
	}

	/// <summary>
	/// Sends the request without reading the response body.
	/// </summary>
	public async Task Execute()
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);

		using HttpResponseMessage response = await Send(HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
		await response.Content.ReadAsStringAsync().ConfigureAwait(false);
	}
	/// <summary>
	/// Sends the request and reads the response <see cref="string" />.
	/// </summary>
	/// <returns>
	/// A <see cref="string" />, representing the REST response.
	/// </returns>
	public async Task<string> ReadString()
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);

		using HttpResponseMessage response = await Send().ConfigureAwait(false);
		return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
	}
	/// <summary>
	/// Sends the request and reads the response as a <see cref="byte" />[].
	/// </summary>
	/// <returns>
	/// A <see cref="byte" />[], representing the REST response.
	/// </returns>
	public Task<byte[]> ReadByteArray()
	{
		return ReadByteArray(null);
	}
	/// <summary>
	/// Sends the request and reads the response as a <see cref="byte" />[].
	/// </summary>
	/// <param name="progressCallback">A delegate that is invoked with information about the progress of the download.</param>
	/// <returns>
	/// A <see cref="byte" />[], representing the REST response.
	/// </returns>
	public async Task<byte[]> ReadByteArray(ProgressCallback? progressCallback)
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);

		return (await ReadFile(progressCallback).ConfigureAwait(false)).Content;
	}
	/// <summary>
	/// Sends the request and reads the response as a <see cref="byte" />[], including the filename from the Content-Disposition.
	/// </summary>
	/// <returns>
	/// A new <see cref="Blob" /> that contains both the filename from the Content-Disposition, and the file content.
	/// </returns>
	public Task<Blob> ReadFile()
	{
		return ReadFile(null);
	}
	/// <summary>
	/// Sends the request and reads the response as a <see cref="byte" />[], including the filename from the Content-Disposition.
	/// </summary>
	/// <param name="progressCallback">A delegate that is invoked with information about the progress of the download.</param>
	/// <returns>
	/// A new <see cref="Blob" /> that contains both the filename from the Content-Disposition, and the file content.
	/// </returns>
	public async Task<Blob> ReadFile(ProgressCallback? progressCallback)
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);

		if (progressCallback == null)
		{
			using HttpResponseMessage response = await Send().ConfigureAwait(false);

			return new(
				response.Content.Headers.ContentDisposition?.FileNameStar ?? "",
				await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false));
		}
		else
		{
			using HttpResponseMessage response = await Send(HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
			using Stream stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

			using MemoryStream memoryStream = new(response.Content.Headers.ContentLength <= int.MaxValue ? (int)response.Content.Headers.ContentLength.Value : int.MaxValue);
			byte[] buffer = new byte[4096];
			int bytesRead;
			long totalBytesRead = 0;
			DateTime lastCallback = default;

			do
			{
				bytesRead = await stream.ReadAsync(buffer).ConfigureAwait(false);
				totalBytesRead += bytesRead;
				memoryStream.Write(buffer, 0, bytesRead);

				if (DateTime.Now - lastCallback > TimeSpan.FromMilliseconds(20))
				{
					lastCallback = DateTime.Now;
					progressCallback(totalBytesRead, Math.Max(totalBytesRead, response.Content.Headers.ContentLength ?? 0));
				}
			}
			while (bytesRead > 0);

			progressCallback(totalBytesRead, Math.Max(totalBytesRead, response.Content.Headers.ContentLength ?? 0));

			return new(
				response.Content.Headers.ContentDisposition?.FileNameStar ?? "",
				memoryStream.ToArray());
		}
	}
	/// <summary>
	/// Sends the request and reads the response as a JSON object.
	/// </summary>
	/// <typeparam name="T">The type of the JSON object.</typeparam>
	/// <returns>
	/// The JSON object, deserialized from the REST response.
	/// </returns>
	public Task<T> ReadJson<T>()
	{
		return ReadJson<T>(null);
	}
	/// <summary>
	/// Sends the request and reads the response as a JSON object.
	/// </summary>
	/// <typeparam name="T">The type of the JSON object.</typeparam>
	/// <param name="serializerOptions">Options to control the conversion behavior.</param>
	/// <returns>
	/// The JSON object, deserialized from the REST response.
	/// </returns>
	public async Task<T> ReadJson<T>(JsonSerializerOptions? serializerOptions)
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);

		using HttpResponseMessage response = await Send().ConfigureAwait(false);
		using Stream stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
		return (await JsonSerializer.DeserializeAsync<T>(stream, serializerOptions).ConfigureAwait(false)) ?? throw new JsonException("Deserialization returned null value.");
	}
	/// <summary>
	/// Sends the request and reads server-sent events.
	/// </summary>
	/// <param name="eventCallback">A delegate that is invoked with the the server-sent event data.</param>
	public async Task ReadEvents(ServerSentEventCallback eventCallback)
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);
		Check.ArgumentNull(eventCallback);

		using HttpResponseMessage response = await Send(HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
		using Stream stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
		using StreamReader reader = new(stream);

		// https://html.spec.whatwg.org/multipage/server-sent-events.html

		string eventType = "message";
		string? eventId = null;
		StringBuilder data = new();

		while (!reader.EndOfStream)
		{
			if (reader.ReadLine() is string line)
			{
				if (line == "")
				{
					eventCallback(eventType, data.ToString().Trim(), eventId);

					eventType = "message";
					data.Clear();
				}
				else if (line.StartsWith(':'))
				{
					// Ignore
				}
				else if (line.Contains(':'))
				{
					string field;
					string value;

					if (line.Contains(':'))
					{
						field = line.SubstringUntil(':');
						value = line.SubstringFrom(':');

						if (value.StartsWith(' '))
						{
							value = value[1..];
						}
					}
					else
					{
						field = line;
						value = "";
					}

					switch (field)
					{
						case "event":
							eventType = value;
							break;
						case "data":
							data.AppendLine(value);
							break;
						case "id" when !value.Contains('\0'):
							eventId = value.ToNullIfEmpty();
							break;
					}
				}
			}
		}
	}

	private async Task<HttpResponseMessage> Send(HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
	{
		HttpRequestMessage request = new(HttpMethod, Url)
		{
			Content = HttpContent
		};

		foreach (KeyValuePair<string, string> header in Headers)
		{
			request.Headers.Add(header.Key, header.Value);
		}

		HttpResponseMessage response = await RestClient.HttpClient.SendAsync(request, completionOption).ConfigureAwait(false);

		if (response.IsSuccessStatusCode)
		{
			return response;
		}
		else
		{
			string content = "";
			try
			{
				content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			}
			catch
			{
			}

			throw new RestException(response.StatusCode, content);
		}
	}
	private static string AddQueryString(string url, string key, string value)
	{
		int indexOfFragment = url.IndexOf('#');
		string fragment = "";

		if (indexOfFragment != -1)
		{
			fragment = url[indexOfFragment..];
			url = url[..indexOfFragment];
		}

		return $"{url}{(url.Contains('?') ? '&' : '?')}{UrlEncoder.Default.Encode(key)}={UrlEncoder.Default.Encode(value)}{fragment}";
	}
	private MultipartFormDataContent AddMultipartContent()
	{
		Check.InvalidOperation(HttpContent is null or MultipartFormDataContent, "Cannot mix multipart content with non-multipart content.");

		HttpContent ??= new MultipartFormDataContent();
		return (MultipartFormDataContent)HttpContent;
	}
}