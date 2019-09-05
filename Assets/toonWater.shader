Shader "Custom/toonWater"
{
    Properties
    {	
		_DepthGradientShallow("Depth Gradient Shallow", Color) = (0.325, 0.807, 0.971, 0.725)
		_DepthGradientDeep("Depth Gradient Deep", Color) = (0.086, 0.407, 1, 0.749)
		_DepthMaxDistance("Depth Maximum Distance", Float) = 1
        _SurfaceNoise("Surface Noise", 2D) = "white" {}
        _SurfaceNoiseCutoff("Surface Noise Cutoff", Range(0, 1)) = 0.777
        _FoamDistance("Foam Distance", Float) = 0.4
        _SurfaceNoiseScroll("Surface Noise Scroll Amount", Vector) = (0.03, 0.03, 0, 0)
    }
    SubShader
    {
        Pass
        {
			CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

			float4 _DepthGradientShallow;
			float4 _DepthGradientDeep;
			float _DepthMaxDistance;
            float _SurfaceNoiseCutoff;
            float _FoamDistance;
            float2 _SurfaceNoiseScroll;
            

			sampler2D _CameraDepthTexture;

            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
				float4 screenPosition : TEXCOORD2;
                float2 noiseUV : TEXCOORD0;
            };
            
                sampler2D _SurfaceNoise;
                float4 _SurfaceNoise_ST;
            v2f vert (appdata v)
            {
                v2f o;
                


                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPosition = ComputeScreenPos(o.vertex);
                o.noiseUV = TRANSFORM_TEX(v.uv, _SurfaceNoise);

                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                //it collects the depth from the render depth buffer
                //depth is the data that how far the objects are
		        float existingDepth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPosition)).r;
                float existingDepthLinear = LinearEyeDepth(existingDepth01);
                //it converts the depth in terms of the vertex
                // screenpos.w is the vertex distance from the camera at a particular position in the screen
                // this is done coz the depth texture is given in screen space
                float depthDifference = existingDepthLinear - i.screenPosition.w;
                
                // saturate returns a range between 0-1
                float waterDepthDifference01 = saturate(depthDifference / _DepthMaxDistance);
                // lerp is a function which takes a number between 0 - 1 and outputs a color
                // if its one it returns the color at the left side and color in the right size if the input zero
                // and a color in between if the value is some where between  0 and 1
                float4 waterColor = lerp(_DepthGradientShallow, _DepthGradientDeep, waterDepthDifference01);

                // in the ine below you are add time value to scroll the uv
                float2 noiseUV = float2(i.noiseUV.x + _Time.y * _SurfaceNoiseScroll.x, i.noiseUV.y + _Time.y * _SurfaceNoiseScroll.y);
                
                // here again you are doing a check wher a value above a certain range is made one  and else its set zero
                float foamDepthDifference01 = saturate(waterDepthDifference01 / _FoamDistance);
                float surfaceNoiseCutoff = foamDepthDifference01 * _SurfaceNoiseCutoff; 
                // then you use a noise texture to add arandomeness
                float surfaceNoiseSample = tex2D(_SurfaceNoise, noiseUV).x;
                
                // this is a in-line conditional check
                // here what it does is 
                // if the left side number is  greater than right side then return the number right side of :
                // else the number at the right side of  :
                float surfaceNoise = surfaceNoiseSample > surfaceNoiseCutoff ? 1 : 0;


                return waterColor + surfaceNoise ;   
                
           }
            ENDCG
        }
    }
}