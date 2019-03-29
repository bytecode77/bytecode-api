sampler2D implicitInput : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 color = tex2D(implicitInput, uv);
	color.rgb = dot(color.rgb, float3(.3, .59, .11));
	return color;
}