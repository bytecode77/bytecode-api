namespace BytecodeApi.Text
{
	/// <summary>
	/// Represents a set of reusable <see cref="string" /> and <see cref="char" /> constants.
	/// </summary>
	public static class TextResources
	{
		/// <summary>
		/// Represents all numeric digits (0-9). This field is constant.
		/// </summary>
		public const string Digits = "0123456789";
		/// <summary>
		/// Represents the alphabet in lowercase. This field is constant.
		/// </summary>
		public const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
		/// <summary>
		/// Represents only the consonants of alphabet in lowercase. This field is constant.
		/// </summary>
		public const string Consonants = "bcdfghjklmnpqrstvwxyz";
		/// <summary>
		/// Represents only the vovels of alphabet in lowercase. This field is constant.
		/// </summary>
		public const string Vovels = "aeiou";
		/// <summary>
		/// Represents the "Lorem Ipsum" text. The length of the <see cref="string" /> is 445 characters. This field is constant.
		/// </summary>
		public const string LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
		/// <summary>
		/// Represents the "The quick brown fox" text. This text is human readable and contains all characters from the alphabet at least once. This field is constant.
		/// </summary>
		public const string TheQuickBrownFox = "The quick brown fox jumps over the lazy dog";
		/// <summary>
		/// Represents the right-to-left mark (U+200F). This character reverses printing direction of characters in user interfaces. This field is constant.
		/// </summary>
		public const char RightToLeftMark = '\u202e';
	}
}