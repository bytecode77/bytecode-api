namespace BytecodeApi.GeoIP.City
{
	/// <summary>
	/// Represents the result of a GeoIP city lookup.
	/// </summary>
	public sealed class CityLookupResult
	{
		/// <summary>
		/// Gets the city of the IP address.
		/// </summary>
		public City City { get; private set; }
		/// <summary>
		/// Gets the postal code of the IP address, or <see langword="null" />, if it cannot be determined.
		/// </summary>
		public string PostalCode { get; private set; }
		/// <summary>
		/// Gets the approximate latitude of the IP address, or 0.0f, if it cannot be determined.
		/// </summary>
		public float Latitude { get; private set; }
		/// <summary>
		/// Gets the approximate longitude of the IP address, or 0.0f, if it cannot be determined.
		/// </summary>
		public float Longitude { get; private set; }
		/// <summary>
		/// Gets the approximate accuracy radius, in kilometers around the latitude and longitude, or 0, if it cannot be determined.
		/// </summary>
		public int AccuracyRadius { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CityLookupResult" /> class.
		/// </summary>
		/// <param name="city">The <see cref="GeoIP.City.City" /> of the lookup result.</param>
		/// <param name="postalCode">The postal code of the lookup result.</param>
		/// <param name="latitude">The approximate latitude of the lookup result.</param>
		/// <param name="longitude">The approximate longitude of the lookup result.</param>
		/// <param name="accuracyRadius">The approximate accuracy radius, in kilometers around the latitude and longitude, of the lookup result.</param>
		public CityLookupResult(City city, string postalCode, float latitude, float longitude, int accuracyRadius)
		{
			City = city;
			PostalCode = postalCode;
			Latitude = latitude;
			Longitude = longitude;
			AccuracyRadius = accuracyRadius;
		}
	}
}