Shader "Cube_in/Res/ToonShaderMultiCol"
{
    Properties
    {
        _MainTex ("Main Tex", 2D) = "white" {}
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
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"

            struct appdata
            {
                half4 vertex : POSITION;
                half2 uv : TEXCOORD0;
                half3 normal : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                half4 vertex : SV_POSITION;
                half2 uv : TEXCOORD0;
                half3 normal : TEXCOORD1;
                half4 color : COLOR;
            };

            sampler2D _MainTex;
            half4 _MainTex_ST;
            half4 _Colors[1023];

            v2f vert(appdata v, uint instanceID: SV_InstanceID)
            {
                //Enable instancing
                UNITY_SETUP_INSTANCE_ID(v);
                
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = v.uv;

                #ifdef UNITY_INSTANCING_ENABLED
                    o.color = _Colors[instanceID];
                #endif

                return o;
            }   

            half4 frag(v2f i) : SV_Target
            {
                half3 dLightPos = half3(0.1,0.1,0.1); //custom directional light
                half4 tex = tex2D(_MainTex, i.uv);
                // half3 norm = normalize(i.normal); // normalize
                half dirLight = 1 - dot(dLightPos, i.normal);

                return i.color * tex * dirLight;
            }

            ENDCG
        }
    }
}