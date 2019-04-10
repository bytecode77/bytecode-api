using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace BytecodeApi
{
	internal static class GeoIPHelper
	{
		public static bool TryConvertIPv4(IPAddress ipAddress, out uint address)
		{
			if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
			{
				byte[] bytes = CSharp.Try(() => ipAddress.MapToIPv4().GetAddressBytes());
				if (bytes?.Length == 4)
				{
					address = BitConverter.ToUInt32(bytes.Reverse().ToArray(), 0);
					return true;
				}
			}

			address = 0;
			return false;
		}
		public static bool TryConvertIPv6(IPAddress ipAddress, out byte[] address)
		{
			if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
			{
				byte[] bytes = CSharp.Try(() => ipAddress.GetAddressBytes());
				if (bytes?.Length == 16)
				{
					address = bytes;
					return true;
				}
			}

			address = null;
			return false;
		}
	}
}