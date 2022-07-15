
void ToonShadingAdditionalLight_float(in float3 WorldPos, in int Index, out float3 Direction, out float3 Color, out float DistanceAttenuation, out float ShadowAttenuation)
{
    Direction = normalize(float3(0.5, 0.5, 0.25));
    Color = float3(0.0, 0.0, 0.0);
    DistanceAttenuation = 0.0f;
    ShadowAttenuation = 0.0f;
    
#ifndef SHADERGRAPH_PREVIEW
    int pixelLightCount = GetAdditionalLightsCount();
    if(Index < pixelLightCount)
    {
        Light light = GetAdditionalLight(Index, WorldPos);
        
        Direction = light.direction;
        Color = light.color;
        DistanceAttenuation = light.distanceAttenuation;
        ShadowAttenuation = light.shadowAttenuation;
    }
#endif
}