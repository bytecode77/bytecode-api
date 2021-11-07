using BytecodeApi.Extensions;
using System;
using System.Collections.Generic;
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
	public sealed class TcpView
	{
		private ProtocolMappingEntry[] ProtocolMap;
		/// <summary>
		/// <see langword="true" /> to resolve protocol names; <see langword="false" /> to only retrieve port numbers.
		/// </summary>
		public bool ResolveProtocolNames { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TcpView" /> class.
		/// </summary>
		public TcpView()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TcpView" /> class.
		/// </summary>
		/// <param name="resolveProtocolNames"><see langword="true" /> to resolve protocol names; <see langword="false" /> to only retrieve port numbers.</param>
		public TcpView(bool resolveProtocolNames) : this()
		{
			ResolveProtocolNames = resolveProtocolNames;
		}

		/// <summary>
		/// Gets the entire table of <see cref="TcpViewEntry" /> objects that represent a snapshot of the current TCP and UDP connections in both IPv4 and IPv6.
		/// </summary>
		/// <returns>
		/// A new <see cref="TcpViewEntry" />[] object with the TCP and UDP table in both IPv4 and IPv6.
		/// </returns>
		public TcpViewEntry[] GetEntries()
		{
			return GetEntries(true, true, true, true);
		}
		/// <summary>
		/// Gets a specified portion of the table of <see cref="TcpViewEntry" /> objects that represent a snapshot of the current TCP and/or UDP connections in either IPv4, IPv6 or both.
		/// </summary>
		/// <param name="tcp4"><see langword="true" /> to include TCPv4 entries.</param>
		/// <param name="tcp6"><see langword="true" /> to include TCPv6 entries.</param>
		/// <param name="udp4"><see langword="true" /> to include UDPv4 entries.</param>
		/// <param name="udp6"><see langword="true" /> to include UDPv6 entries.</param>
		/// <returns>
		/// A new <see cref="TcpViewEntry" />[] object with the TCP and/or UDP table in either IPv4, IPv6 or both.
		/// </returns>
		public TcpViewEntry[] GetEntries(bool tcp4, bool tcp6, bool udp4, bool udp6)
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
				.Each(entry =>
				{
					bool udp = CSharp.EqualsAny(entry.Protocol, TcpViewEntryProtocol.Udp4, TcpViewEntryProtocol.Udp6);
					entry.LocalProtocolName = GetProtocolName(entry.LocalPort, udp);
					entry.RemoteProtocolName = GetProtocolName(entry.RemotePort, udp);
				})
				.ToArray();

			TRow[] GetConnections<TTable, TRow>(bool udp, int version)
			{
				int bufferSize = 0;
				FieldInfo entryCountField = typeof(TTable).GetField("EntryCount");

				GetTable(IntPtr.Zero);
				IntPtr tcpTable = Marshal.AllocHGlobal(bufferSize);

				try
				{
					if (GetTable(tcpTable) == 0)
					{
						TTable table = Marshal.PtrToStructure<TTable>(tcpTable);
						int rowSize = Marshal.SizeOf<TRow>();
						uint entryCount = entryCountField.GetValue<uint>(table);

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
			string GetProtocolName(int? port, bool udp)
			{
				if (ResolveProtocolNames && port != null)
				{
					if (ProtocolMap == null) ProtocolMap = ProtocolMapping.GetEntries();
					return ProtocolMap.FirstOrDefault(entry => entry.Protocol == (udp ? ProtocolMappingProtocol.Udp : ProtocolMappingProtocol.Tcp) && entry.Port == port)?.Name;
				}
				else
				{
					return null;
				}
			}
		}
	}
}