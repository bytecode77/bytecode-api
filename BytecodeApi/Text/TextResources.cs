namespace BytecodeApi.Text;

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
	/// Represents the right-to-left mark (U+200F). This character reverses printing direction of characters in user interfaces. This field is constant.
	/// </summary>
	public const char RightToLeftMark = '\u202e';
}