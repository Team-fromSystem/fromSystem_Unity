Shader "Unlit/MonoToon"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        ENDHLSL

        Pass
        {
            HLSLPROGRAM
            // #pragma vertex vert
            // #pragma fragment frag

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float2 uv : TEXCOORD0;
                float4 positionHCS : SV_POSITION;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            // Varyings vert(Attributes input)
            // {
            //     Varyings output;
            //     output.positionHCS = TransformObjectToHClip(input.positionOS.xyz);
            //     output.uv = input.uv;
            //     return output;
            // }

            // float4 frag(Varyings input) : SV_Target
            // {
            //     float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
            //     float gray = dot(col.rgb, float3(0.299, 0.587, 0.114));
            //     return float4(gray, gray, gray, col.a);
            // }
            ENDHLSL
        }
    }
}
