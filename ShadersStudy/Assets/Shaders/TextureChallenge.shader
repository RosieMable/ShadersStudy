Shader "ShaderStudy/TextureTry"{

	Properties{

		_myTexDif ("Diffuse Texture", 2D) = "white" {}
		_myTexEm ("Emmission Texture", 2D) = "white" {}
	}

SubShader
	{
		CGPROGRAM
	#pragma surface surf Lambert

	sampler2D _myTexDif;
	sampler2D _myTexEm;

	struct Input {
		float2 uv_myTexDif;
		float2 uv_myTexEm;
	};

	void surf(Input IN, inout SurfaceOutput o) {

		o.Albedo = (tex2D(_myTexDif, IN.uv_myTexDif)).rgb;
		o.Emission = (tex2D(_myTexEm, IN.uv_myTexEm)).rgb;
	}

	ENDCG
	}
		Fallback "Diffuse"
}