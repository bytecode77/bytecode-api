using BytecodeApi.IO;
using BytecodeApi.Threading;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="WebRequest" /> and related objects.
	/// </summary>
	public static class WebRequestExtensions
	{
		/// <summary>
		/// Reads all characters from the response of this <see cref="WebRequest" />.
		/// </summary>
		/// <param name="request">The <see cref="WebRequest" /> to read from.</param>
		/// <returns>
		/// A <see cref="string" /> with all characters read from the response of this <see cref="WebRequest" />.
		/// </returns>
		public static string ReadToEndString(this WebRequest request)
		{
			Check.ArgumentNull(request, nameof(request));

			return WebRequestException.Try(() =>
			{
				using (WebResponse response = request.GetResponse())
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					return reader.ReadToEnd();
				}
			});
		}
		/// <summary>
		/// Reads all bytes from the response of this <see cref="WebRequest" />.
		/// </summary>
		/// <param name="request">The <see cref="WebRequest" /> to read from.</param>
		/// <returns>
		/// A <see cref="byte" />[] with all bytes read from the response of this <see cref="WebRequest" />.
		/// </returns>
		public static byte[] ReadToEndBytes(this WebRequest request)
		{
			return request.ReadToEndBytes(null);
		}
		/// <summary>
		/// Reads all bytes from the response of this <see cref="WebRequest" />.
		/// </summary>
		/// <param name="request">The <see cref="WebRequest" /> to read from.</param>
		/// <param name="callback">The <see cref="TransferCallback" /> delegate that is triggered approximately every 100 milliseconds, or <see langword="null" />.</param>
		/// <returns>
		/// A <see cref="byte" />[] with all bytes read from the response of this <see cref="WebRequest" />.
		/// </returns>
		public static byte[] ReadToEndBytes(this WebRequest request, TransferCallback callback)
		{
			Check.ArgumentNull(request, nameof(request));

			return WebRequestException.Try(() =>
			{
				using (WebResponse response = request.GetResponse())
				using (Stream stream = response.GetResponseStream())
				using (MemoryStream memoryStream = new MemoryStream())
				{
					byte[] buffer = new byte[4096];
					Stopwatch stopwatch = ThreadFactory.StartStopwatch();
					int bytesRead;
					long totalBytesRead = 0;
					long callbackBytesRead = 0;

					do
					{
						bytesRead = stream.Read(buffer);
						totalBytesRead += bytesRead;
						memoryStream.Write(buffer, 0, bytesRead);

						if (callback != null)
						{
							callbackBytesRead += bytesRead;
							if (stopwatch.Elapsed > TimeSpan.FromMilliseconds(100))
							{
								stopwatch.Restart();
								callback(callbackBytesRead, totalBytesRead);
								callbackBytesRead = 0;
							}
						}
					}
					while (bytesRead > 0);

					if (callback != null && callbackBytesRead > 0) callback(callbackBytesRead, totalBytesRead);
					return memoryStream.ToArray();
				}
			});
		}
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