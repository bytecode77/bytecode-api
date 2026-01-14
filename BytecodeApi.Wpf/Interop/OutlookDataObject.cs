using BytecodeApi.Data;
using BytecodeApi.Extensions;
using BytecodeApi.Interop;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using IComDataObject = System.Runtime.InteropServices.ComTypes.IDataObject;
using IDataObject = System.Windows.IDataObject;

namespace BytecodeApi.Wpf.Interop;

/// <summary>
/// Implements the data transfer mechanism for Microsoft Outlook. This class is typically used in drag&amp;drop operations from Outlook into a UI application.
/// </summary>
public sealed class OutlookDataObject : IDataObject
{
	private readonly IDataObject DataObject;
	private readonly IComDataObject ComDataObject;
	private readonly IDataObject OleDataObject;
	private readonly MethodInfo GetDataFromHGlobalMethod;

	/// <summary>
	/// Initializes a new instance of the <see cref="OutlookDataObject" /> class and adds the specified object to it.
	/// </summary>
	/// <param name="dataObject">The data to store.</param>
	public OutlookDataObject(IDataObject dataObject)
	{
		Check.ArgumentNull(dataObject);
		Check.InvalidCast(dataObject is IComDataObject, nameof(dataObject));

		DataObject = dataObject;
		ComDataObject = (IComDataObject)DataObject;

		OleDataObject = DataObject
			.GetType()
			.GetField("_innerData", BindingFlags.NonPublic | BindingFlags.Instance)
			?.GetValue<IDataObject>(DataObject) ?? throw Throw.Win32();

		GetDataFromHGlobalMethod = OleDataObject
			.GetType()
			.GetMethod("GetDataFromHGLOBAL", BindingFlags.NonPublic | BindingFlags.Instance) ?? throw Throw.Win32();
	}

	/// <summary>
	/// Returns the data associated with the specified class type format.
	/// </summary>
	/// <param name="format">A <see cref="Type" /> representing the format of the data to retrieve.</param>
	/// <returns>
	/// The data associated with the specified format, or <see langword="null" />.
	/// </returns>
	public object? GetData(Type format)
	{
		Check.ArgumentNull(format);

		return GetData(format.FullName!);
	}
	/// <summary>
	/// Returns the data associated with the specified data format.
	/// </summary>
	/// <param name="format">A <see cref="string" /> that specifies the format of the data to retrieve.</param>
	/// <returns>
	/// The data associated with the specified format, or <see langword="null" />.
	/// </returns>
	public object? GetData(string format)
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
	public object? GetData(string format, bool autoConvert)
	{
		Check.ArgumentNull(format);

