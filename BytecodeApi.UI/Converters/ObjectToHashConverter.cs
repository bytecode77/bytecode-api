using BytecodeApi.Cryptography;
using BytecodeApi.Extensions;
using System.IO;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts objects of various types to their equivalent hash representation using the specified <see cref="HashType" /> in the given parameter. The default hash type is <see cref="HashType.MD5" />. The hash is returned as a hexadecimal <see cref="string" />. Accepted value types are: <see cref="byte" />[], <see cref="string" /> and <see cref="FileInfo" /> (computes the hash from a file).
	/// </summary>
	public sealed class ObjectToHashStringConverter : ConverterBase<object, HashType?, string>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectToHashStringConverter" /> class.
		/// </summary>
		public ObjectToHashStringConverter()
		{
		}

		/// <summary>
		/// Creates a hash from a variety of types and returns their equivalent <see cref="string" /> representation. The parameter specifies the <see cref="HashType" /> to be used.
		/// </summary>
		/// <param name="value">An <see cref="object" /> to convert. Allowed value types are: <see cref="byte" />[], <see cref="string" /> and <see cref="FileInfo" />, where <see cref="FileInfo" /> specifies a file to compute the hash from.</param>
		/// <param name="parameter">The <see cref="HashType" /> specifying the hash algorithm to use.</param>
		/// <returns>
		/// An equivalent <see cref="string" /> representation of the hash that has been computed.
		/// </returns>
		public override string Convert(object value, HashType? parameter)
		{
			byte[] data;

			if (value == null) return null;
			else if (value is byte[] byteArrayValue) data = byteArrayValue;
			else if (value is string stringValue) data = stringValue.ToUTF8Bytes();
			else if (value is FileInfo fileInfoData) data = fileInfoData.ReadAllBytes();
			else throw Throw.UnsupportedType(nameof(value));

			return Hashes.Compute(data, parameter ?? HashType.MD5);
		}
	}
}