using System;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace BytecodeApi.UI.Markup
{
	/// <summary>
	/// Implements icon support for .NET Framework XAML Services.
	/// </summary>
	public sealed class IconExtension : MarkupExtension
	{
		/// <summary>
		/// Gets or sets the source of the icon.
		/// </summary>
		public string Source { get; set; }
		/// <summary>
		/// Gets or sets the size (the width) of the icon.
		/// </summary>
		public int Size { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="IconExtension" /> class, initializing <paramref name="source" /> and <paramref name="size" /> based on the provided values.
		/// </summary>
		/// <param name="source">A <see cref="string" /> value that is assigned to <see cref="Source" />.</param>
		/// <param name="size">A <see cref="int" /> value that is assigned to <see cref="Size" />.</param>
		public IconExtension(string source, int size)
		{
			Source = source;
			Size = size;
		}
		/// <summary>
		/// Returns a <see cref="BitmapFrame" /> value that is computed from the parameters supplied in the constructor of <see cref="IconExtension" />. If the specified icon size could not be found, the smallest icon is returned that is larger than <see cref="Size" />.
		/// </summary>
		/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
		/// <returns>
		/// A <see cref="BitmapFrame" /> value that is computed from the parameters supplied in the constructor of <see cref="IconExtension" />.
		/// </returns>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			Check.ArgumentNull(Source, nameof(Source));
			Check.ArgumentOutOfRangeEx.Greater0(Size, nameof(Size));

			return BitmapDecoder
				.Create(new Uri(Packs.Application + Source), BitmapCreateOptions.DelayCreation, BitmapCacheOption.OnDemand)
				.Frames
				.Where(f => f.Width >= Size)
				.OrderBy(f => f.Width)
				.FirstOrDefault();
		}
	}
}