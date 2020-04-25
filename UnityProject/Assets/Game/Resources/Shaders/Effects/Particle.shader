Shader "Effect/Particle"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.BlendMode)]_SrcFactor("SrcFactor",float)= 5
        [Enum(UnityEngine.Rendering.BlendMode)]_DstFactor("DstFactor",float)= 10
        [Enum(UnityEngine.Rendering.CullMode)]_Cull("Cull",float) = 0
        [Enum(Off,0,On,1)]_ZWrite("ZWrite",float) = 0
        [HDR]_Color ("Color",color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" "IgnoreProjector"="True" "PreviewType" = "Plane"}
        blend [_SrcFactor] [_DstFactor]
        cull [_Cull]
        ZWrite [_ZWrite]
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
                float4 uv0 : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv0 : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : TEXCOORD1;
            };

            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;
            uniform float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // The custom data of the particle system can control the UV offset
                o.uv0 = v.uv0.xy * _MainTex_ST.xy + _MainTex_ST.zw + v.uv0.zw;
                o.color = v.color;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                fixed4 col = tex2D(_MainTex, i.uv0) * i.color * _Color;

                return col;
            }
            ENDCG
        }
    }
}
