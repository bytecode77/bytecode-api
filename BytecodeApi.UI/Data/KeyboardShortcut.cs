using BytecodeApi.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace BytecodeApi.UI.Data
{
	/// <summary>
	/// Represents a keyboard shortcut composed of a key and a set of modifiers (Ctrl, Shift, Alt).
	/// </summary>
	public sealed class KeyboardShortcut : ObservableObject, IEquatable<KeyboardShortcut>
	{
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="KeyboardShortcut" /> uses the Ctrl modifier.
		/// </summary>
		public bool IsCtrl
		{
			get => Get(() => IsCtrl);
			set
			{
				Set(() => IsCtrl, value);
				RaisePropertyChanged(() => DisplayName);
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="KeyboardShortcut" /> uses the Shift modifier.
		/// </summary>
		public bool IsShift
		{
			get => Get(() => IsShift);
			set
			{
				Set(() => IsShift, value);
				RaisePropertyChanged(() => DisplayName);
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="KeyboardShortcut" /> uses the Alt modifier.
		/// </summary>
		public bool IsAlt
		{
			get => Get(() => IsAlt);
			set
			{
				Set(() => IsAlt, value);
				RaisePropertyChanged(() => DisplayName);
			}
		}
		/// <summary>
		/// Gets or sets the key for this <see cref="KeyboardShortcut" />.
		/// </summary>
		public Key Key
		{
			get => Get(() => Key, () => Key.None);
			set
			{
				Set(() => Key, value);
				RaisePropertyChanged(() => KeyName);
				RaisePropertyChanged(() => DisplayName);
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
				List<string> modifiers = new List<string>();

				if (IsCtrl) modifiers.Add("Ctrl");
				if (IsShift) modifiers.Add("Shift");
				if (IsAlt) modifiers.Add("Alt");
				modifiers.Add(KeyName);

				return modifiers.AsString("+");
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="KeyboardShortcut" /> class.
		/// </summary>
		public KeyboardShortcut()
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="KeyboardShortcut" /> class with the specified modifiers and the specified key.
		/// </summary>
		public KeyboardShortcut(bool isCtrl, bool isShift, bool isAlt, Key key) : this()
		{
			IsCtrl = isCtrl;
			IsShift = isShift;
			IsAlt = isAlt;
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
		public override bool Equals(object obj)
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
		public bool Equals(KeyboardShortcut other)
		{
			return other != null && CSharp.TypeEquals(this, other) && IsCtrl == other.IsCtrl && IsShift == other.IsShift && IsAlt == other.IsAlt && Key == other.Key;
		}
		/// <summary>
		/// Returns a hash code for this <see cref="KeyboardShortcut" />.
		/// </summary>
		/// <returns>
		/// The hash code for this <see cref="KeyboardShortcut" /> instance.
		/// </returns>
		public override int GetHashCode()
		{
			return CSharp.GetHashCode((IsCtrl ? 1 : 0) << 10 | (IsShift ? 1 : 0) << 9 | (IsAlt ? 1 : 0) << 8, Key);
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
		public static bool operator ==(KeyboardShortcut a, KeyboardShortcut b)
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
		public static bool operator !=(KeyboardShortcut a, KeyboardShortcut b)
		{
			return !(a == b);
		}
	}
}