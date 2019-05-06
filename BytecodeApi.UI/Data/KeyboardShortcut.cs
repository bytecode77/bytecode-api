using BytecodeApi.Extensions;
using System.Collections.Generic;
using System.Windows.Input;

namespace BytecodeApi.UI.Data
{
	/// <summary>
	/// Represents a keyboard shortcut composed of a key and a set of modifiers (Ctrl, Shift, Alt).
	/// </summary>
	public class KeyboardShortcut : ObservableObject
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
		/// Returns a <see cref="string" /> whose value represents the <see cref="DisplayName" /> property.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> whose value represents the <see cref="DisplayName" /> property.
		/// </returns>
		public override string ToString()
		{
			return DisplayName;
		}
	}
}