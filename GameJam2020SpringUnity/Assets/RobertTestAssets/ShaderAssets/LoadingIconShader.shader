Shader "Unlit/LoadingIconShader" {
    Properties {
		_PrimaryTex ("Main Tex", 2D) = "black" {}
	    _VisualInput ("VisualInput", 2D) = "black" {}
        _ShaderInput ("ShaderInput", 2D) = "black" {}
		
		_VOffset ("VOffset", Range (0.0, 1.0)) = 1.0
		_HOffset ("HOffset", Range (0.0, 1.0)) = 0.0



    }
    SubShader {
	LOD 100

	Tags {
	    "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
	
	}

        Pass {
			Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };


            float4 _MainTex_ST;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
			
			
			sampler2D _PrimaryTex;
            sampler2D _VisualInput;
            sampler2D _ShaderInput;
			Float _VOffset;
			Float _HOffset;
            fixed4 frag (v2f i) : SV_Target {
				float x = _HOffset + tex2D(_ShaderInput,i.uv).g;
				float y = _VOffset + tex2D(_ShaderInput,i.uv).r;

				return tex2D(_VisualInput,float2(x,y)) *  tex2D(_PrimaryTex,i.uv).a;
			
			}
			ENDCG
		}
        
    }
}

