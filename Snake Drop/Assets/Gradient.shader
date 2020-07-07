﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Gradient"
{
    Properties
    {
         _TopColor("Top Color", Color) = (1, 1, 1, 1)
         _BottomColor("Bottom Color", Color) = (1, 1, 1, 1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
    }
        SubShader
    {
    
         Pass
         {
             Blend SrcAlpha OneMinusSrcAlpha
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             struct vertexIn {
                 float4 pos : POSITION;
                 float2 uv : TEXCOORD0;
             };
             struct v2f {
                 float4 pos : SV_POSITION;
                 float2 uv : TEXCOORD0;
             };
             v2f vert(vertexIn input)
             {
                 v2f output;
                 output.pos = UnityObjectToClipPos(input.pos);
                 output.uv = input.uv;
                 return output;
             }
             fixed4 _TopColor, _BottomColor;
             sampler2D _MainTex;
             fixed4 frag(v2f input) : COLOR
             {
                 return lerp(_BottomColor, _TopColor, (input.uv.x *.5) + (input.uv.y * .5)) * tex2D(_MainTex, input.uv);
             }
             ENDCG
         }
    }
}
