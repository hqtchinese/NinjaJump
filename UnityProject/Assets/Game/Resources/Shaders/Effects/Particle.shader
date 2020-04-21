Shader "Effect/Particle"
{
    Properties
    {
        [Emun(SrcAlpha,5,One,1)]_SrcFactor("SrcFactor",float)= 5
        [Emun(OneMinusSrcAlpha,10,One,1)]_DstFactor("DstFactor",float)= 10
        [Emun(Off,0,Back,1,Front,2)]_Cull("Cull",float) = 0
        [HDR]_Color ("Color",color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" "PreviewType" = "Plane"}
        blend [_SrcFactor] [_DstFactor]
        cull [_Cull]
        ZWrite off
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
                float4 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // 粒子系统custom data可以控制UV的偏移
                o.uv = v.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw * v.uv.zw;
                o.color = v.color * _Color;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                return  fixed4(tex2D(_MainTex, i.uv) * i.color);
            }
            ENDCG
        }
    }
}
