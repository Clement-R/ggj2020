Shader "Unlit/Background"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _ColorTop("Color Top", Color) = (1,1,1,1)
        _ColorBot("Color Bot", Color) = (1,1,1,1)
		_Speed("Speed", float) = 1
		_Amount("Amount", float) = 12.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            fixed4 _ColorTop;
            fixed4 _ColorBot;
            float _Speed;
            float _Amount;

            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                i.uv.y += sin(_Time * _Speed) / _Amount;
                fixed4 col = lerp(_ColorBot, _ColorTop, i.uv.y);
                return col;
            }
            ENDCG
        }
    }
}
