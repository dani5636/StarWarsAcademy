// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Custom/Highlight Cube" 
{

	SubShader {
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			struct appdata
			{
				float4 vertex: POSITION;
				float3 normal: normal;
			};
			struct v2f
			{
				float4 vertex: SV_POSITION;
				float3 normal: TEXCOORD0;
				float3 viewDir: TEXCOORD1;
			};

			v2f vert(appdata v){
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);
				return o;
			}

			fixed frag(v2f i) : SV_Target
			{
			float ndotv =(1 - dot(i.normal, i.viewDir))*5;
			
				return float4(ndotv,ndotv,ndotv,0);
			}
			ENDCG
		}
	}
}
