Shader "Unlit/Ripple"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

	CGINCLUDE
#include "UnityCG.cginc"
		uniform sampler2D _MainTex;
	float4 _MainTex_TexelSize;
	uniform float _distanceFactor;
	uniform float _timeFactor;
	uniform float _totalFactor;
	uniform float _waveWidth;
	uniform float _curWaveDis;
	uniform float4 _startPos;

	fixed4 frag(v2f_img i) : SV_Target
	{
		
		#if UNITY_UV_STARTS_AT_TOP
		if (_MainTex_TexelSize.y < 0)
			_startPos.y = 1 - _startPos.y;
	//DX下纹理坐标反向
		#endif
	float2 dv = _startPos.xy - i.uv;//计算uv到中间点的向量(向外扩，反过来就是向里缩)
	dv = dv * float2(_ScreenParams.x / _ScreenParams.y, 1);
	float dis = sqrt(dv.x * dv.x + dv.y * dv.y);
	float sinFactor = sin(dis * _distanceFactor + _Time.y * _timeFactor) * _totalFactor * 0.01;//偏移量小，波峰波谷大
	float discardFactor = clamp(_waveWidth - abs(_curWaveDis - dis), 0, 1) / _waveWidth;//超出当前范围factor通过clamp设置为0
	float2 dv1 = normalize(dv);
	float2 offset = dv1 * sinFactor * discardFactor;//uv偏移
	float2 uv = offset + i.uv;
	return tex2D(_MainTex, uv);
	}

		ENDCG

		SubShader
	{
		Pass
		{
			ZTest Always
			Cull Off
			ZWrite Off
			Fog { Mode off }

			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			ENDCG
		}
	}
	Fallback off
}
