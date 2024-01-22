Shader "Custom/Retro" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _RetroAmount ("Retro Amount", Range(0,1)) = 1
        _VignetteAmount ("Vignette Amount", Range(0,1)) = 0.5
        _VignetteSoftness ("Vignette Softness", Range(0,1)) = 0.5
        _NoiseTexture ("Noise Texture", 2D) = "white" {}
    }

    SubShader {
        Pass {
            Tags { "RenderType"="Opaque" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _Color;
            float _RetroAmount;
            float _VignetteAmount;
            float _VignetteSoftness;
            sampler2D _NoiseTexture;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Sample texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // Apply color tint
                col *= _Color;

                // Apply retro effect using color grading and noise
                col.r = tex2D(_MainTex, float2(col.r, 0)).r;
                col.g = tex2D(_MainTex, float2(col.g, 0.25)).g;
                col.b = tex2D(_MainTex, float2(col.b, 0.5)).b;
                col = lerp(col, tex2D(_MainTex, i.uv), _RetroAmount);
                float noise = tex2D(_NoiseTexture, i.uv * 10).r - 0.5;
                col.rgb += noise * _RetroAmount * 0.2;

                // Apply vignette effect
                float4 vignette = float4(0, 0, 0, 1);
                float2 uv = i.uv - 0.5;
                uv *= 1 - _VignetteSoftness;
                vignette.rgb = exp(-_VignetteAmount * dot(uv, uv));
                col.rgb *= vignette.rgb;

                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
