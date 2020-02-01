Shader "Unlit/PokemonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "black" {}
        _BitMapTex ("BitMap", 2D) = "white" {}
        _AltTex ("Alternative Texture", 2D) = "white" {}
		_Threshold ("Threshold", Range (0.0, 1.0)) = 1.0

    }
    SubShader
    {
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

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };


            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _BitMapTex;
            sampler2D _AltTex;
			Float _Threshold;
            fixed4 frag (v2f i) : SV_Target
            {
				if( tex2D(_BitMapTex,i.uv).r <= _Threshold && _Threshold != 0) {
					return tex2D(_MainTex,i.uv);
				} else {
					return tex2D(_AltTex,i.uv);
				}
	


			}
			ENDCG
		}
        
    }
}

