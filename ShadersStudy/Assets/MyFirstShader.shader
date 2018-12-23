Shader "ShaderStudy/FirstShader" {

Properties{
	_myColour("Example Colour", Color) = (1, 1, 1)  //Shows in the inspector the first color that it will be set as default
	_EmissionColour("Emission Colour", Color) =(1, 1, 1)
}

SubShader{
	CGPROGRAM
#pragma surface surf Lambert //1- name of the type of shader, 2- name of shader function, 3- Lighting model

	struct Input { //Input data from the model's mesh
		float2 uvMainTex;
};

		fixed4 _myColour; //Array of four float value?
		fixed4 _EmissionColour;

		void surf(Input IN, inout SurfaceOutput o)
		{
			o.Albedo = _myColour.rgb;
			o.Emission = _EmissionColour.rgb;
		}

		ENDCG
}
	FallBack "Diffuse" //Less GPU intense
}
