Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LowRad ("LowRad", float) = 0.3
        _HighRad ("HighRad", float) = 0.5
        _Color ("Color", Color) = (0,0,0,0)
    }
    SubShader
    {
        Tags {"RenderType"="Transparent" "Queue"="Transparent"}
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha

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

            float4 _MainTex_ST;
            sampler2D _MainTex;
            float _LowRad;
            float _HighRad;
            float4 _Color;

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
                float4 col = float4(0,0,0,0);
                
                float2 uvcenter = float2(0.5,0.5);
                
                float d = distance(i.uv, uvcenter);

                if(d < _HighRad && d >_LowRad ){
                    col = _Color;
                }

                return col;
            }
            ENDCG
        }
    }
}
