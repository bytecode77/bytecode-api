namespace BytecodeApi.IO
{
	/// <summary>
	/// Specifies the elevation type for a Windows user.
	/// </summary>
	public enum ElevationType
	{
		/// <summary>
		/// The token does not have a linked token (standard user, elevation requires credentials from an administrative user account).
		/// </summary>
		Default = 1,
		/// <summary>
		/// The token is an elevated token (integrity level is high or above).
		/// </summary>
		Full = 2,
		/// <summary>
		/// The token is a limited token (integrity level is medium, but the user is a local administrator and can elevate with consent).
		/// </summary>
		Limited = 3
	}
}