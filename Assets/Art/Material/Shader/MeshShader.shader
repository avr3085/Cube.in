Shader "Snake_in/MeshShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        // _Color ("Color", Color) = (1,1,1,1)
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
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"

            struct appdata
            {
                half4 vertex : POSITION;
                half4 uv : TEXCOORD0;
                half4 color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                half2 uv : TEXCOORD0;
                half4 vertex : SV_POSITION;
                half4 color : COLOR;
            };

            sampler2D _MainTex;
            half4 _MainTex_ST;
            // half4 _Colors[1023];

            v2f vert (appdata v, uint instanceID: SV_InstanceID)
            {
                //Enable instancing
                UNITY_SETUP_INSTANCE_ID(v);

                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                // o.color = _Colors[instanceID];
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);
                // return col * _Color;
                // return col * i.color;
                return col;
            }
            ENDCG
        }
    }
}
