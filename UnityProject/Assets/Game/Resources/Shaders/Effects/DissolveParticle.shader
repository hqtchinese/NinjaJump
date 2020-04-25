Shader "Effect/DissolveParticle"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.BlendMode)]_SrcFactor("SrcFactor",float)= 5
        [Enum(UnityEngine.Rendering.BlendMode)]_DstFactor("DstFactor",float)= 10
        [Enum(UnityEngine.Rendering.CullMode)]_Cull("Cull",float) = 0
        [Enum(Off,0,On,1)]_ZWrite("ZWrite",float) = 0
        [HDR]_Color ("Color",color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        [Toggle]_HardEdge("HardEdge",int) = 0
        [HDR]_DisColor ("DisColor",color) = (1,1,1,0)
        _DisTex("Dissolve",2D) = "white" {}
        _MaskTex("Mask",2D) = "white"{}
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
            #pragma multi_compile_particles
            #pragma multi_compile _ _HARDEDGE_ON
           
            #pragma vertex vert
            #pragma fragment frag
      

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv0 : TEXCOORD0;
                float4 diss : TEXCOORD1;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 uv0 : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;
            uniform sampler2D _DisTex;
            uniform float4 _DisTex_ST;
            uniform sampler2D _MaskTex;
            uniform float4 _MaskTex_ST;
            uniform float4 _Color;
            uniform float4 _DisColor;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                o.uv0.xy = v.uv0.xy * _MainTex_ST.xy + _MainTex_ST.zw + v.diss.xy;
                o.uv0.zw = v.uv0.xy * _DisTex_ST.xy + _DisTex_ST.zw + v.diss.zw;
                o.uv1.xy = v.uv0.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
                o.uv1.zw = v.uv0.zw;
                o.color = v.color;
       

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture 
                float4 disTex = tex2D(_DisTex,i.uv0.zw);
                float noise = dot(disTex.rgb,fixed3(0.3,0.59,0.11))*disTex.a;
                float4 maskTex = tex2D(_MaskTex,i.uv1.xy);
                maskTex.r = dot(maskTex.rgb,fixed3(0.3,0.59,0.11))*maskTex.a;
                float4 mainTex = tex2D(_MainTex, i.uv0.xy + (float2(noise,noise)*i.uv1.w));

                float factor = 1 - i.uv1.z ;
#if _HARDEDGE_ON
                float4 col = mainTex * _Color * i.color;
                //Graphics mask
                float mask = step(noise,factor - _DisColor.a);
                //Bright width and color
                float3 edge = _DisColor.rgb * saturate(step(noise,factor)-mask);

                col.rgb *= mask;
                col.rgb += edge;
                col.a *= mainTex.a * step(noise,factor);
                
#else   
                noise = saturate(noise + factor);
                float4 col = mainTex * _Color * i.color;
                col.a = saturate( col.a * noise);
#endif
                col.a *= maskTex.r;
                return col;
            }
            ENDCG
        }
    }
}
