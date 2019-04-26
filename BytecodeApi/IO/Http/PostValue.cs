using System;

namespace BytecodeApi.IO.Http
{
	/// <summary>
	/// Represents an HTTP POST value as a key value pair, used by the <see cref="HttpClient" /> class.
	/// </summary>
	public struct PostValue : IEquatable<PostValue>
	{
		/// <summary>
		/// Gets or sets the key of the HTTP POST value.
		/// </summary>
		public string Key { get; set; }
		/// <summary>
		/// Gets or sets the value of the HTTP POST value.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PostValue" /> structure with the specified key and value.
		/// </summary>
		/// <param name="key">A <see cref="string" /> specifying the key of the HTTP POST value.</param>
		/// <param name="value">A <see cref="string" /> specifying the value of the HTTP POST value.</param>
		public PostValue(string key, string value)
		{
			Key = key;
			Value = value;
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + Key + ", " + Value + "]";
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
			return obj is PostValue postValue && Equals(postValue);
		}
		/// <summary>
		/// Determines whether this instance is equal to another <see cref="PostValue" />.
		/// </summary>
		/// <param name="other">The <see cref="PostValue" /> to compare to this instance.</param>
		/// <returns>
		/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(PostValue other)
		{
			return Key == other.Key && Value == other.Value;
		}
		/// <summary>
		/// Returns a hash code for this <see cref="PostValue" />.
		/// </summary>
		/// <returns>
		/// The hash code for this <see cref="PostValue" /> instance.
		/// </returns>
		public override int GetHashCode()
		{
			return CSharp.GetHashCode(Key, Value);
		}

		/// <summary>
		/// Compares two <see cref="PostValue" /> instances for equality.
		/// </summary>
		/// <param name="a">The first <see cref="PostValue" /> to compare.</param>
		/// <param name="b">The second <see cref="PostValue" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="PostValue" /> are equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator ==(PostValue a, PostValue b)
		{
			return Equals(a, b);
		}
		/// <summary>
		/// Compares two <see cref="PostValue" /> instances for inequality.
		/// </summary>
		/// <param name="a">The first <see cref="PostValue" /> to compare.</param>
		/// <param name="b">The second <see cref="PostValue" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="PostValue" /> are not equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator !=(PostValue a, PostValue b)
		{
			return !(a == b);
		}
	}
}