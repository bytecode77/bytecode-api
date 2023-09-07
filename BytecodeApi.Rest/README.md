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