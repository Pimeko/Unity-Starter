Shader "Sleek Render/Post Process/Brightpass Dualfilter Blur"
{
    Properties
    {
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct v2f
            {
                half4 vertex : SV_POSITION;
                half2 uv[8] : TEXCOORD0;
            };

            // Common struct for SleekRender image effects
            struct appdata
            {
                half4 vertex : POSITION;
                half4 uv : TEXCOORD0;
            };

            sampler2D_half _MainTex;
            float4 _TexelSize;

            // Using Markus-upsample formula for UV offset calculations in vertex shader
            void calculateUpsampleTapPoints(appdata v, half2 halfpixel, out half2 uv[8])
            {
                uv[0] = v.uv + half2(-halfpixel.x * 2.0, 0.0);
                uv[1] = v.uv + half2(-halfpixel.x, halfpixel.y);
                uv[2] = v.uv + half2(0.0, halfpixel.y * 2.0);
                uv[3] = v.uv + half2(halfpixel.x, halfpixel.y);
                uv[4] = v.uv + half2(halfpixel.x * 2.0, 0.0);
                uv[5] = v.uv + half2(halfpixel.x, -halfpixel.y);
                uv[6] = v.uv + half2(0.0, -halfpixel.y * 2.0);
                uv[7] = v.uv + half2(-halfpixel.x, -halfpixel.y);
            }

            // Texture fetch + brightpass through a predefined pixel luminance threshold
            half4 getTapAndLumaFrom(sampler2D_half tex, half2 uv, half4 luminanceThreshold)
            {
                half4 tap = tex2D(tex, uv.xy);
                // Calculating the pixel brightness
                half luma = saturate(dot(half4(tap.rgb, 1.0h), luminanceThreshold)); 
                // Makes dark pixels black, leaving only bright-enough pixels on the scene
                return tap *= luma;
            }

            // Using Markus-upsample formula for pixel value calculation
            // Five-tap boxy blur
            // Version without brightpass
            half4 applyUpsampleBrightpassTapLogic(half2 uv[8], sampler2D_half tex, half4 luminanceThreshold)
            {
                half4 result = getTapAndLumaFrom(tex, uv[0], luminanceThreshold);
                result += getTapAndLumaFrom(tex, uv[1], luminanceThreshold) * 2.0;
                result += getTapAndLumaFrom(tex, uv[2], luminanceThreshold);
                result += getTapAndLumaFrom(tex, uv[3], luminanceThreshold) * 2.0;
                result += getTapAndLumaFrom(tex, uv[4], luminanceThreshold);
                result += getTapAndLumaFrom(tex, uv[5], luminanceThreshold) * 2.0;
                result += getTapAndLumaFrom(tex, uv[6], luminanceThreshold);
                result += getTapAndLumaFrom(tex, uv[7], luminanceThreshold) * 2.0;
    
                return result / 12.0h;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = v.vertex;
                calculateUpsampleTapPoints(v, _TexelSize.xy, o.uv);

                if (_ProjectionParams.x < 0)
                {
                    for(int i = 0; i < 8; i++)
                    {
                        o.uv[i].y = 1.0h - o.uv[i].y;
                    }
                }

                return o;
            }
            
            half4 _LuminanceThreshold;

            half4 frag (v2f i) : SV_Target
            {
                return applyUpsampleBrightpassTapLogic(i.uv, _MainTex, _LuminanceThreshold);
            }
            ENDCG
        }
    }
}
