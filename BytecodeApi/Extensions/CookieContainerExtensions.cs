using System.Collections;
using System.Linq;
using System.Net;
using System.Reflection;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="CookieContainer" /> objects.
	/// </summary>
	public static class CookieContainerExtensions
	{
		/// <summary>
		/// Gets the cookies from all domains from this <see cref="CookieContainer" />.
		/// </summary>
		/// <param name="cookieContainer">The <see cref="CookieContainer" /> to get the cookies from.</param>
		/// <returns>
		/// A new <see cref="Cookie" />[] with the cookies from all domains from this <see cref="CookieContainer" />.
		/// </returns>
		public static Cookie[] GetCookies(this CookieContainer cookieContainer)
		{
			Check.ArgumentNull(cookieContainer, nameof(cookieContainer));

			return cookieContainer
				.GetType()
				.GetField("m_domainTable", BindingFlags.Instance | BindingFlags.NonPublic)
				.GetValue<Hashtable>(cookieContainer)
				.Cast<DictionaryEntry>()
				.SelectMany(entry => entry.Value
					.GetType()
					.GetField("m_list", BindingFlags.Instance | BindingFlags.NonPublic)
					.GetValue<SortedList>(entry.Value)
					.Values
					.Cast<CookieCollection>()
					.SelectMany(collection => collection.Cast<Cookie>()))
				.ToArray();
		}
	}
}