Shader "Hidden/CustomDepth"
{
    Properties
    {
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"  }
        LOD 100

        Pass
        {
            Cull Off
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {                
                float4 vertex : SV_POSITION;
                float3 positionWS:TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                VertexPositionInputs positionInputs = GetVertexPositionInputs(v.vertex.xyz);
                o.vertex = positionInputs.positionCS;
                o.positionWS = positionInputs.positionWS;
                return o;
            }

            real4 frag (v2f i) : SV_Target
            {
                real4 col = length( i.positionWS-_WorldSpaceCameraPos); 
                return col/_ProjectionParams.z;
            }
            ENDHLSL
        }
    }
}