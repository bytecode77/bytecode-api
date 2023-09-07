using BytecodeApi.Wpf.Extensions;
using BytecodeApi.Wpf.Properties;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace BytecodeApi.Wpf.Effects;

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
		PixelShader = new();
		PixelShader.SetStreamSource(new MemoryStream(Resources.GrayscaleEffect));
		UpdateShaderValue(InputProperty);
	}
}

// GrayscaleEffect.fx
/*******************************************************************************
sampler2D implicitInput : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(implicitInput, uv);
	color.rgb = dot(color.rgb, float3(.3, .59, .11));
	return color;
}
*******************************************************************************/