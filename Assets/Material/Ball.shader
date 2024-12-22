Shader "Unlit/Ball"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _TrailColor ("Trail Color", Color) = (1, 1, 1, 1)
        _TrailLength ("Trail Length", Float) = 1.0
        _TimeFactor ("Time Factor", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Pass
        {
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
                float4 pos : POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _TrailColor;
            float _TrailLength;
            float _TimeFactor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                // Calculate fading effect based on time
                float timeFade = max(0, 1 - (_Time - v.vertex.x) / _TrailLength);
                o.color = _TrailColor * timeFade;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                return i.color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
