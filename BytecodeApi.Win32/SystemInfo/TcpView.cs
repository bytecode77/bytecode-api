using BytecodeApi.Extensions;
using BytecodeApi.Interop;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BytecodeApi.Win32.SystemInfo;

/// <summary>
/// Provides a snapshot of the TCPView table for both TCP and UDP connections in both IPv4 and IPv6.
/// </summary>
public sealed class TcpView
{
	/// <summary>
	/// Gets all entries of the TCPView table.
	/// </summary>
	public ReadOnlyCollection<TcpViewEntry> Entries { get; private init; }

	private TcpView(IEnumerable<TcpViewEntry> entries)
	{
		Entries = entries.ToReadOnlyCollection();
	}
	/// <summary>
	/// Creates a new <see cref="TcpView" /> instance and loads a snapshot of the TCPView table.
	/// </summary>
	/// <returns>
	/// The <see cref="TcpView" /> this method creates.
	/// </returns>
	public static TcpView Load()
	{
		ProtocolMapping protocolMap = ProtocolMapping.Load();

		List<TcpViewEntry> entries =
		[
			.. GetConnections<Native.TcpTable, Native.TcpRow>(false, 2)
				.Select(row => new TcpViewEntry
				{
					Protocol = TransportProtocolAndVersion.Tcp4,
					LocalAddress = new(BitConverter.GetBytes(row.LocalAddress)),
					LocalPort = BitConverter.ToUInt16([row.LocalPort[1], row.LocalPort[0]], 0),
					RemoteAddress = new(BitConverter.GetBytes(row.RemoteAddress)),
					RemotePort = BitConverter.ToUInt16([row.RemotePort[1], row.RemotePort[0]], 0),
					TcpState = (TcpState)row.State,
					ProcessId = (int)row.OwningProcessId
				}),
			.. GetConnections<Native.Tcp6Table, Native.Tcp6Row>(false, 23)
				.Select(row => new TcpViewEntry
				{
					Protocol = TransportProtocolAndVersion.Tcp6,
					LocalAddress = new(row.LocalAddress),
					LocalPort = BitConverter.ToUInt16([row.LocalPort[1], row.LocalPort[0]], 0),
					RemoteAddress = new(row.RemoteAddress),
					RemotePort = BitConverter.ToUInt16([row.RemotePort[1], row.RemotePort[0]], 0),
					TcpState = (TcpState)row.State,
					ProcessId = (int)row.OwningProcessId
				}),
			.. GetConnections<Native.UdpTable, Native.UdpRow>(true, 2)
				.Select(row => new TcpViewEntry
				{
					Protocol = TransportProtocolAndVersion.Udp4,
					LocalAddress = new(row.LocalAddress),
					LocalPort = BitConverter.ToUInt16([row.LocalPort[1], row.LocalPort[0]], 0),
					ProcessId = (int)row.OwningProcessId
				}),
			.. GetConnections<Native.Udp6Table, Native.Udp6Row>(true, 23)
				.Select(row => new TcpViewEntry
				{
					Protocol = TransportProtocolAndVersion.Udp6,
					LocalAddress = new(row.LocalAddress),
					LocalPort = (int)row.LocalPort,
					ProcessId = (int)row.OwningProcessId
				})
		];

		foreach (TcpViewEntry entry in entries)
		{
			entry.LocalProtocolName = GetProtocolName(entry.LocalPort, entry.Protocol);
			entry.RemoteProtocolName = GetProtocolName(entry.RemotePort, entry.Protocol);
		}

		return new(entries);

		TRow[] GetConnections<TTable, TRow>(bool udp, int version)
		{
			int bufferSize = 0;
			FieldInfo entryCountField = typeof(TTable).GetField("EntryCount") ?? throw Throw.InvalidOperation("Could not find field 'EntryCount'.");

			GetTable(0);
			using HGlobal tcpTable = new(bufferSize);

			if (GetTable(tcpTable.Handle) == 0)
			{
				TTable table = Marshal.PtrToStructure<TTable>(tcpTable.Handle) ?? throw Throw.Win32();
				int rowSize = Marshal.SizeOf<TRow>();
				uint entryCount = entryCountField.GetValue<uint>(table);

				TRow[] tableRows = new TRow[entryCount];
				nint ptr = tcpTable.Handle + 4;
				for (int i = 0; i < entryCount; i++, ptr += rowSize)
				{
					tableRows[i] = Marshal.PtrToStructure<TRow>(ptr) ?? throw Throw.Win32();
				}

				return tableRows;
			}
			else
			{
				return [];
			}

			uint GetTable(nint table)
			{
				return udp ? Native.GetExtendedUdpTable(table, ref bufferSize, true, version, 1) : Native.GetExtendedTcpTable(table, ref bufferSize, true, version, 5);
			}
		}
		string? GetProtocolName(int? port, TransportProtocolAndVersion protocol)
		{
			if (port != null)
			{
				TransportProtocol protocol2 = protocol is TransportProtocolAndVersion.Udp4 or TransportProtocolAndVersion.Udp6
					? TransportProtocol.Udp
					: TransportProtocol.Tcp;

				return protocolMap.Entries.FirstOrDefault(entry => entry.Protocol == protocol2 && entry.Port == port)?.Name;
			}
			else
			{
				return null;
			}
		}
	}
}

file static class Native
{
	[DllImport("iphlpapi.dll", SetLastError = true)]
	public static extern uint GetExtendedTcpTable(nint tcpTable, ref int bufferLength, bool sort, int ipVersion, int tableClass, uint reserved = 0);
	[DllImport("iphlpapi.dll", SetLastError = true)]
	public static extern uint GetExtendedUdpTable(nint udpTable, ref int bufferLength, bool sort, int ipVersion, int tableClass, uint reserved = 0);

	[StructLayout(LayoutKind.Sequential)]
	public struct TcpTable
	{
		public uint EntryCount;
		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
		public TcpRow[] Table;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct TcpRow
	{
		public uint State;
		public uint LocalAddress;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] LocalPort;
		public uint RemoteAddress;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] RemotePort;
		public uint OwningProcessId;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct Tcp6Table
	{
		public uint EntryCount;
		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
		public Tcp6Row[] Table;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct Tcp6Row
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] LocalAddress;
		public uint LocalScopeId;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] LocalPort;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] RemoteAddress;
		public uint RemoteScopeId;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] RemotePort;
		public uint State;
		public uint OwningProcessId;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct UdpTable
	{
		public uint EntryCount;
		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
		public UdpRow[] Table;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct UdpRow
	{
		public uint LocalAddress;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] LocalPort;
		public uint OwningProcessId;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct Udp6Table
	{
		public uint EntryCount;
		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
		public Udp6Row[] Table;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct Udp6Row
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] LocalAddress;
		public uint LocalScopeId;
		public uint LocalPort;
		public uint OwningProcessId;
	}
}