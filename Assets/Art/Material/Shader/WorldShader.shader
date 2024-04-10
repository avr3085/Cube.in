Shader "Snake_in/WorldShader"
{
    Properties
    {
        [NoScaleOffset]_MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _ProjectionScale ("Scale", Integer) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                half4 vertex : POSITION;
            };

            struct v2f
            {
                half4 vertex : SV_POSITION;
                half3 worldPosition : TEXCOORD0;
            };

            sampler2D _MainTex;
            // half4 _MainTex_ST;
            half4 _Color;
            half _ProjectionScale;

            v2f vert (appdata v)
            {
                v2f o;
                o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half2 projection = i.worldPosition.xy;
                half4 col = tex2D(_MainTex, projection / _ProjectionScale);

                return col * _Color;
            }
            ENDCG
        }
    }
}
