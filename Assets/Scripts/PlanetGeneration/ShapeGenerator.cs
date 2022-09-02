using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShapeGenerator
{
    PlanetProperties planetProperties;
    NoiseInterface[] noiseLayers;
    MinMaxHeight heightRange;

    public ShapeGenerator(PlanetProperties planetProperties)
    {
        this.planetProperties = planetProperties;
        heightRange = new MinMaxHeight();

        noiseLayers = new NoiseInterface[planetProperties.noiseProperties.Length];
        for(int i = 0; i < noiseLayers.Length; i++)
        {
            noiseLayers[i] = NoiseFilterSelector.GetLayerNoise(planetProperties.noiseProperties[i]);
        }
    }

    public void updateElevation()
    {
        planetProperties.materialProperties.planetMaterial.SetVector("_elevationMinMax", new Vector4(0, 0, heightRange.min, heightRange.max));
    }

    public void setCenter(Vector3 center)
    {
        planetProperties.materialProperties.planetMaterial.SetVector("_planetCenter", new Vector4(center.x, center.y, center.z));
    }

    public void MapTextures()
    {
        planetProperties.materialProperties.mapTexturesToShader();
    }


    public Vector3 getPointHeight(Vector3 point)
    {
        float pointHeight = 0f;

        for (int i = 0; i < planetProperties.noiseProperties.Length; i++)
        {
            pointHeight += noiseLayers[i].getTerrainHeight(point);
        }

        float elevation = planetProperties.planetRadius * (1 + pointHeight);
        heightRange.checkValue(elevation);

        return point * elevation;
    }
}
