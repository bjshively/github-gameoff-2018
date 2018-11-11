Shader ".TEST/Forward Cel-Shading" {
    Properties {
        _Color("Color", Color) = (1, 1, 1, 1)
        _MainTex("Texture", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType" = "Opaque"
        }
        LOD 200
 
        CGPROGRAM
        #pragma surface surf CelShadingForward
 
        half4 LightingCelShadingForward(SurfaceOutput s, half3 lightDir, half atten) {
            half NdotL = dot(s.Normal, lightDir);
            if (NdotL <= 0.0) NdotL = 0;
            else NdotL = 1;
            half4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten * 1);
            c.a = s.Alpha;
            return c;
        }
 
        sampler2D _MainTex;
        fixed4 _Color;
 
        struct Input {
            float2 uv_MainTex;
        };
 
        void surf(Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}