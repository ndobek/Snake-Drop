// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Pixel Glass"
{
    Properties
    {
        _PixelSize("Pixel Size", float) = 1
    }
        SubShader
        {
            // Draw after all opaque geometry
            Tags { "Queue" = "Transparent" }

            // Grab the screen behind the object into _BackgroundTexture
            GrabPass
            {
                "_BackgroundTexture"
            }

            // Render the object with the texture generated above, and invert the colors
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct v2f
                {
                    float4 grabPos : TEXCOORD0;
                    float4 pos : SV_POSITION;
                };

                v2f vert(appdata_base v) {
                    v2f o;
                    // use UnityObjectToClipPos from UnityCG.cginc to calculate 
                    // the clip-space of the vertex
                    o.pos = UnityObjectToClipPos(v.vertex);

                    // use ComputeGrabScreenPos function from UnityCG.cginc
                    // to get the correct texture coordinate
                    o.grabPos = ComputeGrabScreenPos(o.pos);
                    return o;
                }

                sampler2D _BackgroundTexture;
                float _PixelSize;

                half4 frag(v2f i) : SV_Target
                {
                    float pxSize = _PixelSize * unity_CameraProjection._m11 / _ScreenParams.x;
                    float4 adj = float4(i.grabPos.x % pxSize, i.grabPos.y % pxSize, i.grabPos.z % pxSize,0);
                    half4 bgcolor = tex2Dproj(_BackgroundTexture, (i.grabPos - adj));
                    return bgcolor;
                }
                ENDCG
            }

        }
    }