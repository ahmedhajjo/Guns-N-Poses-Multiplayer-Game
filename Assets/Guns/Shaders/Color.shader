Shader "ahmedhajjo/Color"
{
	Properties
	{
		_Color("Texture", Color) = (1,0,0,1)
		_MainTex("Texture", 2D) = "white" {}
		_Frequency("Texture", Float) = 1
		_Amplitude("Texture", Float) = 1
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;

			};

			struct v2f
			{
				/*float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)*/
				float4 vertex : SV_POSITION;
				float3 normal:NOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Frequency;
			float _Amplitude;
			v2f vert(appdata v)
			{
				v2f o;
				v.vertex = mul(unity_ObjectToWorld,v.vertex);

				v.vertex.y+=_Amplitude*sin(length(v.vertex.xz)* _Frequency + _Time.w);





				v.vertex.y+= _Amplitude/10*sin(3 *length(v.vertex.xz) * _Frequency + _Time.w);
				/*o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);*/
				o.vertex = mul(UNITY_MATRIX_VP, v.vertex);
				o.normal = mul(UNITY_MATRIX_MV, v.normal);

				return o;
			}

			fixed3 frag(v2f i) : SV_Target
			{
				// sample the texture
			//	fixed4 col = tex2D(_MainTex, i.uv);
			//// apply fog
			//UNITY_APPLY_FOG(i.fogCoord, col);
			return float3(1,0,0);
		}
		ENDCG
	}
	}
}
