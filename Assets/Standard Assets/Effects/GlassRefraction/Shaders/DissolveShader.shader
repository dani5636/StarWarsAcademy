Shader "Unlit/DissolveShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		/*[Gamma]  _Metallic ("Metallic", Range(0.000000,1.000000)) = 0.000000
		_MetallicGlossMap ("Metallic", 2D) = "white" { }
		_BumpScale ("Scale", Float) = 1.000000
		_BumpMap ("Normal Map", 2D) = "bump" { }
		_OcclusionStrength ("Strength", Range(0.000000,1.000000)) = 1.000000
		_OcclusionMap ("Occlusion", 2D) = "white" { } */

		_DissolveTexture("DissolveShader", 2D) = "white" {}
		_DissolveY("Current Y of dissolve effect", Float) = 0
		_DissolveSize("Size of the effect", Float) = 2
		_StartingY("Starting point of the effect", Float) = -10
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
				float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _DissolveTexture;
			float _DissolveY;
			float _DissolveSize;
			float _StartingY;

			/*sampler2D _BumpMap;
			float _BumpScale;

			sampler2D _OcclusionMap;
			float _OcclusionStrength;

			sampler2D _MetallicGlossMap;
			float _Metallic;
			*/
		
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float transition = _DissolveY - i.worldPos.y;
				clip(_StartingY + (transition + (tex2D( _DissolveTexture,i.uv)) * _DissolveSize));
			

				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}
