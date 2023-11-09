using BytecodeApi.Extensions;
using System.Collections;
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
	private readonly RestClient RestClient;
	private readonly HttpMethod HttpMethod;
	private string Url;
	private HttpContent? HttpContent;

	internal RestRequest(RestClient restClient, HttpMethod method, string url)
	{
		RestClient = restClient;
		HttpMethod = method;
		Url = url;
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
		Check.ArgumentNull(RestClient.Options);

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
				DateTime dateTimeParameter => dateTimeParameter.ToStringInvariant(RestClient.Options.QueryParameterDateTimeFormat),
				DateOnly dateOnlyParameter => dateOnlyParameter.ToStringInvariant(RestClient.Options.QueryParameterDateOnlyFormat),
				TimeOnly timeOnlyParameter => timeOnlyParameter.ToStringInvariant(RestClient.Options.QueryParameterTimeOnlyFormat),
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
	public RestRequest JsonContent<T>(T content)
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
	public RestRequest JsonContent<T>(T content, JsonSerializerOptions? serializerOptions)
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);
		Check.ArgumentNull(content);

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
	public async Task<byte[]> ReadByteArray()
	{
		Check.ObjectDisposed<RestClient>(RestClient.Disposed);

		using HttpResponseMessage response = await Send().ConfigureAwait(false);
		return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
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

	private async Task<HttpResponseMessage> Send()
	{
		HttpRequestMessage request = new(HttpMethod, Url)
		{
			Content = HttpContent
		};

		HttpResponseMessage response = await RestClient.HttpClient.SendAsync(request).ConfigureAwait(false);

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
}