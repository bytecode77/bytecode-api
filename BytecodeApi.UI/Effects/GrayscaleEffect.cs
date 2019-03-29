using BytecodeApi.UI.Extensions;
using BytecodeApi.UI.Properties;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace BytecodeApi.UI.Effects
{
	/// <summary>
	/// Provides a grayscale bitmap effect. This shader is typically used on WPF images to render the disabled state of an element.
	/// </summary>
	public sealed class GrayscaleEffect : ShaderEffect
	{
		/// <summary>
		/// Identifies the <see cref="Input" /> dependency property. This field is read-only.
		/// </summary>
		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(GrayscaleEffect), 0);
		/// <summary>
		/// Gets or sets a <see cref="Brush" /> that applies a grayscale effect on the bitmap.
		/// </summary>
		public Brush Input
		{
			get => this.GetValue<Brush>(InputProperty);
			set => SetValue(InputProperty, value);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GrayscaleEffect" /> class.
		/// </summary>
		public GrayscaleEffect()
		{
			PixelShader = new PixelShader();
			PixelShader.SetStreamSource(new MemoryStream(Resources.FileGrayscaleEffect));
			UpdateShaderValue(InputProperty);
		}
	}
}