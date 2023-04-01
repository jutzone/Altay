Shader "Custom/AlphaMask" {

    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Alpha ("Alpha (A)", 2D) = "white" {}
    }
    
    SubShader {
        Tags { "RenderType" = "Transparent" "Queue" = "AlphaTest"}
        
        ZWrite Off
        
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask RGBA
        
        Pass {
            SetTexture[_MainTex] {
                Combine texture
            }
            SetTexture[_MainTex] {
                Combine previous, texture*previous//texture lerp (texture) previous
            }
        }
    }
}