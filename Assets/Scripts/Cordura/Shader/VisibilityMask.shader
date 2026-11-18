

Shader "UI/VisibilityMaskURP"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,1)
        _Radius ("Vision Radius", Float) = 0.35
        _Softness ("Edge Softness", Float) = 0.03
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "RenderPipeline"="UniversalPipeline"
        }

        Cull Off
        ZWrite Off
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            float4 _Color;
            float _Radius;
            float _Softness;
            float4 _PlayerScreenPos;

            struct Attributes
            {
                float4 position : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.position = TransformObjectToHClip(v.position.xyz);
                o.uv = v.uv;
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                float2 delta = i.uv - _PlayerScreenPos.xy;

                delta.x *= _ScreenParams.x / _ScreenParams.y;

                float distanceFromPlayer = length(delta);

                float darkness = smoothstep(
                    _Radius - _Softness,
                    _Radius + _Softness,
                    distanceFromPlayer
                );

                return half4(
                        0,
                        0,
                        0,
                        darkness
                    );
            }

            ENDHLSL
        }
    }
}
