Shader "Unlit/HB"
{
    Properties
    {
        _Health ("Health", Range(0, 1)) = 1
        _ColorA ("ColorA", Color) = (0, 1, 0, 1)
        _ColorB ("ColorB", Color) = (1, 0, 0, 1)
        _ThreshHoldMin ("ThreshHoldMin", Range(0, 1)) = 0.2
        _ThreshHoldMax ("ThreshHoldMax", Range(0, 1)) = 0.8
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"
                "Queue"="Transparent" }

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha


            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _Health;
            float4 _ColorA;
            float4 _ColorB;
            float _ThreshHoldMax;
            float _ThreshHoldMin;

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float Remap (float value, float from1, float to1, float from2, float to2) 
            {
                return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
            }

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (Interpolators i) : SV_Target
            {
                float t = Remap(_Health, _ThreshHoldMin, _ThreshHoldMax, 0, 1);
                float4 col = lerp(_ColorB, _ColorA, t);
                if (i.uv.x > _Health)
                {
                    return float4(0, 0, 0, 0);
                }
                else
                {
                    return col;
                }
            }
            ENDCG
        }
    }
}
