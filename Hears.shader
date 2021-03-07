Shader "Custom/Hears" 
{
	Properties 
	{
		_Color ("Main Color", Color) = (1, 1, 1, 1)
		_Power ("Power of Color ", Float ) = 0.5
		_Shininess ("Shininess", Range (0, 10)) = 0.5
		_Cutoff ("Base Alpha cutoff", Range ( 0, 0.9 )) = .5
		_Opacity("Opacity", Range(0,3)) = 0.5
		_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_Cube ("Cubemap", CUBE) = "_Skybox" { TexGen CubeReflect }
	}
	SubShader 
	{
		Tags { "Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout" }
		Lighting off
		// Render both front and back facing polygons.
		Cull Off
		// first pass:
		//   render any pixels that are more than [_Cutoff] opaque
		Pass 
		{  
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			struct vertexInput
			{
				float4 vertex : POSITION;
            	float4 texcoord : TEXCOORD0;
            	float3 normal : NORMAL;
            	float4 tangent : TANGENT;
			};
			struct vertexOutput
			{
				float4 pos : SV_POSITION;
            	float4 posWorld : TEXCOORD0;
            	// position of the vertex (and fragment) in world space 
            	float4 tex : TEXCOORD1;
            	float3 tangentWorld : TEXCOORD2;  
            	float3 normalWorld : TEXCOORD3;
            	float3 binormalWorld : TEXCOORD4;
			};
			sampler2D _MainTex;
			sampler2D _BumpMap;
			samplerCUBE _Cube;
			float4 _MainTex_ST;
			float4 _BumpMap_ST;
			float _Cutoff;
			float _Shininess;
			float _Power;
			float _Opacity;
			vertexOutput vert ( vertexInput input )
			{
				vertexOutput output;
				output.tangentWorld = normalize(float3(mul(_Object2World, float4(float3(input.tangent), 0.0))));
            	output.normalWorld = normalize(mul(float4(input.normal, 0.0), _World2Object));
            	output.binormalWorld = normalize(cross(output.normalWorld, output.tangentWorld)  * input.tangent.w); // tangent.w is specific to Unity
            	output.posWorld = mul(_Object2World, input.vertex);
            	output.tex = input.texcoord;
            	output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
				return output;
			}
			
			
			float4 _Color;
			float4 frag ( vertexOutput input ) : COLOR
			{
				float4 diff = _Color * tex2D( _MainTex, input.tex.rgb ) * _Power;;
				clip( diff.a - _Cutoff );
				
				float4 encodedNormal = tex2D( _BumpMap, _BumpMap_ST.xy * input.tex.xy + _BumpMap_ST.zw );
            	float3 localCoords = float3(2.0 * encodedNormal.ag - float2(1.0), 0.0);
            	localCoords.z = sqrt(1.0 - dot(localCoords, localCoords));
               	// approximation without sqrt:  localCoords.z = 1.0 - 0.5 * dot(localCoords, localCoords);
            	float3x3 local2WorldTranspose = float3x3( input.tangentWorld, input.binormalWorld, input.normalWorld);
            	float3 normalDirection = normalize(mul(localCoords, local2WorldTranspose));
            	float3 viewDirection = normalize( float3(input.posWorld) - _WorldSpaceCameraPos );
           		float3 reflectedDir = reflect( viewDirection, normalDirection );
           		
           		float4 reflColor = texCUBE(_Cube, reflectedDir) * _Shininess * diff.a;
           		float4 refl = reflColor;
           		float4 col = refl + diff;
           		col.a *= _Opacity;
				col.rgb *= _Power;
				return col;
			}
			ENDCG
		}
		
		// Second pass:
		//   render the semitransparent details.
		Pass 
		{
			Tags { "RequireOption" = "SoftVegetation" }
			// Dont write to the depth buffer
			ZWrite off
			// Set up alpha blending
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			struct appdata_t 
			{
				float4 vertex : POSITION; 
				float3 normal : NORMAL;
				float3 texcoord0: TEXCOORD0;
			};
			struct v2f 
			{
				float4 vertex : POSITION;
				float3 uvMain : TEXCOORD0;
			};
			sampler2D _MainTex;
			float4 _MainTex_ST;
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uvMain = v.texcoord0;
				return o;
			}
			float _Cutoff;
			float _Power;
			float _Opacity;
			float4 _Color;
			half4 frag (v2f i) : COLOR
			{
				half4 diff = _Color * tex2D(_MainTex, i.uvMain) * _Power;;
				clip( -(diff.a - _Cutoff) );
           		half4 col;
           		col = diff;
           		col.a *= _Opacity;
				return col;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
