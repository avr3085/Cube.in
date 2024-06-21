Shader "Grid/ToonShader"
{
    Properties
    {
        _MainTex ("Main Tex", 2D) = "white" {}
        // _Color ("Color", Color) = (1,1,1,1)
        _AmbientColor ("Ambient Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags{"RenderType" = "Opaque"}
        LOD 100

        Pass
        { 
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
			#include "AutoLight.cginc"

            struct appdata
            {
                half4 vertex : POSITION;
                half2 uv : TEXCOORD0;
                half3 normal : NORMAL;
            };

            struct v2f
            {
                half4 vertex : SV_POSITION;
                half2 uv : TEXCOORD0;
                half3 normal : NORMAL;
            };

            sampler2D _MainTex;
            half4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                // o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv = v.uv;
                return o;
            }   


            // half4 _Color;
            half4 _AmbientColor;

            half4 frag(v2f i) : SV_Target
            {
                //Defining a custom light direction
                half3 dLightPos = half3(0.1,0.1,0.1);
                half4 col = tex2D(_MainTex, i.uv);
                // half3 norm = normalize(i.normal);
                half dirLight = 1 - dot(dLightPos, i.normal);

                return col * dirLight;
            }

            ENDCG
        }
    }
}