// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'
// Upgrade NOTE: replaced '_ProjectorClip' with 'unity_ProjectorClip'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Projector/Multiply_NoEdge" {
    Properties {
        _ShadowTex ("Cookie", 2D) = "gray" {}
        _FalloffTex ("FallOff", 2D) = "white" {}
    }
    Subshader {
        Tags {"Queue"="Transparent"}
        Pass {
            ZWrite Off
            Fog { Color (1, 1, 1) }
            AlphaTest Greater 0
            ColorMask RGB
            Blend DstColor Zero
            Offset -0.2, -0.2
            
            CGPROGRAM    
            // Upgrade NOTE: excluded shader from DX11 and Xbox360; has structs without semantics (struct v2f members normal)
            #pragma exclude_renderers d3d11 xbox360
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
           
            struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
           
            struct v2f {
                float4 uvShadow : TEXCOORD0;
                float4 uvFalloff : TEXCOORD1;
                float4 pos : SV_POSITION;
                float3 wNormal;
            };
           
            float4x4 unity_Projector;
            float4x4 unity_ProjectorClip;
           
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                o.uvShadow = mul (unity_Projector, v.vertex);
                o.uvFalloff = mul (unity_ProjectorClip, v.vertex);
               
                //WorldNormalVector?
                o.wNormal = mul( unity_ObjectToWorld, float4( v.normal, 0.0 ) ).xyz;
                return o;
            }
           
            sampler2D _ShadowTex;
            sampler2D _FalloffTex;
           
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 res;
               
                if(i.wNormal.y == 1) {
                    fixed4 texS = tex2Dproj (_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
                    texS.a = 1.0-texS.a;
                   
                    fixed4 texF = tex2Dproj (_FalloffTex, UNITY_PROJ_COORD(i.uvFalloff));
                    res = lerp(fixed4(1,1,1,0), texS, texF.a);
                } else {
                    res = fixed4(0,0,0,0);
                }
                return res;
            }           
            ENDCG
        }
    }
}
 