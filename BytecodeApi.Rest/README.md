# BytecodeApi.Rest

Fluent REST client.

## Examples

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Rest

<details>
<summary>RestClient</summary>

The `RestClient` is an abstract class that provides fluent methods to query REST services.

```
public class MyService : RestClient
{
	public MyService(string baseUrl) : base(baseUrl)
	{
	}

	public async Task<User[]> GetUsers(string searchText)
	{
		return await Get($"{BaseUrl}/users")
			.QueryParameter("search", searchText)
			.ReadJson<User[]>();
	}
	public async Task UpdateUser(User user, bool activate)
	{
		await Post($"{BaseUrl}/users/update")
			.QueryParameter("activate", activate)
			.JsonContent(user)
			.ReadString();
	}
}
```

- The fluent call starts with `Get`, `Post`, `Put`, `Patch`, etc...
- The following calls apply optional parameters to the HTTP request:
  - `QueryParameter`
  - `StringContent`
  - `JsonContent`
  - `FormUrlEncodedContent`
  - `MultipartFileContent` and `MultipartStringContent`
- Finally, execute the REST request by calling either of the following:
  - `ReadString`
  - `ReadByteArray`
  - `ReadJson`

</details>

<details>
<summary>RestException</summary>

If a `RestClient` encounters an error, a `RestException` is thrown:

```
try
{
	MyService myService = new("http://api.exmample.com");
	User[] users = await myService.GetUsers();
}
catch (RestException ex)
{
	Console.WriteLine(ex.StatusCode);
	Console.WriteLine(ex.Content); // The HTML body
}
```

</details>

<details>
<summary>RestRequestOptions</summary>

Modify the properties in `RestClient.RequestOptions` to configure formats, etc. of REST request:

```
public class MyService : RestClient
{
	public MyService(string baseUrl) : base(baseUrl)
	{
		RequestOptions.QueryParameterDateTimeFormat = "dd.MM.yyyy HH:mm:ss";
		RequestOptions.QueryParameterDateOnlyFormat = "dd.MM.yyyy";
		RequestOptions.QueryParameterTimeOnlyFormat = "HH:mm:ss";
	}
}
```

The configured formats are used in `RestRequest.QueryParameter`.
</details>

## Changelog

### 5.0.0 (15.02.2026)

* **change:** Targeting .NET 10.0
* **new:** `RestRequest` extended all methods with `CancellationToken` parameter

### 4.0.0 (15.09.2025)

* **change:** Targeting .NET 9.0
* **new:** Server-sent events using `RestRequest.ReadEvents`, method
* **new:** `RestClient.DisableCertificateValidation` property
* **new:** `RestRequest.RestClient`, `HttpMethod`, and `Url` properties
* **new:** `RestRequest.Execute`, method
* **new:** `BytecodeApi.Rest.GenericRestClient` class

### 3.0.3 (28.11.2024)

* **new:** `RestRequest.Header` method

### 3.0.2 (27.07.2024)

* **new:** `RestRequest.ReadFile` method

### 3.0.1 (10.12.2023)

* **new:** `RestClient.RequestOptions` property
* **new:** `RestRequest.MultipartFileContent` and `MultipartStringContent` methods
* **new:** `RestRequest.ReadByteArray` with progress callback
* **new:** `RestRequestOptions` class

### 3.0.0 (08.09.2023)

* Initial release