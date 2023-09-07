using System.Diagnostics;

namespace BytecodeApi.IO;

/// <summary>
/// Specifies the mandatory integrity level for a <see cref="Process" />.
/// </summary>
public enum ProcessIntegrityLevel
{
	/// <summary>
	/// The <see cref="Process" /> is running with untrusted integrity (SECURITY_MANDATORY_UNTRUSTED_RID).
	/// </summary>
	Untrusted = 0,
	/// <summary>
	/// The <see cref="Process" /> is running with low integrity (SECURITY_MANDATORY_LOW_RID).
	/// </summary>
	Low = 0x1000,
	/// <summary>
	/// The <see cref="Process" /> is running with medium integrity (SECURITY_MANDATORY_MEDIUM_RID or SECURITY_MANDATORY_MEDIUM_PLUS_RID).
	/// </summary>
	Medium = 0x2000,
	/// <summary>
	/// The <see cref="Process" /> is running with high integrity (SECURITY_MANDATORY_HIGH_RID).
	/// </summary>
	High = 0x3000,
	/// <summary>
	/// The <see cref="Process" /> is running with system integrity (SECURITY_MANDATORY_SYSTEM_RID).
	/// </summary>
	System = 0x4000
}