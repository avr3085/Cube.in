// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Grid/ExpShader"
{
    Properties
    {

    }

    SubShader
    {
        // Tags{"RanderType" = "Opaque"}
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        // Pass
        // {
        //     CGPROGRAM
        //     #pragma vertex vert
        //     #pragma fragment frag

        //     #include "UnityCG.cginc"

        //     struct appdata
        //     {
        //         half4 vertex : POSITION;
        //         half2 uv : TEXCOORD0;  
        //     };

        //     struct v2f
        //     {
        //         half4 vertex : SV_POSITION;
        //         half2 uv : TEXCOORD0;
        //         half dist : TEXCOORD1;
        //     };

        //     v2f vert(appdata v)
        //     {
        //         v2f o;
        //         o.vertex = UnityObjectToClipPos(v.vertex);
        //         half4 tDist = mul(UNITY_MATRIX_IT_MV, v.vertex);
        //         o.dist = tDist.z;

        //         o.uv = v.uv;
        //         return o;
        //     }

        //     half4 frag(v2f i) : SV_Target
        //     {
        //         half val = step(i.uv.y, 0.3);
        //         return half4(0, 1, 0, val);
        //     }
        //     ENDCG

        // }

        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                half4 vertex : POSITION;
                half2 uv : TEXCOORD0;  
            };

            struct v2f
            {
                half4 vertex : SV_POSITION;
                half2 uv : TEXCOORD0;
                half dist : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                half4 tDist = mul(UNITY_MATRIX_IT_MV, v.vertex);
                o.dist = tDist.z;

                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                return half4(i.dist, i.dist, i.dist,1);
            }
            ENDCG

        }

        
    }
}