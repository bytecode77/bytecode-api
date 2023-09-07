using BytecodeApi.Extensions;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace BytecodeApi.IO;

/// <summary>
/// Provides a set of <see langword="static" /> methods for network operations.
/// </summary>
public static class Network
{
	/// <summary>
	/// Sets up the <see cref="ServicePointManager.ServerCertificateValidationCallback" /> to validate all certificates without checking.
	/// </summary>
	public static void DisableCertificateValidation()
	{
		ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
	}
	/// <summary>
	/// Globally enables all known security protocols.
	/// </summary>
	public static void EnableAllSecurityProtocols()
	{
		foreach (int protocol in new[]
		{
			48, // SecurityProtocolType.Ssl3
			192, // SecurityProtocolType.Tls
			768, // SecurityProtocolType.Tls11
			3072, // SecurityProtocolType.Tls12
			12288, // SecurityProtocolType.Tls13
		})
		{
			try
			{
				ServicePointManager.SecurityProtocol |= (SecurityProtocolType)protocol;
			}
			catch
			{
			}
		}
	}
	/// <summary>
	/// Sends a Wake-on-LAN magic packet containing the specified <see cref="PhysicalAddress" /> to UDP broadcast on port 9.
	/// <para>Packet bytes: FF FF FF FF FF FF | 16 repetitions of <see cref="PhysicalAddress" /></para>
	/// <para>Total number of bytes: 102.</para>
	/// </summary>
	/// <param name="physicalAddress">The <see cref="PhysicalAddress" /> which is contained within the UDP broadcast packet.</param>
	public static void WakeOnLan(PhysicalAddress physicalAddress)
	{
		WakeOnLan(physicalAddress, null);
	}
	/// <summary>
	/// Sends a Wake-on-LAN magic packet containing the specified <see cref="PhysicalAddress" /> to UDP broadcast on port 9 including a password.
	/// <para>Packet bytes: FF FF FF FF FF FF | 16 repetitions of <see cref="PhysicalAddress" /> | contents of <paramref name="password" /></para>
	/// <para>Total number of bytes: 102.</para>
	/// </summary>
	/// <param name="physicalAddress">The <see cref="PhysicalAddress" /> which is contained within the UDP broadcast packet.</param>
	/// <param name="password">The binary representation of the password. <paramref name="password" /> must either contain 4 or 6 bytes, or be <see langword="null" />.</param>
	public static void WakeOnLan(PhysicalAddress physicalAddress, byte[]? password)
	{
		Check.ArgumentNull(physicalAddress);
		Check.Argument(password == null || password.Length is 4 or 6, nameof(password), "The password must be either 4 or 6 bytes long, or null.");

		byte[] packet = Enumerable
			.Repeat<byte>(0xff, 6)
			.Concat(Enumerable.Repeat(physicalAddress.GetAddressBytes(), 16).SelectMany())
			.Concat(password ?? new byte[0])
			.ToArray();

		using UdpClient client = new();
		client.Connect(IPAddress.Broadcast, 9);
		client.Send(packet, packet.Length);
	}
}