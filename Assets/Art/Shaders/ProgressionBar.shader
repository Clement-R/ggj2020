Shader "Unlit/ProgressionBar"
{
   Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_BackgroundColor("Background Color", Color) = (1,1,1,1)
		_Amount("Amount", Range(0,1)) = 0.5
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
		}

		Cull Off
		Lighting Off
		ZWrite Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				half2 texcoord  : TEXCOORD0;
			};

			fixed4 _Color;
			fixed4 _BackgroundColor;
			float _Amount;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;

				return OUT;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// fixed4 c = lerp(_Color, _BackgroundColor, abs(1.0 - step(i.texcoord.x, _Amount)));
				fixed4 c = lerp(_BackgroundColor, _Color, step(i.texcoord.x, _Amount));
				return c;
			}
			ENDCG
		}
	}
}
