using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace BytecodeApi.Interop;

/// <summary>
/// Represents a handle to unmanaged memory.
/// </summary>
public sealed class HGlobal : IDisposable
{
	private bool Disposed;
	/// <summary>
	/// Gets the handle to the unmanaged memory, or 0, if the handle was disposed.
	/// </summary>
	public nint Handle { get; private set; }
	/// <summary>
	/// Gets the number of bytes allocated.
	/// </summary>
	public int Size { get; private set; }

	private HGlobal(nint handle, int size)
	{
		Handle = handle;
		Size = size;
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="HGlobal" /> class.
	/// </summary>
	/// <param name="size">The number of bytes to allocate.</param>
	public HGlobal(int size) : this(Marshal.AllocHGlobal(size), size)
	{
	}
	/// <summary>
	/// Cleans up Windows resources for this <see cref="HGlobal" />.
	/// </summary>
	~HGlobal()
	{
		Dispose(false);
	}
	/// <summary>
	/// Releases all resources used by the current instance of the <see cref="HGlobal" /> class.
	/// </summary>
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
	private void Dispose(bool disposing)
	{
		if (!Disposed)
		{
			if (Handle != 0)
			{
				Marshal.FreeHGlobal(Handle);
			}

			if (disposing)
			{
				Handle = 0;
				Size = 0;
			}

			Disposed = true;
		}
	}

	/// <summary>
	/// Allocates unmanaged memory and copies the contents of <paramref name="array" /> to the newly allocated memory.
	/// </summary>
	/// <param name="array">A <see cref="byte" />[] to copy.</param>
	/// <returns>
	/// A new <see cref="HGlobal" /> with the specified data.
	/// </returns>
	public static HGlobal FromArray(byte[] array)
	{
		return FromArray(array, 0, array.Length);
	}
	/// <summary>
	/// Allocates unmanaged memory and copies the contents of <paramref name="array" /> to the newly allocated memory.
	/// </summary>
	/// <param name="array">A <see cref="byte" />[] to copy.</param>
	/// <param name="offset">The starting point in the buffer at which to begin.</param>
	/// <param name="count">The number of bytes to take.</param>
	/// <returns>
	/// A new <see cref="HGlobal" /> with the specified data.
	/// </returns>
	public static HGlobal FromArray(byte[] array, int offset, int count)
	{
		HGlobal hglobal = new(count);
		Marshal.Copy(array, offset, hglobal.Handle, count);
		return hglobal;
	}
	/// <summary>
	/// Allocates unmanaged memory and copies the contents of <paramref name="structure" /> to the newly allocated memory.
	/// </summary>
	/// <typeparam name="T">The type of the structure to copy.</typeparam>
	/// <param name="structure">The structure to copy.</param>
	/// <returns>
	/// A new <see cref="HGlobal" /> with the specified structure.
	/// </returns>
	public static HGlobal FromStructure<T>(T structure) where T : struct
	{
		HGlobal hglobal = new(Marshal.SizeOf<T>());
		Marshal.StructureToPtr(structure, hglobal.Handle, true);
		return hglobal;
	}
	/// <summary>
	/// Allocates unmanaged memory and copies the contents of <paramref name="str" /> to the newly allocated memory, converting the <see cref="string" /> to ANSI.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to convert and copy.</param>
	/// <returns>
	/// A new <see cref="HGlobal" /> with the specified <see cref="string" />.
	/// </returns>
	[SupportedOSPlatform("windows")]
	public static HGlobal FromStringAnsi(string? str)
	{
		int charSize = Native.GetCPInfoEx(0, 0, out Native.CpInfoEx info) ? info.MaxCharSize : 2;
		return new(Marshal.StringToHGlobalAnsi(str), str == null ? 0 : (str.Length + 1) * charSize);
	}
	/// <summary>
	/// Allocates unmanaged memory and copies the contents of <paramref name="str" /> to the newly allocated memory.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to convert and copy.</param>
	/// <returns>
	/// A new <see cref="HGlobal" /> with the specified <see cref="string" />.
	/// </returns>
	public static HGlobal FromStringUnicode(string? str)
	{
		return new(Marshal.StringToHGlobalUni(str), str == null ? 0 : (str.Length + 1) * 2);
	}

	/// <summary>
	/// Resizes the allocated block of memory. The value of <see cref="Handle" /> may change after reallocation.
	/// </summary>
	/// <param name="size">The number of bytes to allocate.</param>
	public void ReAlloc(int size)
	{
		Check.ObjectDisposed<HGlobal>(Disposed);
		Check.ArgumentEx.Handle(Handle);

		Handle = Marshal.ReAllocHGlobal(Handle, size);
		Size = size;
	}

	/// <summary>
	/// Converts this <see cref="HGlobal" /> to a pointer of an unspecified type.
	/// </summary>
	/// <returns>
	/// An equivalent pointer of this <see cref="HGlobal" /> instance.
	/// </returns>
	public unsafe void* ToPointer()
	{
		Check.ObjectDisposed<HGlobal>(Disposed);

		return (void*)Handle;
	}
	/// <summary>
	/// Converts this <see cref="HGlobal" /> to a pointer of the specified type.
	/// </summary>
	/// <typeparam name="T">The pointer type to return.</typeparam>
	/// <returns>
	/// An equivalent pointer of this <see cref="HGlobal" /> instance.
	/// </returns>
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
	public unsafe T* ToPointer<T>()
	{
		Check.ObjectDisposed<HGlobal>(Disposed);

		return (T*)Handle;
	}
#pragma warning restore CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
	/// <summary>
	/// Writes the contents of this <see cref="HGlobal" /> to an array.
	/// </summary>
	/// <returns>
	/// A new <see cref="byte" />[] with the contents of this <see cref="HGlobal" /> to an array.
	/// </returns>
	public byte[] ToArray()
	{
		Check.ObjectDisposed<HGlobal>(Disposed);
		Check.ArgumentEx.Handle(Handle);

		byte[] array = new byte[Size];
		Marshal.Copy(Handle, array, 0, array.Length);
		return array;
	}
	/// <summary>
	/// Converts the contents of this <see cref="HGlobal" /> to a structure.
	/// </summary>
	/// <typeparam name="T">The type of the structure to convert to.</typeparam>
	/// <returns>
	/// The structure, read from the handle of this <see cref="HGlobal" />.
	/// </returns>
	public T ToStructure<T>() where T : struct
	{
		Check.ObjectDisposed<HGlobal>(Disposed);
		Check.ArgumentEx.Handle(Handle);
		Check.Argument(Size >= Marshal.SizeOf<T>(), nameof(Size), $"Handle must hold at least one complete instance of the given structure type.");

		return Marshal.PtrToStructure<T>(Handle);
	}
	/// <summary>
	/// Reads an ANSI <see cref="string" /> from this <see cref="HGlobal" />.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> read from this <see cref="HGlobal" />, or <see langword="null" />, if this handle is 0.
	/// </returns>
	public string? ToStringAnsi()
	{
		Check.ObjectDisposed<HGlobal>(Disposed);

		return Marshal.PtrToStringAnsi(Handle);
	}
	/// <summary>
	/// Reads a Unicode <see cref="string" /> from this <see cref="HGlobal" />.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> read from this <see cref="HGlobal" />, or <see langword="null" />, if this handle is 0.
	/// </returns>
	public string? ToStringUnicode()
	{
		Check.ObjectDisposed<HGlobal>(Disposed);

		return Marshal.PtrToStringUni(Handle);
	}
	/// <summary>
	/// Reads a UTF-8 <see cref="string" /> from this <see cref="HGlobal" />.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> read from this <see cref="HGlobal" />, or <see langword="null" />, if this handle is 0.
	/// </returns>
	public string? ToStringUTF8()
	{
		Check.ObjectDisposed<HGlobal>(Disposed);

		return Marshal.PtrToStringUTF8(Handle);
	}
}

[SupportedOSPlatform("windows")]
file static class Native
{
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool GetCPInfoEx([MarshalAs(UnmanagedType.U4)] int CodePage, [MarshalAs(UnmanagedType.U4)] int dwFlags, out CpInfoEx lpCPInfoEx);

	[StructLayout(LayoutKind.Sequential)]
	public struct CpInfoEx
	{
		[MarshalAs(UnmanagedType.U4)]
		public int MaxCharSize;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
		public byte[] DefaultChar;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
		public byte[] LeadBytes;
		public char UnicodeDefaultChar;
		[MarshalAs(UnmanagedType.U4)]
		public int CodePage;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string CodePageName;
	}
}