Shader "Unlit/MirrorShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BitMapTex ("BitMap", 2D) = "white" {}
        _AltTex ("Alternative Texture", 2D) = "white" {}
	_XOffset ("X_Offset", Range (0.0, 1.0)) = 1.0
	_YOffset ("X_Offset", Range (0.0, 1.0)) = 1.0
	_XScaling ("X_Offset", Range (0.0, 10.0)) = 1.0
	_YScaling ("X_Offset", Range (0.0, 10.0)) = 1.0
    }
    SubShader
    {
	LOD 100

	Tags {
	    "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
	
	}

        Pass
        {
            ColorMask RGB
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
	    Float _XOffset;
            Float _YOffset;
	    Float _XScaling;
	    Float _YScaling;

            fixed4 frag (v2f i) : SV_Target
            {
            
		if(tex2D(_BitMapTex,i.uv).a == 0) {
                	fixed4 col = tex2D(_MainTex, i.uv);
                	return col;
		} else {
		   float2 newUV = float2(i.uv.x * _XScaling + _XOffset,i.uv.y * _YScaling + _YOffset);
		   return tex2D(_AltTex, newUV);
		}
		
            }
            ENDCG
        }
    }
}