		if (format == "FileGroupDescriptor")
		{
			byte[] fileGroupDescriptorBytes;
			using (MemoryStream memoryStream = (MemoryStream)DataObject.GetData("FileGroupDescriptor", autoConvert))
			{
				fileGroupDescriptorBytes = memoryStream.ToArray();
			}

			using HGlobal fileGroupDescriptorPtr = HGlobal.FromArray(fileGroupDescriptorBytes);
			Native.FileGroupDescriptorA fileGroupDescriptor = (Native.FileGroupDescriptorA?)Marshal.PtrToStructure(fileGroupDescriptorPtr.Handle, typeof(Native.FileGroupDescriptorA)) ?? throw Throw.Win32();

			string[] fileNames = new string[fileGroupDescriptor.ItemCount];
			nint fileDescriptorPointer = fileGroupDescriptorPtr.Handle + Marshal.SizeOf(fileGroupDescriptor.ItemCount);

			for (int i = 0; i < fileGroupDescriptor.ItemCount; i++)
			{
				Native.FileDescriptorA fileDescriptor = (Native.FileDescriptorA?)Marshal.PtrToStructure(fileDescriptorPointer, typeof(Native.FileDescriptorA)) ?? throw Throw.Win32();
				fileNames[i] = fileDescriptor.FileName ?? throw Throw.Win32();
				fileDescriptorPointer += Marshal.SizeOf(fileDescriptor);
			}

			return fileNames;
		}
		else if (format == "FileGroupDescriptorW")
		{
			byte[] fileGroupDescriptorBytes;
			using (MemoryStream memoryStream = (MemoryStream)DataObject.GetData("FileGroupDescriptorW"))
			{
				fileGroupDescriptorBytes = memoryStream.ToArray();
			}

			using HGlobal fileGroupDescriptorPtr = HGlobal.FromArray(fileGroupDescriptorBytes);
			Native.FileGroupDescriptorW fileGroupDescriptor = (Native.FileGroupDescriptorW?)Marshal.PtrToStructure(fileGroupDescriptorPtr.Handle, typeof(Native.FileGroupDescriptorW)) ?? throw Throw.Win32();

			string[] fileNames = new string[fileGroupDescriptor.ItemCount];
			nint fileDescriptorPointer = fileGroupDescriptorPtr.Handle + Marshal.SizeOf(fileGroupDescriptor.ItemCount);

			for (int i = 0; i < fileGroupDescriptor.ItemCount; i++)
			{
				Native.FileDescriptorW fileDescriptor = (Native.FileDescriptorW?)Marshal.PtrToStructure(fileDescriptorPointer, typeof(Native.FileDescriptorW)) ?? throw Throw.Win32();
				fileNames[i] = fileDescriptor.FileName ?? throw Throw.Win32();
				fileDescriptorPointer += Marshal.SizeOf(fileDescriptor);
			}

			return fileNames;
		}
		else if (format == "FileContents")
		{
			string formatName;
			if (GetDataPresent("FileGroupDescriptorW"))
			{
				formatName = "FileGroupDescriptorW";
			}
			else if (GetDataPresent("FileGroupDescriptor"))
			{
				formatName = "FileGroupDescriptor";
			}
			else
			{
				return null;
			}

			if (GetData(formatName) is string[] fileContentNames)
			{
				return Create.Array(fileContentNames.Length, i => GetData(format, i));
			}
			else
			{
				return null;
			}
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
	public MemoryStream? GetData(string format, int index)
	{
		Check.ArgumentNull(format);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(index);

		FORMATETC formatetc = new()
		{
			cfFormat = (short)DataFormats.GetFormat(format).Id,
			dwAspect = DVASPECT.DVASPECT_CONTENT,
			lindex = index,
			ptd = 0,
			tymed = TYMED.TYMED_ISTREAM | TYMED.TYMED_ISTORAGE | TYMED.TYMED_HGLOBAL
		};

		ComDataObject.GetData(ref formatetc, out STGMEDIUM medium);

		if (medium.tymed == TYMED.TYMED_ISTORAGE)
		{
			Native.IStorage? storage = null;
			Native.IStorage? storage2 = null;
			Native.ILockBytes? lockBytes = null;

			try
			{
				storage = (Native.IStorage)Marshal.GetObjectForIUnknown(medium.unionmember);
				Marshal.Release(medium.unionmember);

				lockBytes = Native.CreateILockBytesOnHGlobal(0, true);
				storage2 = Native.StgCreateDocfileOnILockBytes(lockBytes, 0x1012, 0);

				storage.CopyTo(0, null, 0, storage2);
				lockBytes.Flush();
				storage2.Commit(0);

				lockBytes.Stat(out STATSTG lockBytesStat, 1);

				byte[] content = new byte[lockBytesStat.cbSize];
				lockBytes.ReadAt(0, content, content.Length, null);
				return new(content);
			}
			finally
			{
				if (storage2 != null) Marshal.ReleaseComObject(storage2);
				if (lockBytes != null) Marshal.ReleaseComObject(lockBytes);
				if (storage != null) Marshal.ReleaseComObject(storage);
			}
		}
		else if (medium.tymed == TYMED.TYMED_ISTREAM)
		{
			IStream? stream = null;

			try
			{
				stream = (IStream)Marshal.GetObjectForIUnknown(medium.unionmember);
				Marshal.Release(medium.unionmember);

				stream.Stat(out STATSTG streamStat, 0);

				byte[] content = new byte[streamStat.cbSize];
				stream.Read(content, content.Length, 0);
				return new(content);
			}
			finally
			{
				if (stream != null) Marshal.ReleaseComObject(stream);
			}
		}
		else if (medium.tymed == TYMED.TYMED_HGLOBAL)
		{
			return GetDataFromHGlobalMethod.Invoke<MemoryStream>(OleDataObject, [DataFormats.GetFormat(formatetc.cfFormat).Name, medium.unionmember]);
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
		DataObject.SetData(format, data);
	}
	/// <summary>
	/// Stores the specified data in this data object, along with one or more specified data formats. The data format is specified by a <see cref="string" />.
	/// </summary>
	/// <param name="format">A <see cref="string" /> that specifies what format to store the data in.</param>
	/// <param name="data">The data to store in this data object.</param>
	public void SetData(string format, object data)
	{
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
		DataObject.SetData(format, data, autoConvert);
	}

	/// <summary>
	/// Returns the data as a collection of files.
	/// </summary>
	/// <returns>
	/// A new <see cref="BlobCollection" /> with all files.
	/// </returns>
	public BlobCollection GetFiles()
	{
		if (GetData("FileGroupDescriptor") is string[] fileNames)
		{
			BlobCollection files = [];

			for (int i = 0; i < fileNames.Length; i++)
			{
				using MemoryStream? memoryStream = GetData("FileContents", i) ?? throw Throw.Win32();
				files.Add(new(fileNames[i], memoryStream.ToArray()));
			}

			return files;
		}
		else
		{
			throw Throw.Win32();
		}
	}
}

file static class Native
{
	[DllImport("kernel32.dll")]
	public static extern nint GlobalLock(nint obj);
	[DllImport("ole32.dll", PreserveSig = false)]
	public static extern ILockBytes CreateILockBytesOnHGlobal(nint obj, bool deleteOnRelease);
	[DllImport("OLE32.DLL", CharSet = CharSet.Auto, PreserveSig = false)]
	public static extern nint GetHGlobalFromILockBytes(ILockBytes lockBytes);
	[DllImport("OLE32.DLL", CharSet = CharSet.Unicode, PreserveSig = false)]
	public static extern IStorage StgCreateDocfileOnILockBytes(ILockBytes lockBytes, uint mode, uint reserved);

	[ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000000B-0000-0000-C000-000000000046")]
	public interface IStorage
	{
		[return: MarshalAs(UnmanagedType.Interface)]
		IStream CreateStream([In, MarshalAs(UnmanagedType.BStr)] string name, [In, MarshalAs(UnmanagedType.U4)] int mode, [In, MarshalAs(UnmanagedType.U4)] int reserved1, [In, MarshalAs(UnmanagedType.U4)] int reserved2);
		[return: MarshalAs(UnmanagedType.Interface)]
		IStream OpenStream([In, MarshalAs(UnmanagedType.BStr)] string name, nint reserved1, [In, MarshalAs(UnmanagedType.U4)] int mode, [In, MarshalAs(UnmanagedType.U4)] int reserved2);
		[return: MarshalAs(UnmanagedType.Interface)]
		IStorage CreateStorage([In, MarshalAs(UnmanagedType.BStr)] string name, [In, MarshalAs(UnmanagedType.U4)] int mode, [In, MarshalAs(UnmanagedType.U4)] int reserved1, [In, MarshalAs(UnmanagedType.U4)] int reserved2);
		[return: MarshalAs(UnmanagedType.Interface)]
		IStorage OpenStorage([In, MarshalAs(UnmanagedType.BStr)] string name, nint priority, [In, MarshalAs(UnmanagedType.U4)] int mode, nint snbExclude, [In, MarshalAs(UnmanagedType.U4)] int reserved);
		void CopyTo(int ciidExclude, [In, MarshalAs(UnmanagedType.LPArray)] Guid[]? iidExclude, nint snbExclude, [In, MarshalAs(UnmanagedType.Interface)] IStorage dest);
		void MoveElementTo([In, MarshalAs(UnmanagedType.BStr)] string name, [In, MarshalAs(UnmanagedType.Interface)] IStorage dest, [In, MarshalAs(UnmanagedType.BStr)] string newName, [In, MarshalAs(UnmanagedType.U4)] int flags);
		void Commit(int commitFlags);
		void Revert();
		void EnumElements([In, MarshalAs(UnmanagedType.U4)] int reserved1, nint reserved2, [In, MarshalAs(UnmanagedType.U4)] int reserved3, [MarshalAs(UnmanagedType.Interface)] out object val);
		void DestroyElement([In, MarshalAs(UnmanagedType.BStr)] string name);
		void RenameElement([In, MarshalAs(UnmanagedType.BStr)] string oldName, [In, MarshalAs(UnmanagedType.BStr)] string newName);
		void SetElementTimes([In, MarshalAs(UnmanagedType.BStr)] string name, [In] FILETIME pcTime, [In] FILETIME paTime, [In] FILETIME pmTime);
		void SetClass([In] ref Guid clsId);
		void SetStateBits(int stateBits, int mask);
		void Stat([Out] out STATSTG statStg, int statFlag);
	}
	[ComImport, Guid("0000000A-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface ILockBytes
	{
		void ReadAt([In, MarshalAs(UnmanagedType.U8)] long offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, [In, MarshalAs(UnmanagedType.U4)] int cb, [Out, MarshalAs(UnmanagedType.LPArray)] int[]? bytesRead);
		void WriteAt([In, MarshalAs(UnmanagedType.U8)] long offset, nint pv, [In, MarshalAs(UnmanagedType.U4)] int cb, [Out, MarshalAs(UnmanagedType.LPArray)] int[] bytesWritten);
		void Flush();
		void SetSize([In, MarshalAs(UnmanagedType.U8)] long cb);
		void LockRegion([In, MarshalAs(UnmanagedType.U8)] long libOffset, [In, MarshalAs(UnmanagedType.U8)] long cb, [In, MarshalAs(UnmanagedType.U4)] int lockType);
		void UnlockRegion([In, MarshalAs(UnmanagedType.U8)] long libOffset, [In, MarshalAs(UnmanagedType.U8)] long cb, [In, MarshalAs(UnmanagedType.U4)] int lockType);
		void Stat([Out] out STATSTG statStg, [In, MarshalAs(UnmanagedType.U4)] int statFlag);
	}
	[StructLayout(LayoutKind.Sequential)]
	public sealed class PointL
	{
		public int X;
		public int Y;
	}
	[StructLayout(LayoutKind.Sequential)]
	public sealed class SizeL
	{
		public int Cx;
		public int Cy;
	}
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public sealed class FileGroupDescriptorA
	{
		public uint ItemCount;
		public FileDescriptorA[]? Items;
	}
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public sealed class FileDescriptorA
	{
		public uint Flags;
		public Guid ClsId;
		public SizeL? Size;
		public PointL? Point;
		public uint FileAttributes;
		public FILETIME CreationTime;
		public FILETIME LastAccessTime;
		public FILETIME LastWriteTime;
		public uint FileSizeHigh;
		public uint FileSizeLow;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string? FileName;
	}
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public sealed class FileGroupDescriptorW
	{
		public uint ItemCount;
		public FileDescriptorW[]? Items;
	}
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public sealed class FileDescriptorW
	{
		public uint Flags;
		public Guid ClsId;
		public SizeL? Size;
		public PointL? Point;
		public uint FileAttributes;
		public FILETIME CreationTime;
		public FILETIME LastAccessTime;
		public FILETIME LastWriteTime;
		public uint FileSizeHigh;
		public uint FileSizeLow;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string? FileName;
	}
}