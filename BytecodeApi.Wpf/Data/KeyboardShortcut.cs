using BytecodeApi.Data;
using BytecodeApi.Extensions;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Data;

/// <summary>
/// Represents a keyboard shortcut composed of a key and a set of modifiers (Ctrl, Shift, Alt, Win).
/// </summary>
public sealed class KeyboardShortcut : ObservableObject, IEquatable<KeyboardShortcut>
{
	private ModifierKeys _Modifiers;
	private Key _Key;
	/// <summary>
	/// Gets or sets the modifiers for this <see cref="KeyboardShortcut" />.
	/// </summary>
	public ModifierKeys Modifiers
	{
		get => _Modifiers;
		set
		{
			Set(ref _Modifiers, value);
			RaisePropertyChanged(nameof(IsCtrl));
			RaisePropertyChanged(nameof(IsShift));
			RaisePropertyChanged(nameof(IsAlt));
			RaisePropertyChanged(nameof(IsWin));
			RaisePropertyChanged(nameof(DisplayName));
		}
	}
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="KeyboardShortcut" /> uses the Control modifier.
	/// </summary>
	public bool IsCtrl
	{
		get => Modifiers.HasFlag(ModifierKeys.Control);
		set
		{
			if (value)
			{
				Modifiers |= ModifierKeys.Control;
			}
			else
			{
				Modifiers &= ~ModifierKeys.Control;
			}
		}
	}
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="KeyboardShortcut" /> uses the Shift modifier.
	/// </summary>
	public bool IsShift
	{
		get => Modifiers.HasFlag(ModifierKeys.Shift);
		set
		{
			if (value)
			{
				Modifiers |= ModifierKeys.Shift;
			}
			else
			{
				Modifiers &= ~ModifierKeys.Shift;
			}
		}
	}
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="KeyboardShortcut" /> uses the Alt modifier.
	/// </summary>
	public bool IsAlt
	{
		get => Modifiers.HasFlag(ModifierKeys.Alt);
		set
		{
			if (value)
			{
				Modifiers |= ModifierKeys.Alt;
			}
			else
			{
				Modifiers &= ~ModifierKeys.Alt;
			}
		}
	}
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="KeyboardShortcut" /> uses the Windows modifier.
	/// </summary>
	public bool IsWin
	{
		get => Modifiers.HasFlag(ModifierKeys.Windows);
		set
		{
			if (value)
			{
				Modifiers |= ModifierKeys.Windows;
			}
			else
			{
				Modifiers &= ~ModifierKeys.Windows;
			}
		}
	}
	/// <summary>
	/// Gets or sets the key for this <see cref="KeyboardShortcut" />.
	/// </summary>
	public Key Key
	{
		get => _Key;
		set
		{
			Set(ref _Key, value);
			RaisePropertyChanged(nameof(KeyName));
			RaisePropertyChanged(nameof(DisplayName));
		}
	}
	/// <summary>
	/// Gets the equivalent display name for the <see cref="Key" /> property.
	/// </summary>
	public string KeyName => new KeyConverter().ConvertTo(Key, typeof(string)) as string ?? "";
	/// <summary>
	/// Gets the display name for this instance.
	/// <para>Example: Ctrl+Shift+A</para>
	/// </summary>
	public string DisplayName
	{
		get
		{
			List<string> modifiers = [];

			if (IsCtrl) modifiers.Add("Ctrl");
			if (IsShift) modifiers.Add("Shift");
			if (IsAlt) modifiers.Add("Alt");
			if (IsWin) modifiers.Add("Win");
			modifiers.Add(KeyName);

			return modifiers.AsString("+");
		}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="KeyboardShortcut" /> class.
	/// </summary>
	public KeyboardShortcut()
	{
		Modifiers = ModifierKeys.None;
		Key = Key.None;
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="KeyboardShortcut" /> class with the specified modifiers and the specified key.
	/// </summary>
	/// <param name="modifiers">The modifiers for this <see cref="KeyboardShortcut" />.</param>
	/// <param name="key">The key for this <see cref="KeyboardShortcut" />.</param>
	public KeyboardShortcut(ModifierKeys modifiers, Key key) : this()
	{
		Modifiers = modifiers;
		Key = key;
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="KeyboardShortcut" /> class with the specified modifiers and the specified key.
	/// </summary>
	/// <param name="isCtrl">A value indicating whether this <see cref="KeyboardShortcut" /> uses the Control modifier.</param>
	/// <param name="isShift">A value indicating whether this <see cref="KeyboardShortcut" /> uses the Shift modifier.</param>
	/// <param name="isAlt">A value indicating whether this <see cref="KeyboardShortcut" /> uses the Alt modifier.</param>
	/// <param name="isWin">A value indicating whether this <see cref="KeyboardShortcut" /> uses the Windows modifier.</param>
	/// <param name="key">The key for this <see cref="KeyboardShortcut" />.</param>
	public KeyboardShortcut(bool isCtrl, bool isShift, bool isAlt, bool isWin, Key key) : this()
	{
		IsCtrl = isCtrl;
		IsShift = isShift;
		IsAlt = isAlt;
		IsWin = isWin;
		Key = key;
	}

	/// <summary>
	/// Returns the display name of this <see cref="KeyboardShortcut" />.
	/// </summary>
	/// <returns>
	/// The display name of this <see cref="KeyboardShortcut" />.
	/// </returns>
	public override string ToString()
	{
		return DisplayName;
	}
	/// <summary>
	/// Determines whether the specified <see cref="object" /> is equal to this instance.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
	/// <returns>
	/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is KeyboardShortcut keyboardShortcut && Equals(keyboardShortcut);
	}
	/// <summary>
	/// Determines whether this instance is equal to another <see cref="KeyboardShortcut" />.
	/// </summary>
	/// <param name="other">The <see cref="KeyboardShortcut" /> to compare to this instance.</param>
	/// <returns>
	/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals([NotNullWhen(true)] KeyboardShortcut? other)
	{
		return
			other != null &&
			CSharp.TypeEquals(this, other) &&
			Modifiers == other.Modifiers &&
			Key == other.Key;
	}
	/// <summary>
	/// Returns a hash code for this <see cref="KeyboardShortcut" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="KeyboardShortcut" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return HashCode.Combine((int)Modifiers << 10, Key);
	}

	/// <summary>
	/// Compares two <see cref="KeyboardShortcut" /> instances for equality.
	/// </summary>
	/// <param name="a">The first <see cref="KeyboardShortcut" /> to compare.</param>
	/// <param name="b">The second <see cref="KeyboardShortcut" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="KeyboardShortcut" /> are equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator ==(KeyboardShortcut? a, KeyboardShortcut? b)
	{
		return Equals(a, b);
	}
	/// <summary>
	/// Compares two <see cref="KeyboardShortcut" /> instances for inequality.
	/// </summary>
	/// <param name="a">The first <see cref="KeyboardShortcut" /> to compare.</param>
	/// <param name="b">The second <see cref="KeyboardShortcut" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="KeyboardShortcut" /> are not equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator !=(KeyboardShortcut? a, KeyboardShortcut? b)
	{
		return !(a == b);
	}
}