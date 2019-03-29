namespace BytecodeApi.Cryptography
{
	/// <summary>
	/// Specifies the key type used for asymmetric encryption.
	/// </summary>
	public enum AsymmetricKeyType
	{
		/// <summary>
		/// The key is an RSA public key.
		/// </summary>
		Public,
		/// <summary>
		/// The key is an RSA private key.
		/// </summary>
		Private
	}
}