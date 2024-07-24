# BytecodeApi.Cryptography

Library for encryption & hashing and other cryptographic operations.

## Examples

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Cryptography

<details>
<summary>Hashes</summary>

Compute hash of a `string`:

```
const string str = "Hello, World!";

string hash = Hashes.Compute(str, HashType.SHA256);
byte[] hashBytes = Hashes.ComputeBytes(str, HashType.SHA256);
```

Compute hash of a `byte[]`:

```
byte[] buffer = new byte[] { 1, 2, 3, 4, 5 };

string hash = Hashes.Compute(buffer, HashType.SHA256);
byte[] hashBytes = Hashes.ComputeBytes(buffer, HashType.SHA256);
```
</details>

<details>
<summary>Encryption</summary>

The `Encryption` class encrypts raw `byte[]` buffers using AES.

```
// The payload to encrypt:
byte[] data = new byte[] { 1, 2, 3, 4, 5, 6, 7 };

// Generate random IV:
byte[] iv = Encryption.GenerateIV();

// Derive key from a password, hashed 1000 times:
byte[] key = Hashes.ComputeBytes("password", HashType.SHA256, 1000);

// Encrypt a byte[] using the key & IV
byte[] encrypted = Encryption.Encrypt(data, iv, key);

// Decrypt data using the same key and IV
byte[] decrypted = Encryption.Decrypt(encrypted, iv, key);
```
</details>

<details>
<summary>ContentEncryption</summary>

The `ContentEncryption` class encrypts `byte[]` buffers with a password rather than a raw IV and key.

The resulting `byte[]` contains the IV and information about how many times the password was hashed. Therefore, only the password needs to be provided for decryption.

```
// The payload to encrypt:
byte[] data = new byte[] { 1, 2, 3, 4, 5, 6, 7 };

// Password
const string password = "secret";

// Encrypt using the password and hash the password 1000 times:
byte[] encrypted = ContentEncryption.Encrypt(data, password, 1000);

// Decrypt data using the password:
// The encrypted byte[] contains the IV and information about how many times the password was hashed.
// Therefore, only the password is needed.
byte[] decrypted = ContentEncryption.Decrypt(encrypted, password);
```
</details>

<details>
<summary>AsymmetricEncryption</summary>

The `AsymmetricEncryption` class encrypts and decrypts data asymmetrically using RSA.

The maximum amount of data that can be encrypted depends on the RSA key size. To encrypt any amount of data, use the `AsymmetricContentEncryption` class.

```
// The payload to encrypt:
byte[] data = new byte[] { 1, 2, 3, 4, 5, 6, 7 };

// Generate public/private key pair:
AsymmetricEncryption.GenerateKeyPair(out RSAParameters publicKey, out RSAParameters privateKey);

// Encrypt using the public key:
byte[] encrypted = AsymmetricEncryption.Encrypt(data, publicKey);

// Decrypt using the private key
byte[] decrypted = AsymmetricEncryption.Decrypt(encrypted, privateKey);
```
</details>

<details>
<summary>AsymmetricContentEncryption</summary>

This class encrypts a randomly generated AES key with an RSA key and the data with the AES key.

This is a typical approach, because RSA can only encrypt a certain amount of data, depending on the RSA key size.

```
// The payload to encrypt:
byte[] data = new byte[100]; // Can be any size
for (int i = 0; i < data.Length; i++)
{
	data[i] = 123;
}

// Generate public/private key pair
AsymmetricEncryption.GenerateKeyPair(out RSAParameters publicKey, out RSAParameters privateKey);

// Encrypt using the public key
byte[] encrypted = AsymmetricContentEncryption.Encrypt(data, publicKey);

// Decrypt using the private key
byte[] decrypted = AsymmetricContentEncryption.Decrypt(encrypted, privateKey);
```
</details>

<details>
<summary>BloomFilter</summary>

This `BloomFilter` implementation supports custom sizes, multiple hashes, and a custom hashing delegate:

```
// Create 1 Mbit bloom filter:
BloomFilter<string> bloom = new(1024 * 1024);

// The hashing function is CRC32:
bloom.HashFunctions.Add(str => BitConverter.ToUInt32(Hashes.ComputeBytes(str.ToUTF8Bytes(), HashType.CRC32)));

// Add values to the bloom filter:
bloom.Add("hello");
bloom.Add("world");

// Check, if values exist:
Console.WriteLine(bloom.Contains("hello") ? "maybe" : "no");
Console.WriteLine(bloom.Contains("world") ? "maybe" : "no");
Console.WriteLine(bloom.Contains("foobar") ? "maybe" : "no");
```
</details>

## Changelog

### 3.0.1 (10.12.2023)

* **new:** `MD2`, `MD4` and `SHA224` hash algorithms
* **change:** `CRC64` hash algorithm now uses ECMA 182

### 3.0.0 (08.09.2023)

* Initial release