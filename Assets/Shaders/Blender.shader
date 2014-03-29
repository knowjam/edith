// Unlit alpha-blended shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Edith/Blender" {
Properties {
	_MainTex ("Main Base (RGB) Trans (A)", 2D) = "white" {}
	_AltTex ("Alternative Base (RGB) Trans (A)", 2D) = "black" {}
	_Blend ("Blend", Range(0,1)) = 0
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 100
	
	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha 
	
	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			sampler2D _AltTex;
			float4 _MainTex_ST;
			fixed _Blend;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);
				fixed4 altCol = tex2D(_AltTex, i.texcoord);
				return (1.0-_Blend)*col + _Blend*altCol;
			}
		ENDCG
	}
}

}
