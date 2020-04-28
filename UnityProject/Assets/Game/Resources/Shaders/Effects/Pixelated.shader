Shader "Unlit/Pixe"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.BlendMode)]_SrcFactor("SrcFactor",float)= 5
        [Enum(UnityEngine.Rendering.BlendMode)]_DstFactor("DstFactor",float)= 10
        [Enum(UnityEngine.Rendering.CullMode)]_Cull("Cull",float) = 0
        [Enum(Off,0,On,1)]_ZWrite("ZWrite",float) = 0
        [HDR]_Color ("Color",color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _Factor ("Factor",float) = 35
    }
    SubShader
    {
        Tags { 
            "RenderType"="Transparent" 
            "Queue" = "Transparent" 
            "IgnoreProjector"="True" 
            "PreviewType" = "Plane"
        }
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
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float3 uv0 : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;
            uniform float4 _Color;
            uniform float _Factor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
               
                o.uv0.xy = v.uv0.xy * _MainTex_ST.xy + _MainTex_ST.zw + float2(v.uv0.z,v.uv0.w-1);
                o.uv0.z = v.uv0.z;
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float2 UV = floor( i.uv0.xy * (_Factor + i.uv0.z) / (_Factor + i.uv0.z));
                fixed4 col = tex2D(_MainTex,UV) * i.color * _Color;
 
                return col;
            }
            ENDCG
        }
    }
}
