Shader "Custom/GroundSimpleShader" {
	Properties {
        _Color ("Main Color", Color) = (0,0,0,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf SimpleLambert

      half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten) {
          //half NdotL = dot (s.Normal, lightDir);
          half4 c;
          c.rgb = s.Albedo.rgb;// * _LightColor0.rgb * (NdotL * atten * 2);
          //c.r = sin(_Time*100);
          
          c.a = s.Alpha;
          return c;
      }
      
        
        fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
		};
        

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = _Color;
		}
		ENDCG
	} 
}
