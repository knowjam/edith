
Shader "Edith/MaskedTexture" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Mask ("Culling Mask", 2D) = "white" {}
    _Cutoff ("Alpha cutoff", Range (0,1)) = 0.1
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 200
	

CGPROGRAM
#pragma surface surf Lambert alpha
	
sampler2D _MainTex;
sampler2D _Mask;
fixed4 _Color;

struct Input {
	float2 uv_MainTex;
	float2 uv_Mask;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	float m = tex2D(_Mask, IN.uv_Mask).a;

	o.Albedo = c.rgb;
	o.Alpha = c.a * m;
	o.Emission = c.rgb;

}
ENDCG
}

Fallback "Transparent/VertexLit"
}


/*
Shader "Edith/MaskedTexture" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Mask ("Culling Mask", 2D) = "white" {}
    _Cutoff ("Alpha cutoff", Range (0,1)) = 0.1
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 200
	Blend SrcAlpha OneMinusSrcAlpha
	AlphaTest GEqual [_Cutoff]

	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float2 maskcoord : TEXCOORD1;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				half2 maskcoord : TEXCOORD1;
			};

			sampler2D _MainTex;
			sampler2D _Mask;
			float4 _MainTex_ST;
			float4 _Mask_ST;
			fixed _Cutoff;
			float4 _Color;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.maskcoord = TRANSFORM_TEX(v.texcoord, _Mask);
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);
				float m = tex2D(_Mask, i.maskcoord).a;
				col *= _Color;
				col.a *= m;
				clip(col.a - _Cutoff);
				return col;
			}
		ENDCG
	}
}

}
*/