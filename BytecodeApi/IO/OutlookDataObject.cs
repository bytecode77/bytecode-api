using BytecodeApi.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using IComDataObject = System.Runtime.InteropServices.ComTypes.IDataObject;
using IWindowsDataObject = System.Windows.IDataObject;
using StatStg = System.Runtime.InteropServices.ComTypes.STATSTG;

namespace BytecodeApi.IO
{
	/// <summary>
	/// Implements the data transfer mechanism for Microsoft Outlook. This class is typically used in drag&amp;drop operations from Outlook into a UI application.
	/// </summary>
	public class OutlookDataObject : IWindowsDataObject
	{
		private readonly IWindowsDataObject DataObject;
		private readonly IComDataObject ComDataObject;
		private readonly IWindowsDataObject OleDataObject;
		private readonly MethodInfo GetDataFromHGlobalMethod;

		/// <summary>
		/// Initializes a new instance of the <see cref="OutlookDataObject" /> class and adds the specified object to it.
		/// </summary>
		/// <param name="dataObject">The data to store.</param>
		public OutlookDataObject(IWindowsDataObject dataObject)
		{
			Check.ArgumentNull(dataObject, nameof(dataObject));
			Check.InvalidCast(dataObject is IComDataObject, nameof(dataObject));

			DataObject = dataObject;
			ComDataObject = (IComDataObject)DataObject;
			OleDataObject = DataObject.GetType().GetField("_innerData", BindingFlags.NonPublic | BindingFlags.Instance).GetValue<IWindowsDataObject>(DataObject);
			GetDataFromHGlobalMethod = OleDataObject.GetType().GetMethod("GetDataFromHGLOBLAL", BindingFlags.NonPublic | BindingFlags.Instance);
		}

		/// <summary>
		/// Returns the data associated with the specified class type format.
		/// </summary>
		/// <param name="format">A <see cref="Type" /> representing the format of the data to retrieve.</param>
		/// <returns>
		/// The data associated with the specified format, or <see langword="null" />.
		/// </returns>
		public object GetData(Type format)
		{
			Check.ArgumentNull(format, nameof(format));

			return GetData(format.FullName);
		}
		/// <summary>
		/// Returns the data associated with the specified data format.
		/// </summary>
		/// <param name="format">A <see cref="string" /> that specifies the format of the data to retrieve.</param>
		/// <returns>
		/// The data associated with the specified format, or <see langword="null" />.
		/// </returns>
		public object GetData(string format)
		{
			return GetData(format, true);
		}
		/// <summary>
		/// Returns the data associated with the specified data format, using an automated conversion parameter to determine whether to convert the data to the format.
		/// </summary>
		/// <param name="format">A <see cref="string" /> that specifies the format of the data to retrieve.</param>
		/// <param name="autoConvert"><see langword="true" /> to the convert data to the specified format; otherwise, <see langword="false" />.</param>
		/// <returns>
		/// The data associated with the specified format, or <see langword="null" />.
		/// </returns>
		public object GetData(string format, bool autoConvert)
		{
			Check.ArgumentNull(format, nameof(format));

