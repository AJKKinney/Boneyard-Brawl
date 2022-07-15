void ToonShadingMainLight_float(in float3 WorldPos, out float3 Direction, out float3 Color, out float DistanceAttenuation, out float ShadowAttenuation)
{ 
#ifdef SHADERGRAPH_PREVIEW
	Direction = float3(0.5, 0.5, 0);
	Color = float3(1.0, 1.0, 1.0);
	DistanceAttenuation = 1.0;
	ShadowAttenuation = 1.0;
#else
    float4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
    Light mainLight = GetMainLight(shadowCoord);
	
    Direction = mainLight.direction;
    Color = mainLight.color;
    DistanceAttenuation = mainLight.distanceAttenuation;
    ShadowAttenuation = mainLight.shadowAttenuation;
#endif

}