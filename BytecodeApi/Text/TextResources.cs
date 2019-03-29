using BytecodeApi.Extensions;

namespace BytecodeApi.Text
{
	/// <summary>
	/// Represents a set of reusable <see cref="string" /> and <see cref="char" /> constants.
	/// </summary>
	public static class TextResources
	{
		/// <summary>
		/// Represents all numeric digits (0-9).
		/// </summary>
		public const string Digits = "0123456789";
		/// <summary>
		/// Represents the alphabet in lowercase.
		/// </summary>
		public const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
		/// <summary>
		/// Represents only the consonants of alphabet in lowercase.
		/// </summary>
		public const string Consonants = "bcdfghjklmnpqrstvwxyz";
		/// <summary>
		/// Represents only the vovels of alphabet in lowercase.
		/// </summary>
		public const string Vovels = "aeiou";
		/// <summary>
		/// Represents the "Lorem Ipsum" text. The length of the <see cref="string" /> is 445 characters.
		/// </summary>
		public const string LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
		/// <summary>
		/// Represents the "The quick brown fox" text. This text is human readable and contains all characters from the alphabet at least once.
		/// </summary>
		public const string TheQuickBrownFox = "The quick brown fox jumps over the lazy dog";
		/// <summary>
		/// Represents the eicar test file, a sequence of 68 characters that is detected by security software. This binary sequence is decoded on runtime when this property is retrieved.
		/// <para>Read more: http://www.eicar.org</para>
		/// </summary>
		public static string EicarTestFile
		{
			get
			{
				byte[] eicar = { 0x15, 0x65, 0x1c, 0x77, 0x09, 0x79, 0x1f, 0x23, 0x35, 0x33, 0x5f, 0x32, 0x21, 0x2e, 0x2f, 0x4f, 0x49, 0xa8, 0xd3, 0xd8, 0xa0, 0xbb, 0xcc, 0xd1, 0xbc, 0xaf, 0xe6, 0xba, 0xe4, 0xed, 0xe4, 0xeb, 0xff, 0x9d, 0xe0, 0xe2, 0xf8, 0xf2, 0xfb, 0x83, 0x97, 0x8c, 0xe6, 0x8f, 0x9f, 0x80, 0x9e, 0x8c, 0x94, 0xb2, 0xb6, 0xb5, 0xc4, 0xb8, 0xaa, 0xa1, 0xa1, 0xd5, 0xbd, 0xb7, 0x4d, 0x41, 0x26, 0x2e, 0x45, 0x3b, 0x5b, 0x3c };
				for (int i = 0, key = 77; i < eicar.Length; i++)
				{
					eicar[i] = (byte)(eicar[i] ^ key);
					key += 3;
				}
				return eicar.ToAnsiString();
			}
		}
		/// <summary>
		/// Represents the right-to-left mark (U+200F). This character reverses printing direction of characters in user interfaces.
		/// </summary>
		public const char RightToLeftMark = '\u202e';
	}
}