			if (format == "FileGroupDescriptor")
			{
				IntPtr fileGroupDescriptorPointer = IntPtr.Zero;
				try
				{
					byte[] fileGroupDescriptorBytes;
					using (MemoryStream memoryStream = (MemoryStream)DataObject.GetData("FileGroupDescriptor", autoConvert))
					{
						fileGroupDescriptorBytes = memoryStream.ToArray();
					}

					fileGroupDescriptorPointer = Marshal.AllocHGlobal(fileGroupDescriptorBytes.Length);
					Marshal.Copy(fileGroupDescriptorBytes, 0, fileGroupDescriptorPointer, fileGroupDescriptorBytes.Length);

					Native.FileGroupDescriptorA fileGroupDescriptor = Marshal.PtrToStructure<Native.FileGroupDescriptorA>(fileGroupDescriptorPointer);
					string[] fileNames = new string[fileGroupDescriptor.ItemCount];
					IntPtr ptr = fileGroupDescriptorPointer + Marshal.SizeOf(fileGroupDescriptorPointer);

					for (int i = 0; i < fileGroupDescriptor.ItemCount; i++)
					{
						Native.FileDescriptorA fileDescriptor = Marshal.PtrToStructure<Native.FileDescriptorA>(ptr);
						fileNames[i] = fileDescriptor.FileName;
						ptr += Marshal.SizeOf(fileDescriptor);
					}

					return fileNames;
				}
				finally
				{
					Marshal.FreeHGlobal(fileGroupDescriptorPointer);
				}
			}
			else if (format == "FileGroupDescriptorW")
			{
				IntPtr fileGroupDescriptorPointer = IntPtr.Zero;
				try
				{
					byte[] fileGroupDescriptorBytes;
					using (MemoryStream memoryStream = (MemoryStream)DataObject.GetData("FileGroupDescriptorW"))
					{
						fileGroupDescriptorBytes = memoryStream.ToArray();
					}

					fileGroupDescriptorPointer = Marshal.AllocHGlobal(fileGroupDescriptorBytes.Length);
					Marshal.Copy(fileGroupDescriptorBytes, 0, fileGroupDescriptorPointer, fileGroupDescriptorBytes.Length);

					Native.FileGroupDescriptorW fileGroupDescriptor = Marshal.PtrToStructure<Native.FileGroupDescriptorW>(fileGroupDescriptorPointer);
					string[] fileNames = new string[fileGroupDescriptor.ItemCount];
					IntPtr ptr = fileGroupDescriptorPointer + Marshal.SizeOf(fileGroupDescriptorPointer);

					for (int i = 0; i < fileGroupDescriptor.ItemCount; i++)
					{
						Native.FileDescriptorW fileDescriptor = Marshal.PtrToStructure<Native.FileDescriptorW>(ptr);
						fileNames[i] = fileDescriptor.FileName;
						ptr += Marshal.SizeOf(fileDescriptor);
					}

					return fileNames;
				}
				finally
				{
					Marshal.FreeHGlobal(fileGroupDescriptorPointer);
				}
			}
			else if (format == "FileContents")
			{
				return ((string[])GetData("FileGroupDescriptor"))
					.Select((file, i) => GetData(format, i))
					.ToArray();
			}
			else
			{
				return DataObject.GetData(format, autoConvert);
			}
		}
		/// <summary>
		/// Returns the data associated with the specified data format at the specified index.
		/// </summary>
		/// <param name="format">A <see cref="string" /> that specifies the format of the data to retrieve.</param>
		/// <param name="index">A <see cref="int" /> value specifying the index at which to retrieve the data object from.</param>
		/// <returns>
		/// The data associated with the specified format from at the specified index, or <see langword="null" />.
		/// </returns>
		public byte[] GetData(string format, int index)
		{
			Check.ArgumentNull(format, nameof(format));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(index, nameof(index));

			FORMATETC formatEtc = new FORMATETC
			{
				cfFormat = (short)DataFormats.GetFormat(format).Id,
				dwAspect = DVASPECT.DVASPECT_CONTENT,
				lindex = index,
				ptd = IntPtr.Zero,
				tymed = TYMED.TYMED_ISTREAM | TYMED.TYMED_ISTORAGE | TYMED.TYMED_HGLOBAL
			};

			STGMEDIUM medium;
			ComDataObject.GetData(ref formatEtc, out medium);

			if (medium.tymed == TYMED.TYMED_ISTORAGE)
			{
				Native.IStorage storage = null;
				Native.IStorage storage2 = null;
				Native.ILockBytes lockBytes = null;
				try
				{
					storage = (Native.IStorage)Marshal.GetObjectForIUnknown(medium.unionmember);
					Marshal.Release(medium.unionmember);
					lockBytes = Native.CreateILockBytesOnHGlobal(IntPtr.Zero, true);
					storage2 = Native.StgCreateDocfileOnILockBytes(lockBytes, 0x1012);

					storage.CopyTo(0, null, IntPtr.Zero, storage2);
					lockBytes.Flush();
					storage2.Commit(0);

					StatStg lockBytesStat;
					lockBytes.Stat(out lockBytesStat, 1);
					int lockBytesSize = (int)lockBytesStat.cbSize;

					byte[] lockBytesContent = new byte[lockBytesSize];
					lockBytes.ReadAt(0, lockBytesContent, lockBytesContent.Length, null);
					return lockBytesContent;
				}
				finally
				{
					Marshal.ReleaseComObject(storage2);
					Marshal.ReleaseComObject(lockBytes);
					Marshal.ReleaseComObject(storage);
				}
			}
			else if (medium.tymed == TYMED.TYMED_ISTREAM)
			{
				IStream stream = null;
				try
				{
					stream = (IStream)Marshal.GetObjectForIUnknown(medium.unionmember);
					Marshal.Release(medium.unionmember);

					StatStg streamStat;
					stream.Stat(out streamStat, 0);
					int streamSize = (int)streamStat.cbSize;

					byte[] streamContent = new byte[streamSize];
					stream.Read(streamContent, streamContent.Length, IntPtr.Zero);
					return streamContent;
				}
				finally
				{
					Marshal.ReleaseComObject(stream);
				}
			}
			if (medium.tymed == TYMED.TYMED_HGLOBAL)
			{
				using (MemoryStream memoryStream = GetDataFromHGlobalMethod.Invoke<MemoryStream>(OleDataObject, new object[] { DataFormats.GetFormat(formatEtc.cfFormat).Name, medium.unionmember }))
				{
					return memoryStream.ToArray();
				}
			}
			else
			{
				return null;
			}
		}
		/// <summary>
		/// Checks to see whether the data is available in, or can be converted to, a specified format. The data format is specified by a <see cref="Type" /> object.
		/// </summary>
		/// <param name="format">A <see cref="Type" /> that specifies what format to check for.</param>
		/// <returns>
		/// <see langword="true" />, if the data is in, or can be converted to, the specified format;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool GetDataPresent(Type format)
		{
			Check.ArgumentNull(format, nameof(format));

			return DataObject.GetDataPresent(format);
		}
		/// <summary>
		/// Checks to see whether the data is available in, or can be converted to, a specified format; the data format is specified by a <see cref="string" />.
		/// </summary>
		/// <param name="format">A <see cref="string" /> that specifies what format to check for.</param>
		/// <returns>
		/// <see langword="true" />, if the data is in, or can be converted to, the specified format;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool GetDataPresent(string format)
		{
			Check.ArgumentNull(format, nameof(format));

			return DataObject.GetDataPresent(format);
		}
		/// <summary>
		/// Checks to see whether the data is available in, or can be converted to, a specified format. A <see cref="bool" /> flag indicates whether to check if the data can be converted to the specified format, if it is not available in that format.
		/// </summary>
		/// <param name="format">A <see cref="string" /> that specifies what format to check for.</param>
		/// <param name="autoConvert"><see langword="false" /> to only check for the specified format; <see langword="true" /> to also check whether or not data stored in this data object can be converted to the specified format.</param>
		/// <returns>
		/// <see langword="true" />, if the data is in, or can be converted to, the specified format;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool GetDataPresent(string format, bool autoConvert)
		{
			Check.ArgumentNull(format, nameof(format));

			return DataObject.GetDataPresent(format, autoConvert);
		}
		/// <summary>
		/// Returns a list of all formats that the data in this data object is stored in, or can be converted to.
		/// </summary>
		/// <returns>
		/// A <see cref="string" />[], with each <see cref="string" /> specifying the name of a format supported by this data object.
		/// </returns>
		public string[] GetFormats()
		{
			return DataObject.GetFormats();
		}
		/// <summary>
		/// Returns a list of all formats that the data in this data object is stored in. A <see cref="bool" /> flag indicates whether or not to also include formats that the data can be automatically converted to.
		/// </summary>
		/// <param name="autoConvert"><see langword="true" /> to retrieve all formats that data stored in this data object is stored in, or can be converted to; <see langword="false" /> to retrieve only formats that data stored in this data object is stored in (excluding formats that the data is not stored in, but can be automatically converted to).</param>
		/// <returns>
		/// A <see cref="string" />[], with each <see cref="string" /> specifying the name of a format supported by this data object.
		/// </returns>
		public string[] GetFormats(bool autoConvert)
		{
			return DataObject.GetFormats(autoConvert);
		}
		/// <summary>
		/// Stores the specified data in this data object, automatically converting the data format from the source object type.
		/// </summary>
		/// <param name="data">The data to store in this data object.</param>
		public void SetData(object data)
		{
			DataObject.SetData(data);
		}
		/// <summary>
		/// Stores the specified data in this data object, along with one or more specified data formats. The data format is specified by a <see cref="Type" /> class.
		/// </summary>
		/// <param name="format">A <see cref="Type" /> that specifies what format to store the data in.</param>
		/// <param name="data">The data to store in this data object.</param>
		public void SetData(Type format, object data)
		{
			Check.ArgumentNull(format, nameof(format));

			DataObject.SetData(format, data);
		}
		/// <summary>
		/// Stores the specified data in this data object, along with one or more specified data formats. The data format is specified by a <see cref="string" />.
		/// </summary>
		/// <param name="format">A <see cref="string" /> that specifies what format to store the data in.</param>
		/// <param name="data">The data to store in this data object.</param>
		public void SetData(string format, object data)
		{
			Check.ArgumentNull(format, nameof(format));

			DataObject.SetData(format, data);
		}
		/// <summary>
		/// Stores the specified data in this data object, along with one or more specified data formats. This overload includes a <see cref="bool" /> flag to indicate whether the data may be converted to another format on retrieval.
		/// </summary>
		/// <param name="format">A <see cref="string" /> that specifies what format to store the data in.</param>
		/// <param name="data">The data to store in this data object.</param>
		/// <param name="autoConvert"><see langword="true" /> to the convert data to the specified format; otherwise, <see langword="false" />.</param>
		public void SetData(string format, object data, bool autoConvert)
		{
			Check.ArgumentNull(format, nameof(format));

			DataObject.SetData(format, data, autoConvert);
		}
	}
}