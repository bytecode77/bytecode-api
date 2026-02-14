using BytecodeApi.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace BytecodeApi.Cryptography.Tests.Comparers;

public sealed class RSAParametersEqualityComparer : IEqualityComparer<RSAParameters>
{
	public bool Equals(RSAParameters x, RSAParameters y)
	{
		return
			(x.D == null && y.D == null || x.D?.Compare(y.D) == true) &&
			(x.DP == null && y.DP == null || x.DP?.Compare(y.DP) == true) &&
			(x.DQ == null && y.DQ == null || x.DQ?.Compare(y.DQ) == true) &&
			(x.Exponent == null && y.Exponent == null || x.Exponent?.Compare(y.Exponent) == true) &&
			(x.InverseQ == null && y.InverseQ == null || x.InverseQ?.Compare(y.InverseQ) == true) &&
			(x.Modulus == null && y.Modulus == null || x.Modulus?.Compare(y.Modulus) == true) &&
			(x.P == null && y.P == null || x.P?.Compare(y.P) == true) &&
			(x.Q == null && y.Q == null || x.Q?.Compare(y.Q) == true);
	}

	public int GetHashCode([DisallowNull] RSAParameters obj)
	{
		return obj.GetHashCode();
	}
}