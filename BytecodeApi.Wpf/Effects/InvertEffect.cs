using BytecodeApi.Wpf.Extensions;
using BytecodeApi.Wpf.Properties;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace BytecodeApi.Wpf.Effects;

/// <summary>
/// Provides a bitmap effect that inverts the colors of an image.
/// </summary>
public sealed class InvertEffect : ShaderEffect
{
	/// <summary>
	/// Identifies the <see cref="Input" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(InvertEffect), 0);
	/// <summary>
	/// Gets or sets a <see cref="Brush" /> that applies an invert effect on the bitmap.
	/// </summary>
	public Brush Input
	{
		get => this.GetValue<Brush>(InputProperty);
		set => SetValue(InputProperty, value);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="InvertEffect" /> class.
	/// </summary>
	public InvertEffect()
	{
		PixelShader = new();
		PixelShader.SetStreamSource(new MemoryStream(Resources.InvertEffect));
		UpdateShaderValue(InputProperty);
	}
}

// InvertEffect.fx
/*******************************************************************************
sampler2D input : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(input, uv);
	float alpha = color.a;

	color = 1 - color;
	color.a = alpha;
	color.rgb *= alpha;

	return color;
}
*******************************************************************************/