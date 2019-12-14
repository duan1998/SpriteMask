// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Glitch" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_brightnessAmount("Brightness Amount", Range(0.0, 1)) = 1.0
		_saturationAmount("Saturation Amount", Range(0.0, 1)) = 1.0
		_contrastAmount("Contrast Amount", Range(0.0, 1)) = 1.0
	}

		SubShader
		{
			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"
				struct a2v {
					float4 vertex:POSITION;
					float4 texcoord:TEXCOORD0;
				};

			struct v2f {
				float4 pos:SV_POSITION;
				float2 uv:TEXCOORD0;
			};

			
			uniform sampler2D _MainTex;
			fixed _brightnessAmount;
			fixed _saturationAmount;
			fixed _contrastAmount;
			sampler2D _NoiseTex;
			sampler2D _TrashTex;
			float _Intensity;

			v2f vert(a2v v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				return o;
			}

			float3 ContrastSaturationBrightness(float3 color, float brt, float sat, float con)
			{
				float AvgLumR = 0.5;
				float AvgLumG = 0.5;
				float AvgLumB = 0.5;
				
				float3 LuminanceCoeff = float3(0.2125, 0.7154, 0.0721);
				
				float3 AvgLumin = float3(AvgLumR, AvgLumG, AvgLumB);
				float3 brtColor = color * brt;
				float intensityf = dot(brtColor, LuminanceCoeff);
				float3 intensity = float3(intensityf, intensityf, intensityf);
				
				float3 satColor = lerp(intensity, brtColor, sat);
				float3 conColor = lerp(AvgLumin, satColor, con);
				return conColor;
			}

			fixed4 frag(v2f i) : COLOR
			{
				float4 glitch = tex2D(_NoiseTex, i.uv);

				float thresh = 1.001 - _Intensity * 1.001;
				float w_d = step(thresh, pow(glitch.z, 2.5));
				float w_f = step(thresh, pow(glitch.w, 2.5)); 
				float w_c = step(thresh, pow(glitch.z, 3.5)); 

				float2 uv = frac(i.uv + glitch.xy * w_d);
				float4 source = tex2D(_MainTex, uv);

				float3 color = lerp(source, tex2D(_TrashTex, uv), w_f).rgb;

				float3 neg = saturate(color.grb + (1 - dot(color, 1)) * 0.5);
				color = lerp(color, neg, w_c);
				color.rgb = ContrastSaturationBrightness(color.rgb, _brightnessAmount, _saturationAmount, _contrastAmount);

				return float4(color, source.a);
			}
				ENDCG
		}
	} 
	FallBack off
}
