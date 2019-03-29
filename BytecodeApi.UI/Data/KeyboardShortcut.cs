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
		/// Gets or sets <see cref="bool" /> value indicating whether this <see cref="KeyboardShortcut" /> uses the Ctrl modifier.
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
		/// Gets or sets <see cref="bool" /> value indicating whether this <see cref="KeyboardShortcut" /> uses the Shift modifier.
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
		/// Gets or sets <see cref="bool" /> value indicating whether this <see cref="KeyboardShortcut" /> uses the Alt modifier.
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
			get => Get(() => Key);
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
		public string KeyName
		{
			get
			{
				//IMPORTANT: Proper implementation
				switch (Key)
				{
					case Key.D0:
					case Key.D1:
					case Key.D2:
					case Key.D3:
					case Key.D4:
					case Key.D5:
					case Key.D6:
					case Key.D7:
					case Key.D8:
					case Key.D9: return Key.ToString().Substring(1);
					case Key.NumPad0:
					case Key.NumPad1:
					case Key.NumPad2:
					case Key.NumPad3:
					case Key.NumPad4:
					case Key.NumPad5:
					case Key.NumPad6:
					case Key.NumPad7:
					case Key.NumPad8:
					case Key.NumPad9: return "NumPad " + Key.ToString().Substring(6);
					case Key.Oem5: return "^";
					case Key.OemOpenBrackets: return "ß";
					case Key.Oem6: return "´";
					case Key.OemPlus: return "+";
					case Key.OemQuestion: return "#";
					case Key.OemMinus: return "-";
					case Key.OemPeriod: return ".";
					case Key.OemComma: return ",";
					case Key.OemBackslash: return "<";
					default: return Key.ToString();
				}
			}
		}
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
			Key = Key.None;
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