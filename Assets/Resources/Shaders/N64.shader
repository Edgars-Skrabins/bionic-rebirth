Shader "Custom/N64"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainColor ("BaseColor", color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                // fixed4 col = tex2D(_MainTex, i.uv);

                // UNITY_APPLY_FOG(i.fogCoord, col);
                // return col;

                fixed4 col = tex2D(_MainTex, i.uv);
                col = col * _MainColor;
                col.a = 1.0;
                col.rgb = floor(col.rgb * 4) / 4; // Quantize color to 4 levels
                col.rgb += ddx(col.rgb) + ddy(col.rgb); // Add some dithering
                return col;
                
            }
            ENDCG
        }
    }
}