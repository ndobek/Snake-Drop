// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/World Space Pixelated"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PixelSize("Pixel Size", float) = 1
        _XOffset("X Offset", range(0,1)) = 0
        _YOffset("Y Offset", range(0,1)) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off
        ZWrite Off
        ZTest Always

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
                float2 uv : TEXCOORD0;
                float4 worldSpacePos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldSpacePos = mul(unity_ObjectToWorld, v.uv);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _PixelSize;
            float _XOffset;
            float _YOffset;

            fixed4 frag(v2f i) : SV_Target
            {
                float pxSize = _PixelSize;
                float3 adj = float3((i.worldSpacePos.x + (_XOffset * pxSize)) % pxSize, (i.worldSpacePos.y + (_YOffset * pxSize)) % pxSize, i.worldSpacePos.z % pxSize);
                fixed4 col = tex2D(_MainTex, mul(unity_WorldToObject, i.worldSpacePos - adj));
                return col;
            }
            ENDCG
        }
    }
}
