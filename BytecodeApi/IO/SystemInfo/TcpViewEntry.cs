using BytecodeApi.Extensions;
using BytecodeApi.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Provides information from the TCPView table for both TCP and UDP protocols in both IPv4 and IPv6.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class TcpViewEntry
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay => CSharp.DebuggerDisplay<TcpViewEntry>("Protocol = {0}, Local: {1}, Remote: {2}", Protocol, new QuotedString(LocalAddress + ":" + (LocalProtocolName ?? LocalPort.ToString())), new QuotedString(RemoteAddress == null ? null : RemoteAddress + ":" + (RemoteProtocolName ?? RemotePort?.ToString())));
		private static ProtocolMappingEntry[] ProtocolMap;
		/// <summary>
		/// Gets the protocol that is associated with this instance.
		/// </summary>
		public TcpViewEntryProtocol Protocol { get; private set; }
		/// <summary>
		/// Gets the local IP address that is associated with this instance.
		/// </summary>
		public IPAddress LocalAddress { get; private set; }
		/// <summary>
		/// Gets the local port that is associated with this instance.
		/// </summary>
		public int LocalPort { get; private set; }
		/// <summary>
		/// Gets the name of the protocol of the local port that is associated with this instance, if the resolveProtocolNames parameter was set to <see langword="true" /> in <see cref="GetEntries()" />. Protocol names are resolved according to the services file in %systemroot%\drivers\etc\services.
		/// </summary>
		public string LocalProtocolName { get; private set; }
		/// <summary>
		/// Gets the remote IP address that is associated with this instance.
		/// </summary>
		public IPAddress RemoteAddress { get; private set; }
		/// <summary>
		/// Gets the remote port that is associated with this instance.
		/// </summary>
		public int? RemotePort { get; private set; }
		/// <summary>
		/// Gets the name of the protocol of the remote port that is associated with this instance, if the resolveProtocolNames parameter was set to <see langword="true" /> in <see cref="GetEntries()" />. Protocol names are resolved according to the services file in %systemroot%\drivers\etc\services.
		/// </summary>
		public string RemoteProtocolName { get; private set; }
		/// <summary>
		/// Gets the TCP state that is associated with this instance and returns <see langword="null" />, if this <see cref="TcpViewEntry" /> is a UDP connection.
		/// </summary>
		public TcpState? TcpState { get; private set; }
		/// <summary>
		/// Gets the PID of the process that is associated with this instance.
		/// </summary>
		public int ProcessId { get; private set; }

		private TcpViewEntry()
		{
		}
		/// <summary>
		/// Gets the entire table of <see cref="TcpViewEntry" /> objects that represent a snapshot of the current TCP and UDP connections in both IPv4 and IPv6. Protocol names are resolved according to the services file in %systemroot%\drivers\etc\services.
		/// </summary>
		/// <returns>
		/// A new <see cref="TcpViewEntry" />[] object with the TCP and UDP table in both IPv4 and IPv6. Protocol names are resolved according to the services file in %systemroot%\drivers\etc\services.
		/// </returns>
		public static TcpViewEntry[] GetEntries()
		{
			return GetEntries(true, true, true, true);
		}
		/// <summary>
		/// Gets a specified portion of the table of <see cref="TcpViewEntry" /> objects that represent a snapshot of the current TCP and/or UDP connections in either IPv4, IPv6 or both. Protocol names are resolved according to the services file in %systemroot%\drivers\etc\services.
		/// </summary>
		/// <param name="tcp4"><see langword="true" /> to include TCPv4 entries.</param>
		/// <param name="tcp6"><see langword="true" /> to include TCPv6 entries.</param>
		/// <param name="udp4"><see langword="true" /> to include UDPv4 entries.</param>
		/// <param name="udp6"><see langword="true" /> to include UDPv6 entries.</param>
		/// <returns>
		/// A new <see cref="TcpViewEntry" />[] object with the TCP and/or UDP table in either IPv4, IPv6 or both. Protocol names are resolved according to the services file in %systemroot%\drivers\etc\services.
		/// </returns>
		public static TcpViewEntry[] GetEntries(bool tcp4, bool tcp6, bool udp4, bool udp6)
		{
			return GetEntries(tcp4, tcp6, udp4, udp6, true);
		}
		/// <summary>
		/// Gets a specified portion of the table of <see cref="TcpViewEntry" /> objects that represent a snapshot of the current TCP and/or UDP connections in either IPv4, IPv6 or both. If <paramref name="resolveProtocolNames" /> is set to <see langword="true" />, Protocol names are resolved according to the services file in %systemroot%\drivers\etc\services.
		/// </summary>
		/// <param name="tcp4"><see langword="true" /> to include TCPv4 entries.</param>
		/// <param name="tcp6"><see langword="true" /> to include TCPv6 entries.</param>
		/// <param name="udp4"><see langword="true" /> to include UDPv4 entries.</param>
		/// <param name="udp6"><see langword="true" /> to include UDPv6 entries.</param>
		/// <param name="resolveProtocolNames"><see langword="true" /> to resolve Protocol names according to the services file in %systemroot%\drivers\etc\services.</param>
		/// <returns>
		/// A new <see cref="TcpViewEntry" />[] object with the TCP and/or UDP table in either IPv4, IPv6 or both. If <paramref name="resolveProtocolNames" /> is set to <see langword="true" />, Protocol names are resolved according to the services file in %systemroot%\drivers\etc\services.
		/// </returns>
		public static TcpViewEntry[] GetEntries(bool tcp4, bool tcp6, bool udp4, bool udp6, bool resolveProtocolNames)
		{
			List<TcpViewEntry> entries = new List<TcpViewEntry>();

			if (tcp4)
			{
				entries.AddRange
				(
					GetConnections<Native.TcpTable, Native.TcpRow>(false, 2)
					.Select(row => new TcpViewEntry
					{
						Protocol = TcpViewEntryProtocol.Tcp4,
						LocalAddress = new IPAddress(BitConverter.GetBytes(row.LocalAddress)),
						LocalPort = BitConverter.ToUInt16(new[] { row.LocalPort[1], row.LocalPort[0] }, 0),
						RemoteAddress = new IPAddress(BitConverter.GetBytes(row.RemoteAddress)),
						RemotePort = BitConverter.ToUInt16(new[] { row.RemotePort[1], row.RemotePort[0] }, 0),
						TcpState = (TcpState)row.State,
						ProcessId = (int)row.OwningProcessId
					})
				);
			}
			if (tcp6)
			{
				entries.AddRange
				(
					GetConnections<Native.Tcp6Table, Native.Tcp6Row>(false, 23)
						.Select(row => new TcpViewEntry
						{
							Protocol = TcpViewEntryProtocol.Tcp6,
							LocalAddress = new IPAddress(row.LocalAddress),
							LocalPort = BitConverter.ToUInt16(new[] { row.LocalPort[1], row.LocalPort[0] }, 0),
							RemoteAddress = new IPAddress(row.RemoteAddress),
							RemotePort = BitConverter.ToUInt16(new[] { row.RemotePort[1], row.RemotePort[0] }, 0),
							TcpState = (TcpState)row.State,
							ProcessId = (int)row.OwningProcessId
						})
				);
			}
			if (udp4)
			{
				entries.AddRange
				(
					GetConnections<Native.UdpTable, Native.UdpRow>(true, 2)
						.Select(row => new TcpViewEntry
						{
							Protocol = TcpViewEntryProtocol.Udp4,
							LocalAddress = new IPAddress(row.LocalAddress),
							LocalPort = BitConverter.ToUInt16(new[] { row.LocalPort[1], row.LocalPort[0] }, 0),
							ProcessId = (int)row.OwningProcessId
						})
				);
			}
			if (udp6)
			{
				entries.AddRange
				(
					GetConnections<Native.Udp6Table, Native.Udp6Row>(true, 23)
						.Select(row => new TcpViewEntry
						{
							Protocol = TcpViewEntryProtocol.Udp6,
							LocalAddress = new IPAddress(row.LocalAddress),
							LocalPort = (int)row.LocalPort,
							ProcessId = (int)row.OwningProcessId
						})
				);
			}

			return entries
				.Select(entry =>
				{
					entry.LocalProtocolName = ResolvePort(entry.LocalPort, CSharp.EqualsAny(entry.Protocol, TcpViewEntryProtocol.Udp4, TcpViewEntryProtocol.Udp6));
					entry.RemoteProtocolName = entry.RemotePort == null ? null : ResolvePort(entry.RemotePort.Value, CSharp.EqualsAny(entry.Protocol, TcpViewEntryProtocol.Udp4, TcpViewEntryProtocol.Udp6));
					return entry;
				})
				.ToArray();

			TRow[] GetConnections<TTable, TRow>(bool udp, int version)
			{
				int bufferSize = 0;
				FieldInfo numEntriesField = typeof(TTable).GetField("EntryCount");

				GetTable(IntPtr.Zero);
				IntPtr tcpTable = Marshal.AllocHGlobal(bufferSize);

				try
				{
					if (GetTable(tcpTable) == 0)
					{
						TTable table = Marshal.PtrToStructure<TTable>(tcpTable);
						int rowSize = Marshal.SizeOf<TRow>();
						uint entryCount = numEntriesField.GetValue<uint>(table);

						TRow[] tableRows = new TRow[entryCount];
						IntPtr ptr = tcpTable + 4;
						for (int i = 0; i < entryCount; i++, ptr += rowSize) tableRows[i] = Marshal.PtrToStructure<TRow>(ptr);
						return tableRows;
					}
					else
					{
						return new TRow[0];
					}
				}
				finally
				{
					Marshal.FreeHGlobal(tcpTable);
				}

				uint GetTable(IntPtr table) => udp ? Native.GetExtendedUdpTable(table, ref bufferSize, true, version, 1) : Native.GetExtendedTcpTable(table, ref bufferSize, true, version, 5);
			}
			string ResolvePort(int port, bool udp)
			{
				if (resolveProtocolNames)
				{
					if (ProtocolMap == null) ProtocolMap = ProtocolMappingEntry.GetProtocolMappingEntries();
					return ProtocolMap.FirstOrDefault(entry => entry.Protocol == (udp ? ProtocolMappingProtocol.Udp : ProtocolMappingProtocol.Tcp) && entry.Port == port)?.Name;
				}
				else
				{
					return null;
				}
			}
		}
		/// <summary>
		/// Invalidates the internal protocol map that is retrieved using the <see cref="ProtocolMappingEntry" /> class and causes it to be reloaded when <see cref="GetEntries()" /> is called.
		/// </summary>
		public static void InvalidateProtocolMapping()
		{
			ProtocolMap = null;
		}
	}
}