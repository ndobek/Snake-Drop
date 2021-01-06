// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Pixelated"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PixelSize("Pixel Size", float) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Tags
        {
            "Queue" = "Transparent"
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"



            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = mul(unity_ObjectToWorld, v.uv);
                return o;
            }

            sampler2D _MainTex;
            float _PixelSize;

            fixed4 frag(v2f i) : SV_Target
            {
                float pxSize = _PixelSize * unity_CameraProjection._m11  / _ScreenParams.x;
                float3 adj = float3(i.uv.x % pxSize, i.uv.y % pxSize, i.uv.z % pxSize);
                fixed4 col = tex2D(_MainTex, mul(unity_WorldToObject, i.uv - adj));
                return col;
            }
            ENDCG
        }
    }
}
