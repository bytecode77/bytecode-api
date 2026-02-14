# BytecodeApi.Rest

Fluent REST client.

## Examples

See: [Examples](https://github.com/bytecode77/bytecode-api/blob/master/BytecodeApi.Rest/README.md)

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