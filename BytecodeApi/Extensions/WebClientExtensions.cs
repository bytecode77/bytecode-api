using System;
using System.Net;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="WebClient" /> objects.
	/// </summary>
	public static class WebClientExtensions
	{
		/// <summary>
		/// Downloads the requested resource as a <see cref="string" />[], separated by either a CRLF or a LF. The resource to download is specified as a <see cref="string" /> containing the URI.
		/// </summary>
		/// <param name="webClient">The <see cref="WebClient" /> to be used.</param>
		/// <param name="address">A <see cref="string" /> containing the URI to download.</param>
		/// <returns>
		/// A <see cref="string" />[] containing the requested resource, separated by either a CRLF or a LF.
		/// </returns>
		public static string[] DownloadLines(this WebClient webClient, string address)
		{
			Check.ArgumentNull(webClient, nameof(webClient));
			Check.ArgumentNull(address, nameof(address));

			return webClient.DownloadString(address).SplitToLines();
		}
		/// <summary>
		/// Downloads the requested resource as a <see cref="string" />[], separated by either a CRLF or a LF. The resource to download is specified as a <see cref="Uri" />.
		/// </summary>
		/// <param name="webClient">The <see cref="WebClient" /> to be used.</param>
		/// <param name="address">A <see cref="Uri" /> object containing the URI to download.</param>
		/// <returns>
		/// A <see cref="string" />[] containing the requested resource, separated by either a CRLF or a LF.
		/// </returns>
		public static string[] DownloadLines(this WebClient webClient, Uri address)
		{
			Check.ArgumentNull(webClient, nameof(webClient));
			Check.ArgumentNull(address, nameof(address));

			return webClient.DownloadString(address).SplitToLines();
		}
	}
}